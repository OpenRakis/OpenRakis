namespace DuneTools.ViewModels;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;

public sealed partial class SimpleDataTabViewModel : ViewModelBase
{
    private readonly int _baseOffset;
    private readonly int _count;
    private readonly int _stride;
    private readonly IReadOnlyList<SimpleFieldDefinition> _fields;
    private readonly Func<byte[], int, bool>? _recordFilter;
    private readonly string _recordLabelPrefix;
    private readonly List<int> _recordSlots = [];
    private byte[]? _buffer;

    private SimpleDataTabViewModel(
        string header,
        int baseOffset,
        int count,
        int stride,
        IReadOnlyList<SimpleFieldDefinition> fields,
        string noDataMessage,
        Func<byte[], int, bool>? recordFilter = null,
        string recordLabelPrefix = "Record")
    {
        Header = header;
        _baseOffset = baseOffset;
        _count = count;
        _stride = stride;
        _fields = fields;
        _recordFilter = recordFilter;
        _recordLabelPrefix = recordLabelPrefix;
        NoDataMessage = noDataMessage;

        RecordOptions = [];
        Fields = [];
        HasRecords = false;
        SelectedIndex = -1;
        Status = noDataMessage;
    }

    public string Header { get; }
    public string NoDataMessage { get; }
    public ObservableCollection<string> RecordOptions { get; }
    public ObservableCollection<SimpleFieldValueViewModel> Fields { get; }

    [ObservableProperty]
    private bool _hasRecords;

    [ObservableProperty]
    private int _selectedIndex;

    [ObservableProperty]
    private string _status;

    public static SimpleDataTabViewModel CreateNpcs()
    {
        return new SimpleDataTabViewModel(
            "NPCs",
            baseOffset: 21493,
            count: 16,
            stride: 16,
            fields:
            [
                new("Sprite ID", 0, 1),
                new("Room Location", 2, 1),
                new("Type Of Place", 3, 1),
                new("Dialogue Available", 4, 1),
                new("Exact Place", 5, 1),
                new("For Dialogue", 6, 1)
            ],
                noDataMessage: "No data yet.",
                recordLabelPrefix: "NPC");
    }

    public static SimpleDataTabViewModel CreateSmugglers()
    {
        return new SimpleDataTabViewModel(
            "Smugglers",
            baseOffset: 21751,
            count: 6,
            stride: 17,
            fields:
            [
                new("Region", 0, 1),
                new("Willingness To Haggle", 1, 1),
                new("Harvesters", 4, 1),
                new("Ornithopters", 5, 1),
                new("Krys Knives", 6, 1),
                new("Laser Guns", 7, 1),
                new("Weirding Modules", 8, 1),
                new("Harvesters Price", 9, 1),
                new("Ornithopters Price", 10, 1),
                new("Krys Knives Price", 11, 1),
                new("Laser Guns Price", 12, 1),
                new("Weirding Modules Price", 13, 1)
            ],
            noDataMessage: "No data yet.",
            recordLabelPrefix: "Smuggler");
    }

