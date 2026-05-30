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
    private const int Dune37SpiceOffset = 17599;
    private const int Dune37GameStageOffset = 17481;

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

    [ObservableProperty]
    private string _spiceValue = "n/a";

    [ObservableProperty]
    private string _gameStageValue = "n/a";

    [ObservableProperty]
    private string _gameStageDescription = "n/a";

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
        if (bytes.Length <= Dune37ContactDistanceOffset || bytes.Length <= Dune37SpiceOffset + 1 || bytes.Length <= Dune37GameStageOffset)
        {
            CharismaValue = "n/a";
            ContactDistanceValue = "n/a";
            SpiceValue = "n/a";
            GameStageValue = "n/a";
            GameStageDescription = "n/a";
            return;
        }

        byte charismaRaw = bytes[Dune37CharismaOffset];
        byte contactDistanceRaw = bytes[Dune37ContactDistanceOffset];
        byte spiceLowRaw = bytes[Dune37SpiceOffset];
        byte spiceHighRaw = bytes[Dune37SpiceOffset + 1];
        byte gameStageRaw = bytes[Dune37GameStageOffset];

        int charismaDecoded = charismaRaw <= 1 ? 0 : (int)(charismaRaw / 2.0);
        int contactDistanceDecoded = int.Parse(contactDistanceRaw.ToString("X"), NumberStyles.HexNumber);
        int spiceDecoded = (((spiceHighRaw << 8) | spiceLowRaw) * 10);

        CharismaValue = $"{charismaDecoded} (raw: {charismaRaw})";
        ContactDistanceValue = $"{contactDistanceDecoded} (raw: {contactDistanceRaw})";
        SpiceValue = $"{spiceDecoded} (raw: {spiceHighRaw:X2}{spiceLowRaw:X2})";
        GameStageValue = $"0x{gameStageRaw:X2} ({gameStageRaw})";
        GameStageDescription = GetGameStageDescription(gameStageRaw);
    }

    private static string GetGameStageDescription(byte id)
    {
        return id switch
        {
            0x00 => "Game start",
            0x01 => "met Gurney, Duncan appears in throne room",
            0x02 => "go find the stillsuit maker",
            0x04 => "find prospectors, visit sietch",
            0x05 => "go back home",
            0x06 => "looking for hidden comms room",
            0x08 => "getting warmer to the hidden comms room",
            0x0C => "found the comms room, go talk to Duncan",
            0x0D => "go find a harvester ?",
            0x10 => "found the harvester in Tuono Harg",
            0x14 => "go into the desert",
            0x18 => "look for Gurney",
            0x2C => "take Stilgar home to meet your folks",
            0x34 => "Leto is about to leave",
            0x35 => "Leto has left",
            0x39 => "Leto has left (why the different value then ?)",
            0x48 => "morning song starts playing",
            0x4F => "can ride worms",
            0x50 => "have ridden a worm, let's tell Thufir",
            0x51 => "look for hidden rooms (greenhouse)",
            0x54 => "show the greenhouse to Chani and Stilgar",
            0x55 => "go meet Liet Kynes",
            0x60 => "go find Chani",
            0x64 => "Chani has been kidnapped",
            0x68 => "Chani is back",
            0xC8 => "ending",
            _ => "Unused / not yet discovered."
        };
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
