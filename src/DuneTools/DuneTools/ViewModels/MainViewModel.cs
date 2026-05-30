namespace DuneTools.ViewModels;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Avalonia.Platform;
using AvaloniaHex.Document;
using CommunityToolkit.Mvvm.ComponentModel;

public partial class MainViewModel : ViewModelBase
{
    private const string UnsupportedVariantMessage = "Unsupported save variant: known-field decoding, coverage, and highlighting are disabled.";
    private const int GlobalsTabIndex = 0;
    private const int NpcsTabIndex = 1;
    private const int SmugglersTabIndex = 2;
    private const int LocationsTabIndex = 3;
    private const int FremenTabIndex = 4;
    private const int HarkonnenTabIndex = 5;

    [ObservableProperty]
    private IBinaryDocument? _document;

    [ObservableProperty]
    private string _fileName = "No file loaded";

    [ObservableProperty]
    private long _fileSize;

    [ObservableProperty]
    private string _statusSelectionOffset = "-";

    [ObservableProperty]
    private string _statusSelectionLength = "-";

    [ObservableProperty]
    private string _statusPrimaryField = "-";

    [ObservableProperty]
    private string _statusKnownState = "N/A";

    [ObservableProperty]
    private string _statusCoveragePercent = "N/A";

    [ObservableProperty]
    private string _statusMessage = "No file loaded";

    [ObservableProperty]
    private int _activeTabIndex = GlobalsTabIndex;

    public GlobalsViewModel Globals { get; } = new();
    public SimpleDataTabViewModel NpcsTab { get; } = SimpleDataTabViewModel.CreateNpcs();
    public SimpleDataTabViewModel SmugglersTab { get; } = SimpleDataTabViewModel.CreateSmugglers();
    public SimpleDataTabViewModel LocationsTab { get; } = SimpleDataTabViewModel.CreateLocations();
    public SimpleDataTabViewModel FremenTab { get; } = SimpleDataTabViewModel.CreateFremen();
    public SimpleDataTabViewModel HarkonnenTab { get; } = SimpleDataTabViewModel.CreateHarkonnen();
    public HexSelectionInspectorViewModel Inspector { get; } = new();
    public HexHighlightSnapshot HighlightSnapshot { get; private set; } = HexHighlightSnapshot.Empty;

    private IReadOnlyList<KnownFieldDescriptor> KnownFields => _activeKnownFields;

    private readonly IReadOnlyList<KnownFieldDescriptor> _knownFields;
    private IReadOnlyList<KnownFieldDescriptor> _activeKnownFields;
    private bool _knownFeaturesEnabled = true;
    private string? _knownFeaturesStatus;

    public MainViewModel()
    {
        _knownFields = KnownFieldCatalogFactory.Build();
        _activeKnownFields = _knownFields;
        TryLoadDefaultResource();
    }

    private void TryLoadDefaultResource()
    {
        try
        {
            byte[] compressed = LoadResourceBytes(new Uri("avares://DuneTools/DUNE37S1.SAV"));
            DecompressionResult result = DecompressWithMetadata(compressed);
            _activeKnownFields = MergeKnownFields(result.CompressionDescriptors);
            ApplyLoadedBytes(result.DecompressedBytes, "DUNE37S1.SAV (default, decompressed)");
        }
        catch (Exception ex)
        {
            SetLoadError($"Failed to load default save: {ex.Message}");
        }
    }

    public void LoadFileFromBytes(byte[] compressed, string name)
    {
        try
        {
            DecompressionResult result = DecompressWithMetadata(compressed);
            bool recognized = IsRecognizedSaveVariantName(name);
            _activeKnownFields = MergeKnownFields(result.CompressionDescriptors);
            ApplyLoadedBytes(result.DecompressedBytes, name, recognized);
        }
        catch (Exception ex)
        {
            SetLoadError($"Failed to load save: {ex.Message}");
        }
    }

    public void UpdateSelection(ulong offset, ulong length)
    {
        Inspector.UpdateFromSelection(offset, length);
        SyncTabsFromOffset(offset);
        UpdateStatusFromInspector();
    }

    private void ApplyLoadedBytes(byte[] bytes, string name)
    {
        ApplyLoadedBytes(bytes, name, true);
    }