    public static SimpleDataTabViewModel CreateLocations()
    {
        return new SimpleDataTabViewModel(
            "Locations",
            baseOffset: 17695,
            count: 70,
            stride: 28,
            fields:
            [
                SimpleFieldHelpers.ByteField("Region", 0),
                SimpleFieldHelpers.ByteField("SubRegion", 1),
                SimpleFieldHelpers.ByteField("PosX Map", 3),
                SimpleFieldHelpers.ByteField("PosY Map", 4),
                SimpleFieldHelpers.ByteField("PosX", 6),
                SimpleFieldHelpers.ByteField("PosY", 7),
                SimpleFieldHelpers.ByteField("Appearance", 8),
                SimpleFieldHelpers.ByteField("Housed Troop ID", 9),
                new("Status Bitfield", 10, 1, bytes => $"0x{bytes[0]:X2}"),
                SimpleFieldHelpers.BitField("Status Has Vegetation", 10, 0),
                SimpleFieldHelpers.BitField("Status In Battle", 10, 1),
                SimpleFieldHelpers.BitField("Status Infiltrated", 10, 2),
                SimpleFieldHelpers.BitField("Status Battle Won", 10, 3),
                SimpleFieldHelpers.BitField("Status See Inventory", 10, 4),
                SimpleFieldHelpers.BitField("Status Has Windtrap", 10, 5),
                SimpleFieldHelpers.BitField("Status Prospected", 10, 6),
                SimpleFieldHelpers.BitField("Status Not Discovered", 10, 7),
                SimpleFieldHelpers.ByteField("Game Stage", 11),
                SimpleFieldHelpers.ByteField("Spicefield ID", 16),
                SimpleFieldHelpers.ByteField("Spice", 17),
                SimpleFieldHelpers.ByteField("Spice Density", 18),
                SimpleFieldHelpers.ByteField("Harvesters", 20),
                SimpleFieldHelpers.ByteField("Ornithopters", 21),
                SimpleFieldHelpers.ByteField("Krys Knives", 22),
                SimpleFieldHelpers.ByteField("Laser Guns", 23),
                SimpleFieldHelpers.ByteField("Weirding Modules", 24),
                SimpleFieldHelpers.ByteField("Atomics", 25),
                SimpleFieldHelpers.ByteField("Bulbs", 26),
                SimpleFieldHelpers.ByteField("Water", 27)
            ],
            noDataMessage: "No data yet.",
            recordLabelPrefix: "Location");
    }

    public static SimpleDataTabViewModel CreateFremen()
    {
        return new SimpleDataTabViewModel(
            "Fremen",
            baseOffset: 19657,
            count: 68,
            stride: 27,
            fields:
            [
                SimpleFieldHelpers.ByteField("Troop ID", 0),
                SimpleFieldHelpers.ByteField("Next Troop In Location", 1),
                SimpleFieldHelpers.ByteField("Position Around Location", 2),
                SimpleFieldHelpers.ByteField("Job", 3),
                SimpleFieldHelpers.ByteField("Unknown 1", 4),
                SimpleFieldHelpers.ByteField("Unknown 2", 5),
                new("Coordinates", 6, 4, bytes => string.Join(" ", bytes.Select(static b => b.ToString(CultureInfo.InvariantCulture)))),
                SimpleFieldHelpers.ByteField("Unknown 3", 10),
                SimpleFieldHelpers.ByteField("Unknown 4", 11),
                SimpleFieldHelpers.ByteField("Unknown 5", 12),
                SimpleFieldHelpers.ByteField("Unknown 6", 13),
                SimpleFieldHelpers.ByteField("Unknown 7", 14),
                SimpleFieldHelpers.ByteField("Unknown 8", 15),
                SimpleFieldHelpers.ByteField("Espionage Discovery Status", 16),
                SimpleFieldHelpers.ByteField("Equipment Repairing Status", 17),
                SimpleFieldHelpers.ByteField("Dissatisfaction", 18),
                SimpleFieldHelpers.ByteField("Speech", 19),
                SimpleFieldHelpers.ByteField("Miss You Msg", 20),
                SimpleFieldHelpers.ByteField("Motivation", 21),
                SimpleFieldHelpers.ByteField("Spice Skill", 22),
                SimpleFieldHelpers.ByteField("Army Skill", 23),
                SimpleFieldHelpers.ByteField("Ecology Skill", 24),
                new("Equipment Bitfield", 25, 1, bytes => $"0x{bytes[0]:X2}"),
                SimpleFieldHelpers.BitField("Equipment Bulbs", 25, 1),
                SimpleFieldHelpers.BitField("Equipment Atomics", 25, 2),
                SimpleFieldHelpers.BitField("Equipment Weirdings", 25, 3),
                SimpleFieldHelpers.BitField("Equipment Laser Guns", 25, 4),
                SimpleFieldHelpers.BitField("Equipment Krys Knives", 25, 5),
                SimpleFieldHelpers.BitField("Equipment Ornithopters", 25, 6),
                SimpleFieldHelpers.BitField("Equipment Harvesters", 25, 7),
                new("Population", 26, 1, bytes => (bytes[0] * 10).ToString(CultureInfo.InvariantCulture))
            ],
            noDataMessage: "No Fremen troops in this save.",
            recordFilter: static (buffer, troopStart) => IsFremenTroop(buffer[troopStart + 3]),
            recordLabelPrefix: "Troop");
    }

