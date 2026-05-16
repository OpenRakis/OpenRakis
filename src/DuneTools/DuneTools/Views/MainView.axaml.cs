namespace DuneTools.Views;

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using DuneTools.ViewModels;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
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