    private void ApplyLoadedBytes(byte[] bytes, string name, bool enableKnownFeatures)
    {
        _knownFeaturesEnabled = enableKnownFeatures;
        _knownFeaturesStatus = enableKnownFeatures ? null : UnsupportedVariantMessage;

        Document = new MemoryBinaryDocument(bytes);
        FileName = name;
        FileSize = bytes.Length;
        NpcsTab.SetBuffer(bytes);
        SmugglersTab.SetBuffer(bytes);
        LocationsTab.SetBuffer(bytes);
        FremenTab.SetBuffer(bytes);
        HarkonnenTab.SetBuffer(bytes);
        if (enableKnownFeatures)
        {
            Globals.UpdateFromBytes(bytes);
            Inspector.SetBuffer(bytes, KnownFields, false);
            StatusCoveragePercent = "N/A";
            UpdateHighlightSnapshot(bytes.Length);
        }
        else
        {
            Globals.Reset();
            Inspector.SetBuffer(bytes, [], false);
            StatusCoveragePercent = "N/A";
            HighlightSnapshot = HexHighlightSnapshot.Empty;
            OnPropertyChanged(nameof(HighlightSnapshot));
        }

        StatusMessage = _knownFeaturesStatus ?? "Ready";
        UpdateStatusFromInspector();
    }

    public void SetLoadError(string message)
    {
        Document = null;
        FileName = $"Error: {message}";
        FileSize = 0;
        Globals.Reset();
        NpcsTab.Reset();
        SmugglersTab.Reset();
        LocationsTab.Reset();
        FremenTab.Reset();
        HarkonnenTab.Reset();
        Inspector.SetBuffer(null, KnownFields, true);
        StatusMessage = message;
        StatusSelectionOffset = "-";
        StatusSelectionLength = "-";
        StatusPrimaryField = "-";
        StatusKnownState = "N/A";
        StatusCoveragePercent = "N/A";
        _knownFeaturesEnabled = false;
        _knownFeaturesStatus = message;
        _activeKnownFields = _knownFields;
        HighlightSnapshot = HexHighlightSnapshot.Empty;
        OnPropertyChanged(nameof(HighlightSnapshot));
    }

    private static byte[] LoadResourceBytes(Uri resourceUri)
    {
        using var stream = AssetLoader.Open(resourceUri);
        using var memoryStream = new MemoryStream();
        stream.CopyTo(memoryStream);
        return memoryStream.ToArray();
    }

    private void UpdateStatusFromInspector()
    {
        string? inspectorMessage = Inspector.Info.StatusMessage;
        if (!string.IsNullOrWhiteSpace(inspectorMessage))
        {
            StatusSelectionOffset = "-";
            StatusSelectionLength = "-";
            StatusPrimaryField = "-";
            StatusKnownState = "N/A";
            StatusMessage = inspectorMessage;
            return;
        }

        StatusSelectionOffset = Inspector.Info.OffsetHex ?? "-";
        StatusSelectionLength = Inspector.Info.SelectionLength ?? "-";
        StatusPrimaryField = Inspector.Info.PrimaryFieldName ?? "-";
        StatusKnownState = _knownFeaturesEnabled
            ? (Inspector.Info.PrimaryFieldName is null ? "Unknown" : "Known")
            : "N/A";
        StatusMessage = _knownFeaturesStatus ?? "Ready";
    }

    private void UpdateHighlightSnapshot(int documentLength)
    {
        if (!_knownFeaturesEnabled)
        {
            HighlightSnapshot = HexHighlightSnapshot.Empty;
            StatusCoveragePercent = "N/A";
            OnPropertyChanged(nameof(HighlightSnapshot));
            return;
        }

        if (documentLength <= 0)
        {
            HighlightSnapshot = HexHighlightSnapshot.Empty;
            StatusCoveragePercent = "N/A";
            OnPropertyChanged(nameof(HighlightSnapshot));
            return;
        }

        int globalsClamped;
        int globalsSkipped;
        IReadOnlyList<ByteRange> globalsRanges = BuildKnownRanges(documentLength, static descriptor => descriptor.Name.StartsWith("Globals", StringComparison.Ordinal), out globalsClamped, out globalsSkipped);

        int troopClamped;
        int troopSkipped;
        IReadOnlyList<ByteRange> troopRanges = BuildKnownRanges(documentLength, static descriptor => descriptor.Name.StartsWith("Troop", StringComparison.Ordinal), out troopClamped, out troopSkipped);

        int locationClamped;
        int locationSkipped;
        IReadOnlyList<ByteRange> locationRanges = BuildKnownRanges(documentLength, static descriptor => descriptor.Name.StartsWith("Location", StringComparison.Ordinal), out locationClamped, out locationSkipped);

        int smugglerClamped;
        int smugglerSkipped;
        IReadOnlyList<ByteRange> smugglerRanges = BuildKnownRanges(documentLength, static descriptor => descriptor.Name.StartsWith("Smuggler", StringComparison.Ordinal), out smugglerClamped, out smugglerSkipped);

        int npcClamped;
        int npcSkipped;
        IReadOnlyList<ByteRange> npcRanges = BuildKnownRanges(documentLength, static descriptor => descriptor.Name.StartsWith("NPC", StringComparison.Ordinal), out npcClamped, out npcSkipped);

        int compressionClamped;
        int compressionSkipped;
        IReadOnlyList<ByteRange> compressionRanges = BuildKnownRanges(documentLength, static descriptor => descriptor.Name.StartsWith("Compression", StringComparison.Ordinal), out compressionClamped, out compressionSkipped);

        List<ByteRange> allKnownRanges = [];
        allKnownRanges.AddRange(globalsRanges);
        allKnownRanges.AddRange(troopRanges);
        allKnownRanges.AddRange(locationRanges);
        allKnownRanges.AddRange(smugglerRanges);
        allKnownRanges.AddRange(npcRanges);
        allKnownRanges.AddRange(compressionRanges);

        IReadOnlyList<ByteRange> mergedKnownRanges = MergeRanges(allKnownRanges);
        IReadOnlyList<ByteRange> unknownRanges = BuildUnknownRanges(mergedKnownRanges, (ulong)documentLength);

        HighlightSnapshot = new HexHighlightSnapshot(
            globalsRanges,
            troopRanges,
            locationRanges,
            smugglerRanges,
            npcRanges,
            compressionRanges,
            unknownRanges);

        long knownBytes = mergedKnownRanges.Sum(static range => (long)(range.EndExclusive - range.Start));
        StatusCoveragePercent = $"{(knownBytes * 100.0 / documentLength):0.0}%";
        OnPropertyChanged(nameof(HighlightSnapshot));
    }

