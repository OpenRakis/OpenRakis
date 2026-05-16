namespace DuneTools.ViewModels;

using System;
using System.IO;
using Avalonia.Platform;
using AvaloniaHex.Document;
using CommunityToolkit.Mvvm.ComponentModel;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private IBinaryDocument? _document;

    [ObservableProperty]
    private string _fileName = "No file loaded";

    [ObservableProperty]
    private long _fileSize;

    public MainViewModel()
    {
        LoadDefaultResource();
    }

    private void LoadDefaultResource()
    {
        try
        {
            var defaultFileUri = new Uri("avares://DuneTools/DUNE37S1.SAV");
            using var stream = AssetLoader.Open(defaultFileUri);
            using var ms = new MemoryStream();
            stream.CopyTo(ms);
            var bytes = ms.ToArray();

            Document = new MemoryBinaryDocument(bytes);
            FileName = "DUNE37S1.SAV (default)";
            FileSize = bytes.Length;
        }
        catch (Exception ex)
        {
            FileName = $"Error loading default file: {ex.Message}";
        }
    }

    public void LoadFileFromBytes(byte[] bytes, string name)
    {
        Document = new MemoryBinaryDocument(bytes);
        FileName = name;
        FileSize = bytes.Length;
    }
}
