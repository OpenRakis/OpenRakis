namespace DuneTools.ViewModels;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Avalonia.Platform;
using AvaloniaHex.Document;
using CommunityToolkit.Mvvm.ComponentModel;

public partial class MainViewModel : ViewModelBase
{
    private const int Dune37CharismaOffset = 17480;
    private const int Dune37ContactDistanceOffset = 21909;

    [ObservableProperty]
    private IBinaryDocument? _document;

    [ObservableProperty]
    private string _fileName = "No file loaded";

    [ObservableProperty]
    private long _fileSize;

    [ObservableProperty]
    private string _charismaValue = "n/a";

    [ObservableProperty]
    private string _contactDistanceValue = "n/a";

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
            var compressed = ms.ToArray();
            var bytes = Decompress(compressed);

            Document = new MemoryBinaryDocument(bytes);
            FileName = "DUNE37S1.SAV (default, decompressed)";
            FileSize = bytes.Length;
            UpdateGeneralsValues(bytes);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public void LoadFileFromBytes(byte[] compressed, string name)
    {
        var bytes = Decompress(compressed);
        Document = new MemoryBinaryDocument(bytes);
        FileName = name;
        FileSize = bytes.Length;
        UpdateGeneralsValues(bytes);
    }

    private void UpdateGeneralsValues(byte[] bytes)
    {
        if (bytes.Length <= Dune37ContactDistanceOffset)
        {
            CharismaValue = "n/a";
            ContactDistanceValue = "n/a";
            return;
        }

        byte charismaRaw = bytes[Dune37CharismaOffset];
        byte contactDistanceRaw = bytes[Dune37ContactDistanceOffset];

        int charismaDecoded = charismaRaw <= 1 ? 0 : (int)(charismaRaw / 2.0);
        int contactDistanceDecoded = int.Parse(contactDistanceRaw.ToString("X"), System.Globalization.NumberStyles.HexNumber);

        CharismaValue = $"{charismaDecoded} (raw: {charismaRaw})";
        ContactDistanceValue = $"{contactDistanceDecoded} (raw: {contactDistanceRaw})";
    }

    private static byte[] Decompress(byte[] data)
    {
        var output = new List<byte>();
        int streamLength = data.Length - 3;
        int offset = 0;

        while (offset <= streamLength)
        {
            byte b0 = data[offset];
            byte b1 = data[offset + 1];
            byte b2 = data[offset + 2];

            if (b0 == 0xF7 && b1 == 0x01 && b2 == 0xF7)
            {
                // Control sequence: emit a literal 0xF7
                output.Add(0xF7);
                offset += 3;
            }
            else if (b0 == 0xF7 && b1 > 2)
            {
                // RLE deflate: repeat b2 exactly b1 times
                for (int i = 0; i < b1; i++)
                    output.Add(b2);
                offset += 3;
            }
            else
            {
                // Literal byte
                output.Add(b0);
                if (offset == streamLength)
                {
                    output.Add(b1);
                    output.Add(b2);
                }
                offset++;
            }
        }

        return output.ToArray();
    }
}
