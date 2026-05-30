namespace DuneTools.Views;

using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Threading;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using AvaloniaHex;
using DuneTools.Behaviors;
using DuneTools.ViewModels;

public partial class MainView : UserControl
{
    private readonly DispatcherTimer _selectionDebounceTimer = new() { Interval = TimeSpan.FromMilliseconds(16) };
    private HexSelectionChangedEventArgs? _pendingSelection;

    public MainView()
    {
        InitializeComponent();
        AttachSelectionSyncBehavior();
        HexFieldSelectionBehavior.SelectionChanged += OnHexSelectionChanged;
        DetachedFromVisualTree += OnDetachedFromVisualTree;
        _selectionDebounceTimer.Tick += OnSelectionDebounceTick;
    }

    private void OnDetachedFromVisualTree(object? sender, Avalonia.VisualTreeAttachmentEventArgs e)
    {
        HexFieldSelectionBehavior.SelectionChanged -= OnHexSelectionChanged;
        _selectionDebounceTimer.Tick -= OnSelectionDebounceTick;
        DetachedFromVisualTree -= OnDetachedFromVisualTree;
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
        HexFieldSelectionBehavior.SetByteOffset(charisma, 17480);
        HexFieldSelectionBehavior.SetByteLength(charisma, 1);

        HexFieldSelectionBehavior.SetHexEditor(contactDistance, hexEditor);
        HexFieldSelectionBehavior.SetByteOffset(contactDistance, 21909);
        HexFieldSelectionBehavior.SetByteLength(contactDistance, 1);

        HexFieldSelectionBehavior.SetHexEditor(spice, hexEditor);
        HexFieldSelectionBehavior.SetByteOffset(spice, 17599);
        HexFieldSelectionBehavior.SetByteLength(spice, 2);

        HexFieldSelectionBehavior.SetHexEditor(gameStage, hexEditor);
        HexFieldSelectionBehavior.SetByteOffset(gameStage, 17481);
        HexFieldSelectionBehavior.SetByteLength(gameStage, 1);

        HexFieldSelectionBehavior.SetHexEditor(gameStageDescription, hexEditor);
        HexFieldSelectionBehavior.SetByteOffset(gameStageDescription, 17481);
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
                }
            }
            catch (Exception ex)
            {
                // Could show error dialog here
                if (DataContext is MainViewModel vm)
                {
                    vm.FileName = $"Error: {ex.Message}";
                }
            }
        }
    }
}
