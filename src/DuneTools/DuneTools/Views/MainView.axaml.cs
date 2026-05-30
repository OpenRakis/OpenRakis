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
using Avalonia.Threading;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using AvaloniaHex;
using AvaloniaHex.Document;
using AvaloniaHex.Rendering;
using DuneTools.Behaviors;
using DuneTools.ViewModels;

public partial class MainView : UserControl
{
    private readonly DispatcherTimer _selectionDebounceTimer = new() { Interval = TimeSpan.FromMilliseconds(16) };
    private HexSelectionChangedEventArgs? _pendingSelection;
    private readonly RangesHighlighter _knownHighlighter = new();
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

        _knownHighlighter.Background = new SolidColorBrush(knownColor, 0.20);
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
        ApplyRanges(_knownHighlighter.Ranges, _viewModel?.HighlightSnapshot.KnownRanges);
        ApplyRanges(_unknownHighlighter.Ranges, _viewModel?.HighlightSnapshot.UnknownRanges);
    }

    private void AttachHighlighters(HexView hexView)
    {
        if (!hexView.LineTransformers.Contains(_unknownHighlighter))
        {
            hexView.LineTransformers.Add(_unknownHighlighter);
        }

        if (!hexView.LineTransformers.Contains(_knownHighlighter))
        {
            hexView.LineTransformers.Add(_knownHighlighter);
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
}
