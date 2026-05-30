namespace DuneTools.Views;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Styling;
using Avalonia.Threading;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using AvaloniaHex;
using AvaloniaHex.Document;
using AvaloniaHex.Editing;
using AvaloniaHex.Rendering;
using DuneTools.Behaviors;
using DuneTools.ViewModels;

public partial class MainView : UserControl
{
    private readonly DispatcherTimer _selectionDebounceTimer = new() { Interval = TimeSpan.FromMilliseconds(16) };
    private HexSelectionChangedEventArgs? _pendingSelection;
    private readonly RangesHighlighter _globalsHighlighter = new();
    private readonly RangesHighlighter _troopsHighlighter = new();
    private readonly RangesHighlighter _locationsHighlighter = new();
    private readonly RangesHighlighter _smugglersHighlighter = new();
    private readonly RangesHighlighter _npcsHighlighter = new();
    private readonly RangesHighlighter _compressionHighlighter = new();
    private readonly RangesHighlighter _unknownHighlighter = new();
    private MainViewModel? _viewModel;

    public MainView()
    {
        InitializeComponent();
        ConfigureHighlighters();
        AttachSelectionSyncBehavior();
        DataContextChanged += OnDataContextChanged;
        HexFieldSelectionBehavior.SelectionChanged += OnHexSelectionChanged;
        DetachedFromVisualTree += OnDetachedFromVisualTree;
        _selectionDebounceTimer.Tick += OnSelectionDebounceTick;
        RefreshRangeHighlighting();
    }

    private void OnDetachedFromVisualTree(object? sender, Avalonia.VisualTreeAttachmentEventArgs e)
    {
        if (_viewModel is not null)
        {
            _viewModel.PropertyChanged -= OnViewModelPropertyChanged;
        }

        HexFieldSelectionBehavior.SelectionChanged -= OnHexSelectionChanged;
        DataContextChanged -= OnDataContextChanged;
        _selectionDebounceTimer.Tick -= OnSelectionDebounceTick;
        DetachedFromVisualTree -= OnDetachedFromVisualTree;
    }

    private void ConfigureHighlighters()
    {
        Color knownColor = GetRequiredColor("SystemAccentColor");
        Color unknownColor = GetRequiredColor("SystemChromeLowColor");

        _globalsHighlighter.Background = new SolidColorBrush(knownColor, 0.20);
        _troopsHighlighter.Background = new SolidColorBrush(ShiftHue(knownColor, 52), 0.20);
        _locationsHighlighter.Background = new SolidColorBrush(ShiftHue(knownColor, 114), 0.20);
        _smugglersHighlighter.Background = new SolidColorBrush(ShiftHue(knownColor, 176), 0.20);
        _npcsHighlighter.Background = new SolidColorBrush(ShiftHue(knownColor, 238), 0.20);
        _compressionHighlighter.Background = new SolidColorBrush(ShiftHue(knownColor, 300), 0.22);
        _unknownHighlighter.Background = new SolidColorBrush(unknownColor, 0.12);
    }

    private void OnDataContextChanged(object? sender, EventArgs e)
    {
        if (_viewModel is not null)
        {
            _viewModel.PropertyChanged -= OnViewModelPropertyChanged;
        }

        _viewModel = DataContext as MainViewModel;
        if (_viewModel is not null)
        {
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        RefreshRangeHighlighting();
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(MainViewModel.Document) || e.PropertyName == nameof(MainViewModel.HighlightSnapshot))
        {
            RefreshRangeHighlighting();
        }
    }

    private void RefreshRangeHighlighting()
    {
        HexEditor? editor = this.FindControl<HexEditor>("HexEditorControl");
        if (editor?.HexView is null)
        {
            return;
        }

        AttachHighlighters(editor.HexView);
        ApplyRanges(_globalsHighlighter.Ranges, _viewModel?.HighlightSnapshot.GlobalsRanges);
        ApplyRanges(_troopsHighlighter.Ranges, _viewModel?.HighlightSnapshot.TroopRanges);
        ApplyRanges(_locationsHighlighter.Ranges, _viewModel?.HighlightSnapshot.LocationRanges);
        ApplyRanges(_smugglersHighlighter.Ranges, _viewModel?.HighlightSnapshot.SmugglerRanges);
        ApplyRanges(_npcsHighlighter.Ranges, _viewModel?.HighlightSnapshot.NpcRanges);
        ApplyRanges(_compressionHighlighter.Ranges, _viewModel?.HighlightSnapshot.CompressionRanges);
        ApplyRanges(_unknownHighlighter.Ranges, _viewModel?.HighlightSnapshot.UnknownRanges);
    }

