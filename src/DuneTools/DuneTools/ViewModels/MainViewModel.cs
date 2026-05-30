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
    [ObservableProperty]
    private IBinaryDocument? _document;

    [ObservableProperty]
    private string _fileName = "No file loaded";

    [ObservableProperty]
    private long _fileSize;

    public GeneralsViewModel Generals { get; } = new();
    public HexSelectionInspectorViewModel Inspector { get; } = new();

    private IReadOnlyDictionary<int, KnownFieldDescriptor> KnownFields => _knownFields;

    private readonly IReadOnlyDictionary<int, KnownFieldDescriptor> _knownFields;

    public MainViewModel()
    {
        _knownFields = CreateKnownFields();
        LoadDefaultResource();
    }

    private void LoadDefaultResource()
    {
        byte[] compressed = LoadResourceBytes(new Uri("avares://DuneTools/DUNE37S1.SAV"));
        ApplyLoadedBytes(Decompress(compressed), "DUNE37S1.SAV (default, decompressed)");
    }

    public void LoadFileFromBytes(byte[] compressed, string name)
    {
        ApplyLoadedBytes(Decompress(compressed), name);
    }

    public void UpdateSelection(ulong offset, ulong length)
    {
        Inspector.UpdateFromSelection(offset, length);
    }

    private void ApplyLoadedBytes(byte[] bytes, string name)
    {
        Document = new MemoryBinaryDocument(bytes);
        FileName = name;
        FileSize = bytes.Length;
        Generals.UpdateFromBytes(bytes);
        Inspector.SetBuffer(bytes, KnownFields, false);
    }

    private static byte[] LoadResourceBytes(Uri resourceUri)
    {
        using var stream = AssetLoader.Open(resourceUri);
        using var memoryStream = new MemoryStream();
        stream.CopyTo(memoryStream);
        return memoryStream.ToArray();
    }

    private IReadOnlyDictionary<int, KnownFieldDescriptor> CreateKnownFields()
    {
        return new Dictionary<int, KnownFieldDescriptor>
        {
            [GeneralsViewModel.CharismaOffset] = new(
                "Charisma",
                GeneralsViewModel.CharismaOffset,
                1,
                "n/a",
                "unsigned byte",
                "raw <= 1 ? 0 : raw / 2.0",
                bytes =>
                {
                    byte raw = bytes[0];
                    int decoded = GeneralsViewModel.DecodeCharisma(raw);
                    return new KnownFieldDecodeResult(decoded.ToString(CultureInfo.InvariantCulture), $"raw: {raw}", null);
                }),
            [GeneralsViewModel.ContactDistanceOffset] = new(
                "Contact Distance",
                GeneralsViewModel.ContactDistanceOffset,
                1,
                "n/a",
                "unsigned byte",
                "parse raw byte as hexadecimal decimal",
                bytes =>
                {
                    byte raw = bytes[0];
                    int decoded = GeneralsViewModel.DecodeContactDistance(raw);
                    return new KnownFieldDecodeResult(decoded.ToString(CultureInfo.InvariantCulture), $"raw: {raw}", null);
                }),
            [GeneralsViewModel.SpiceOffset] = new(
                "Spice",
                GeneralsViewModel.SpiceOffset,
                2,
                "little-endian",
                "unsigned uint16",
                "little-endian uint16 * 10",
                bytes =>
                {
                    int raw = bytes[0] | (bytes[1] << 8);
                    int decoded = GeneralsViewModel.DecodeSpice(bytes[0], bytes[1]);
                    return new KnownFieldDecodeResult(decoded.ToString(CultureInfo.InvariantCulture), $"raw: {raw:X4}", null);
                }),
            [GeneralsViewModel.GameStageOffset] = new(
                "Game Stage",
                GeneralsViewModel.GameStageOffset,
                1,
                "n/a",
                "unsigned byte",
                "lookup game stage description",
                bytes =>
                {
                    byte raw = bytes[0];
                    string description = GeneralsViewModel.DescribeGameStage(raw);
                    if (description == "Unused / not yet discovered.")
                    {
                        description = "Unknown";
                    }

                    return new KnownFieldDecodeResult(GeneralsViewModel.FormatGameStage(raw), $"raw: {raw}", description);
                })
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