    private void SyncTabsFromOffset(ulong offset)
    {
        if (IsGlobalsOffset(offset))
        {
            ActiveTabIndex = GlobalsTabIndex;
            return;
        }

        if (NpcsTab.TrySelectRecordByOffset(offset))
        {
            ActiveTabIndex = NpcsTabIndex;
            return;
        }

        if (SmugglersTab.TrySelectRecordByOffset(offset))
        {
            ActiveTabIndex = SmugglersTabIndex;
            return;
        }

        if (LocationsTab.TrySelectRecordByOffset(offset))
        {
            ActiveTabIndex = LocationsTabIndex;
            return;
        }

        if (FremenTab.TrySelectRecordByOffset(offset))
        {
            ActiveTabIndex = FremenTabIndex;
            return;
        }

        if (HarkonnenTab.TrySelectRecordByOffset(offset))
        {
            ActiveTabIndex = HarkonnenTabIndex;
        }
    }

    private static bool IsGlobalsOffset(ulong offset)
    {
        return offset == (ulong)GlobalsViewModel.CharismaOffset
            || offset == (ulong)GlobalsViewModel.ContactDistanceOffset
            || (offset >= (ulong)GlobalsViewModel.SpiceOffset && offset < (ulong)(GlobalsViewModel.SpiceOffset + 2))
            || offset == (ulong)GlobalsViewModel.GameStageOffset;
    }

    private IReadOnlyList<ByteRange> BuildKnownRanges(int documentLength, Func<KnownFieldDescriptor, bool> predicate, out int clampedCount, out int skippedCount)
    {
        List<ByteRange> ranges = [];
        clampedCount = 0;
        skippedCount = 0;

        foreach (KnownFieldDescriptor descriptor in KnownFields)
        {
            if (!predicate(descriptor))
            {
                continue;
            }

            int rawStart = descriptor.Offset;
            int rawEnd = descriptor.Offset + descriptor.Length;
            int start = Math.Max(0, rawStart);
            int end = Math.Min(documentLength, rawEnd);

            if (start != rawStart || end != rawEnd)
            {
                clampedCount++;
            }

            if (end <= start)
            {
                skippedCount++;
                continue;
            }

            ranges.Add(new ByteRange((ulong)start, (ulong)end));
        }

        return ranges;
    }

    private static IReadOnlyList<ByteRange> MergeRanges(IReadOnlyList<ByteRange> ranges)
    {
        if (ranges.Count == 0)
        {
            return [];
        }

        List<ByteRange> sorted = ranges.OrderBy(static range => range.Start).ThenBy(static range => range.EndExclusive).ToList();
        List<ByteRange> merged = [sorted[0]];

        for (int i = 1; i < sorted.Count; i++)
        {
            ByteRange current = sorted[i];
            ByteRange last = merged[^1];

            if (current.Start <= last.EndExclusive)
            {
                merged[^1] = new ByteRange(last.Start, Math.Max(last.EndExclusive, current.EndExclusive));
            }
            else
            {
                merged.Add(current);
            }
        }

        return merged;
    }