    public static SimpleDataTabViewModel CreateHarkonnen()
    {
        return new SimpleDataTabViewModel(
            "Harkonnen",
            baseOffset: 19657,
            count: 68,
            stride: 27,
            fields:
            [
                SimpleFieldHelpers.ByteField("Troop ID", 0),
                SimpleFieldHelpers.ByteField("Next Troop In Location", 1),
                SimpleFieldHelpers.ByteField("Position Around Location", 2),
                SimpleFieldHelpers.ByteField("Job", 3),
                SimpleFieldHelpers.ByteField("Unknown 1", 4),
                SimpleFieldHelpers.ByteField("Unknown 2", 5),
                new("Coordinates", 6, 4, bytes => string.Join(" ", bytes.Select(static b => b.ToString(CultureInfo.InvariantCulture)))),
                SimpleFieldHelpers.ByteField("Unknown 3", 10),
                SimpleFieldHelpers.ByteField("Unknown 4", 11),
                SimpleFieldHelpers.ByteField("Unknown 5", 12),
                SimpleFieldHelpers.ByteField("Unknown 6", 13),
                SimpleFieldHelpers.ByteField("Unknown 7", 14),
                SimpleFieldHelpers.ByteField("Unknown 8", 15),
                SimpleFieldHelpers.ByteField("Espionage Discovery Status", 16),
                SimpleFieldHelpers.ByteField("Equipment Repairing Status", 17),
                SimpleFieldHelpers.ByteField("Dissatisfaction", 18),
                SimpleFieldHelpers.ByteField("Speech", 19),
                SimpleFieldHelpers.ByteField("Miss You Msg", 20),
                SimpleFieldHelpers.ByteField("Motivation", 21),
                SimpleFieldHelpers.ByteField("Spice Skill", 22),
                SimpleFieldHelpers.ByteField("Army Skill", 23),
                SimpleFieldHelpers.ByteField("Ecology Skill", 24),
                new("Equipment Bitfield", 25, 1, bytes => $"0x{bytes[0]:X2}"),
                SimpleFieldHelpers.BitField("Equipment Bulbs", 25, 1),
                SimpleFieldHelpers.BitField("Equipment Atomics", 25, 2),
                SimpleFieldHelpers.BitField("Equipment Weirdings", 25, 3),
                SimpleFieldHelpers.BitField("Equipment Laser Guns", 25, 4),
                SimpleFieldHelpers.BitField("Equipment Krys Knives", 25, 5),
                SimpleFieldHelpers.BitField("Equipment Ornithopters", 25, 6),
                SimpleFieldHelpers.BitField("Equipment Harvesters", 25, 7),
                new("Population", 26, 1, bytes => (bytes[0] * 10).ToString(CultureInfo.InvariantCulture))
            ],
            noDataMessage: "No Harkonnen troops in this save.",
            recordFilter: static (buffer, troopStart) => !IsFremenTroop(buffer[troopStart + 3]),
            recordLabelPrefix: "Troop");
    }

    public void SetBuffer(byte[]? buffer)
    {
        _buffer = buffer;
        BuildRecordSlots();

        if (!HasRecords)
        {
            Status = NoDataMessage;
            Fields.Clear();
            SelectedIndex = -1;
            return;
        }

        if (buffer is null)
        {
            Status = "No file loaded";
            Fields.Clear();
            return;
        }

        UpdateFields();
    }

    public void Reset()
    {
        _buffer = null;
        _recordSlots.Clear();
        RecordOptions.Clear();
        HasRecords = false;
        SelectedIndex = -1;
        Fields.Clear();
        Status = NoDataMessage;
    }

    public bool TrySelectRecordByOffset(ulong offset)
    {
        if (!HasRecords || _buffer is null)
        {
            return false;
        }

        for (int slotIndex = 0; slotIndex < _recordSlots.Count; slotIndex++)
        {
            int recordIndex = _recordSlots[slotIndex];
            int recordStart = _baseOffset + (recordIndex * _stride);
            foreach (SimpleFieldDefinition field in _fields)
            {
                int start = recordStart + field.RelativeOffset;
                int endExclusive = start + Math.Max(1, field.Length);
                if (start < 0 || endExclusive > _buffer.Length)
                {
                    continue;
                }

                if (offset >= (ulong)start && offset < (ulong)endExclusive)
                {
                    if (SelectedIndex != slotIndex)
                    {
                        SelectedIndex = slotIndex;
                    }

                    return true;
                }
            }
        }

        return false;
    }

