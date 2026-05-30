namespace DuneTools.Views;

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using AvaloniaHex;
using DuneTools.Behaviors;
using DuneTools.ViewModels;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
        AttachSelectionSyncBehavior();
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