    private void AttachHighlighters(HexView hexView)
    {
        if (!hexView.LineTransformers.Contains(_npcsHighlighter))
        {
            hexView.LineTransformers.Add(_npcsHighlighter);
        }

        if (!hexView.LineTransformers.Contains(_smugglersHighlighter))
        {
            hexView.LineTransformers.Add(_smugglersHighlighter);
        }

        if (!hexView.LineTransformers.Contains(_locationsHighlighter))
        {
            hexView.LineTransformers.Add(_locationsHighlighter);
        }

        if (!hexView.LineTransformers.Contains(_troopsHighlighter))
        {
            hexView.LineTransformers.Add(_troopsHighlighter);
        }

        if (!hexView.LineTransformers.Contains(_globalsHighlighter))
        {
            hexView.LineTransformers.Add(_globalsHighlighter);
        }

        if (!hexView.LineTransformers.Contains(_compressionHighlighter))
        {
            hexView.LineTransformers.Add(_compressionHighlighter);
        }

        if (!hexView.LineTransformers.Contains(_unknownHighlighter))
        {
            hexView.LineTransformers.Add(_unknownHighlighter);
        }
    }

    private static void ApplyRanges(BitRangeUnion target, IReadOnlyList<ByteRange>? source)
    {
        target.Clear();

        if (source is null)
        {
            return;
        }

        foreach (ByteRange range in source)
        {
            if (range.EndExclusive <= range.Start)
            {
                continue;
            }

            target.Add(new BitRange(range.Start, range.EndExclusive));
        }
    }

    private static Color GetRequiredColor(string resourceKey)
    {
        if (Avalonia.Application.Current is null)
        {
            throw new InvalidOperationException("Avalonia application is not initialized.");
        }

        bool found = Avalonia.Application.Current.TryGetResource(resourceKey, Avalonia.Application.Current.ActualThemeVariant, out object? resource);
        if (!found || resource is not Color color)
        {
            throw new InvalidOperationException($"Required theme color resource '{resourceKey}' was not found.");
        }

        return color;
    }

    private static Color ShiftHue(Color color, double hueOffsetDegrees)
    {
        (double hue, double saturation, double lightness) = ToHsl(color);
        double shiftedHue = (hue + hueOffsetDegrees) % 360.0;
        if (shiftedHue < 0)
        {
            shiftedHue += 360.0;
        }

        double adjustedSaturation = Math.Clamp(saturation * 0.90, 0.0, 1.0);
        double adjustedLightness = Math.Clamp(lightness + (Avalonia.Application.Current?.ActualThemeVariant == ThemeVariant.Dark ? 0.08 : -0.04), 0.0, 1.0);
        return FromHsl(color.A, shiftedHue, adjustedSaturation, adjustedLightness);
    }

    private static (double Hue, double Saturation, double Lightness) ToHsl(Color color)
    {
        double red = color.R / 255.0;
        double green = color.G / 255.0;
        double blue = color.B / 255.0;

        double max = Math.Max(red, Math.Max(green, blue));
        double min = Math.Min(red, Math.Min(green, blue));
        double lightness = (max + min) / 2.0;

        if (Math.Abs(max - min) < 0.00001)
        {
            return (0.0, 0.0, lightness);
        }

        double delta = max - min;
        double saturation = lightness > 0.5
            ? delta / (2.0 - max - min)
            : delta / (max + min);

        double hue;
        if (Math.Abs(max - red) < 0.00001)
        {
            hue = (green - blue) / delta + (green < blue ? 6.0 : 0.0);
        }
        else if (Math.Abs(max - green) < 0.00001)
        {
            hue = (blue - red) / delta + 2.0;
        }
        else
        {
            hue = (red - green) / delta + 4.0;
        }

        return (hue * 60.0, saturation, lightness);
    }

    private static Color FromHsl(byte alpha, double hue, double saturation, double lightness)
    {
        double chroma = (1.0 - Math.Abs((2.0 * lightness) - 1.0)) * saturation;
        double hueSection = hue / 60.0;
        double secondary = chroma * (1.0 - Math.Abs((hueSection % 2.0) - 1.0));

        double redPrime = 0.0;
        double greenPrime = 0.0;
        double bluePrime = 0.0;

        if (hueSection >= 0.0 && hueSection < 1.0)
        {
            redPrime = chroma;
            greenPrime = secondary;
        }
        else if (hueSection < 2.0)
        {
            redPrime = secondary;
            greenPrime = chroma;
        }
        else if (hueSection < 3.0)
        {
            greenPrime = chroma;
            bluePrime = secondary;
        }
        else if (hueSection < 4.0)
        {
            greenPrime = secondary;
            bluePrime = chroma;
        }
        else if (hueSection < 5.0)
        {
            redPrime = secondary;
            bluePrime = chroma;
        }
        else
        {
            redPrime = chroma;
            bluePrime = secondary;
        }

        double match = lightness - (chroma / 2.0);
        byte red = (byte)Math.Round((redPrime + match) * 255.0);
        byte green = (byte)Math.Round((greenPrime + match) * 255.0);
        byte blue = (byte)Math.Round((bluePrime + match) * 255.0);

        return Color.FromArgb(alpha, red, green, blue);
    }