    partial void OnSelectedIndexChanged(int value)
    {
        if (_buffer is not null && HasRecords)
        {
            UpdateFields();
        }
    }

    private void UpdateFields()
    {
        Fields.Clear();

        if (_buffer is null || !HasRecords || SelectedIndex < 0 || SelectedIndex >= _recordSlots.Count)
        {
            Status = HasRecords ? "No file loaded" : NoDataMessage;
            return;
        }

        int actualRecordIndex = _recordSlots[SelectedIndex];
        int recordStart = _baseOffset + (actualRecordIndex * _stride);

        foreach (SimpleFieldDefinition field in _fields)
        {
            int start = recordStart + field.RelativeOffset;
            int endExclusive = start + field.Length;

            string value;
            if (start < 0 || endExclusive > _buffer.Length)
            {
                value = "n/a";
            }
            else
            {
                byte[] slice = _buffer.Skip(start).Take(field.Length).ToArray();
                value = field.ValueFormatter is null
                    ? string.Join(" ", slice.Select(static b => b.ToString(CultureInfo.InvariantCulture)))
                    : field.ValueFormatter(slice);
            }

            Fields.Add(new SimpleFieldValueViewModel(field.Name, (ulong)start, (ulong)Math.Max(1, field.Length), value));
        }

        Status = "Ready";
    }

    private void BuildRecordSlots()
    {
        _recordSlots.Clear();
        RecordOptions.Clear();

        if (_count <= 0 || _stride <= 0 || _fields.Count == 0)
        {
            HasRecords = false;
            return;
        }

        if (_buffer is null)
        {
            for (int i = 0; i < _count; i++)
            {
                _recordSlots.Add(i);
                RecordOptions.Add($"{_recordLabelPrefix} {i:D2}");
            }

            HasRecords = _recordSlots.Count > 0;
            if (HasRecords && SelectedIndex < 0)
            {
                SelectedIndex = 0;
            }

            return;
        }

        for (int i = 0; i < _count; i++)
        {
            int recordStart = _baseOffset + (i * _stride);
            int recordEndExclusive = recordStart + _stride;
            if (recordStart < 0 || recordEndExclusive > _buffer.Length)
            {
                continue;
            }

            if (_recordFilter is not null && !_recordFilter(_buffer, recordStart))
            {
                continue;
            }

            _recordSlots.Add(i);
            RecordOptions.Add($"{_recordLabelPrefix} {i:D2}");
        }

        HasRecords = _recordSlots.Count > 0;
        if (!HasRecords)
        {
            SelectedIndex = -1;
        }
        else if (SelectedIndex < 0 || SelectedIndex >= _recordSlots.Count)
        {
            SelectedIndex = 0;
        }
    }

    private static bool IsFremenTroop(byte job)
    {
        if (job > 0xA0)
        {
            return true;
        }

        int computed = job & 0xF;
        return computed < 0xC;
    }
}

public readonly record struct SimpleFieldDefinition(string Name, int RelativeOffset, int Length, Func<byte[], string>? ValueFormatter = null);

internal static class SimpleFieldHelpers
{
    public static SimpleFieldDefinition ByteField(string name, int offset)
    {
        return new SimpleFieldDefinition(name, offset, 1);
    }

    public static SimpleFieldDefinition BitField(string name, int offset, int bit)
    {
        return new SimpleFieldDefinition(
            name,
            offset,
            1,
            bytes => ((bytes[0] & (1 << bit)) != 0).ToString());
    }
}

public sealed class SimpleFieldValueViewModel
{
    public SimpleFieldValueViewModel(string name, ulong offset, ulong length, string value)
    {
        Name = name;
        Offset = offset;
        Length = length;
        Value = value;
    }

    public string Name { get; }
    public ulong Offset { get; }
    public ulong Length { get; }
    public string Value { get; }
}