    private static IReadOnlyList<ByteRange> BuildUnknownRanges(IReadOnlyList<ByteRange> knownRanges, ulong totalLength)
    {
        if (totalLength == 0)
        {
            return [];
        }

        if (knownRanges.Count == 0)
        {
            return [new ByteRange(0, totalLength)];
        }

        List<ByteRange> unknown = [];
        ulong cursor = 0;
        foreach (ByteRange known in knownRanges)
        {
            if (cursor < known.Start)
            {
                unknown.Add(new ByteRange(cursor, known.Start));
            }

            cursor = Math.Max(cursor, known.EndExclusive);
        }

        if (cursor < totalLength)
        {
            unknown.Add(new ByteRange(cursor, totalLength));
        }

        return unknown;
    }

    private static DecompressionResult DecompressWithMetadata(byte[] data)
    {
        var output = new List<byte>();
        List<KnownFieldDescriptor> compressionDescriptors = [];
        int streamLength = data.Length - 3;
        int offset = 0;
        int segmentIndex = 0;

        while (offset <= streamLength)
        {
            byte b0 = data[offset];
            byte b1 = data[offset + 1];
            byte b2 = data[offset + 2];
            int outputStart = output.Count;

            if (b0 == 0xF7 && b1 == 0x01 && b2 == 0xF7)
            {
                // Control sequence: emit a literal 0xF7
                output.Add(0xF7);
                compressionDescriptors.Add(CreateCompressionDescriptor(
                    segmentIndex++,
                    outputStart,
                    1,
                    offset,
                    b0,
                    b1,
                    b2,
                    "Escape literal marker",
                    "Control sequence F7 01 F7 emits a literal F7 byte."));
                offset += 3;
            }
            else if (b0 == 0xF7 && b1 > 2)
            {
                // RLE deflate: repeat b2 exactly b1 times
                for (int i = 0; i < b1; i++)
                {
                    output.Add(b2);
                }

                compressionDescriptors.Add(CreateCompressionDescriptor(
                    segmentIndex++,
                    outputStart,
                    b1,
                    offset,
                    b0,
                    b1,
                    b2,
                    $"RLE repeat x{b1}",
                    $"Control sequence F7 {b1:X2} {b2:X2} emits {b1} copies of {b2:X2}."));
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

        return new DecompressionResult(output.ToArray(), compressionDescriptors);
    }

    private static KnownFieldDescriptor CreateCompressionDescriptor(
        int segmentIndex,
        int outputOffset,
        int outputLength,
        int streamOffset,
        byte control0,
        byte control1,
        byte control2,
        string decodedValue,
        string description)
    {
        string name = $"Compression Segment {segmentIndex:D4}";
        string controlRaw = $"F7 stream @0x{streamOffset:X}: {control0:X2} {control1:X2} {control2:X2}";

        return new KnownFieldDescriptor(
            name,
            outputOffset,
            outputLength,
            "n/a",
            "generated bytes",
            description,
            _ => new KnownFieldDecodeResult(decodedValue, controlRaw, description));
    }

    private IReadOnlyList<KnownFieldDescriptor> MergeKnownFields(IReadOnlyList<KnownFieldDescriptor> compressionDescriptors)
    {
        if (compressionDescriptors.Count == 0)
        {
            return _knownFields;
        }

        return _knownFields.Concat(compressionDescriptors).ToList();
    }

    private static bool IsRecognizedSaveVariantName(string name)
    {
        string upper = name.ToUpperInvariant();
        return upper.Contains("DUNE21", StringComparison.Ordinal)
            || upper.Contains("DUNE23", StringComparison.Ordinal)
            || upper.Contains("DUNE24", StringComparison.Ordinal)
            || upper.Contains("DUNE37", StringComparison.Ordinal)
            || upper.Contains("DUNE38", StringComparison.Ordinal);
    }
}

public readonly record struct ByteRange(ulong Start, ulong EndExclusive);

public sealed record HexHighlightSnapshot(
    IReadOnlyList<ByteRange> GlobalsRanges,
    IReadOnlyList<ByteRange> TroopRanges,
    IReadOnlyList<ByteRange> LocationRanges,
    IReadOnlyList<ByteRange> SmugglerRanges,
    IReadOnlyList<ByteRange> NpcRanges,
    IReadOnlyList<ByteRange> CompressionRanges,
    IReadOnlyList<ByteRange> UnknownRanges)
{
    public static HexHighlightSnapshot Empty { get; } = new([], [], [], [], [], [], []);
}

public sealed record DecompressionResult(byte[] DecompressedBytes, IReadOnlyList<KnownFieldDescriptor> CompressionDescriptors);