    private void AttachSelectionSyncBehavior()
    {
        HexEditor? hexEditor = this.FindControl<HexEditor>("HexEditorControl");
        TextBox? charisma = this.FindControl<TextBox>("CharismaTextBox");
        TextBox? contactDistance = this.FindControl<TextBox>("ContactDistanceTextBox");
        TextBox? spice = this.FindControl<TextBox>("SpiceTextBox");
        TextBox? gameStage = this.FindControl<TextBox>("GameStageTextBox");
        TextBox? gameStageDescription = this.FindControl<TextBox>("GameStageDescriptionTextBox");

        if (hexEditor is null
            || charisma is null
            || contactDistance is null
            || spice is null
            || gameStage is null
            || gameStageDescription is null)
        {
            return;
        }

        HexFieldSelectionBehavior.SetHexEditor(charisma, hexEditor);
        HexFieldSelectionBehavior.SetByteOffset(charisma, GlobalsViewModel.CharismaOffset);
        HexFieldSelectionBehavior.SetByteLength(charisma, 1);

        HexFieldSelectionBehavior.SetHexEditor(contactDistance, hexEditor);
        HexFieldSelectionBehavior.SetByteOffset(contactDistance, GlobalsViewModel.ContactDistanceOffset);
        HexFieldSelectionBehavior.SetByteLength(contactDistance, 1);

        HexFieldSelectionBehavior.SetHexEditor(spice, hexEditor);
        HexFieldSelectionBehavior.SetByteOffset(spice, GlobalsViewModel.SpiceOffset);
        HexFieldSelectionBehavior.SetByteLength(spice, 2);

        HexFieldSelectionBehavior.SetHexEditor(gameStage, hexEditor);
        HexFieldSelectionBehavior.SetByteOffset(gameStage, GlobalsViewModel.GameStageOffset);
        HexFieldSelectionBehavior.SetByteLength(gameStage, 1);

        HexFieldSelectionBehavior.SetHexEditor(gameStageDescription, hexEditor);
        HexFieldSelectionBehavior.SetByteOffset(gameStageDescription, GlobalsViewModel.GameStageOffset);
        HexFieldSelectionBehavior.SetByteLength(gameStageDescription, 1);
    }

    private void OnHexSelectionChanged(object? sender, HexSelectionChangedEventArgs e)
    {
        HexEditor? currentHexEditor = this.FindControl<HexEditor>("HexEditorControl");
        if (!ReferenceEquals(e.Editor, currentHexEditor))
        {
            return;
        }

        _pendingSelection = e;
        _selectionDebounceTimer.Stop();
        _selectionDebounceTimer.Start();
    }

    private void OnSelectionDebounceTick(object? sender, EventArgs e)
    {
        _selectionDebounceTimer.Stop();

        if (_pendingSelection is null || DataContext is not MainViewModel vm)
        {
            return;
        }

        HexSelectionChangedEventArgs selection = _pendingSelection;
        _pendingSelection = null;

        void ApplySelection() => vm.UpdateSelection(selection.Offset, selection.Length);

        if (Dispatcher.UIThread.CheckAccess())
        {
            ApplySelection();
        }
        else
        {
            Dispatcher.UIThread.Post(ApplySelection);
        }
    }

    private async void OnUploadClick(object? sender, RoutedEventArgs e)
    {
        var topLevel = TopLevel.GetTopLevel(this);
        if (topLevel == null)
            return;

        var storageProvider = topLevel.StorageProvider;
        if (!storageProvider.CanOpen)
            return;

        var files = await storageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Select a file to view",
            AllowMultiple = false
        });

        if (files.Any())
        {
            var file = files[0];
            try
            {
                await using var stream = await file.OpenReadAsync();
                using var ms = new MemoryStream();
                await stream.CopyToAsync(ms);
                var bytes = ms.ToArray();

                if (DataContext is MainViewModel vm)
                {
                    vm.LoadFileFromBytes(bytes, file.Name);
                    RefreshRangeHighlighting();
                }
            }
            catch (Exception ex)
            {
                // Could show error dialog here
                if (DataContext is MainViewModel vm)
                {
                    vm.SetLoadError(ex.Message);
                }
            }
        }
    }

    private void OnSimpleFieldSelectClick(object? sender, RoutedEventArgs e)
    {
        if (sender is not Button { Tag: SimpleFieldValueViewModel field })
        {
            return;
        }

        HexEditor? editor = this.FindControl<HexEditor>("HexEditorControl");
        if (editor is null)
        {
            return;
        }

        ulong length = Math.Max(1UL, field.Length);
        editor.Selection.Range = new BitRange(field.Offset, field.Offset + length);
        editor.Caret.Location = new BitLocation(field.Offset);
        editor.Focus();
        editor.HexView?.BringIntoView(new BitLocation(field.Offset));

        if (DataContext is MainViewModel vm)
        {
            vm.UpdateSelection(field.Offset, length);
        }
    }
}
