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
    private byte[]? _buffer;

    private SimpleDataTabViewModel(string header, int baseOffset, int count, int stride, IReadOnlyList<SimpleFieldDefinition> fields, string noDataMessage)
    {
        Header = header;
        _baseOffset = baseOffset;
        _count = count;
        _stride = stride;
        _fields = fields;
        NoDataMessage = noDataMessage;

        RecordOptions = new ObservableCollection<string>(Enumerable.Range(0, count).Select(static i => i.ToString("D2", CultureInfo.InvariantCulture)));
        Fields = [];
        HasRecords = count > 0;
        SelectedIndex = count > 0 ? 0 : -1;
        Status = count > 0 ? "No file loaded" : noDataMessage;
    }

    public string Header { get; }
    public string NoDataMessage { get; }
    public bool HasRecords { get; }
    public ObservableCollection<string> RecordOptions { get; }
    public ObservableCollection<SimpleFieldValueViewModel> Fields { get; }

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
            noDataMessage: "No data yet.");
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
            noDataMessage: "No data yet.");
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
                new("Region", 0, 1),
                new("SubRegion", 1, 1),
                new("Game Stage", 11, 1),
                new("Spice", 17, 1),
                new("Water", 27, 1)
            ],
            noDataMessage: "No data yet.");
    }

    public static SimpleDataTabViewModel CreateFremen()
    {
        return new SimpleDataTabViewModel("Fremen", 0, 0, 0, [], "No data yet.");
    }

    public static SimpleDataTabViewModel CreateHarkonnen()
    {
        return new SimpleDataTabViewModel("Harkonnen", 0, 0, 0, [], "No data yet.");
    }

    public void SetBuffer(byte[]? buffer)
    {
        _buffer = buffer;

        if (!HasRecords)
        {
            Status = NoDataMessage;
            Fields.Clear();
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
        Fields.Clear();
        Status = HasRecords ? "No file loaded" : NoDataMessage;
    }

    public bool TrySelectRecordByOffset(ulong offset)
    {
        if (!HasRecords || _buffer is null)
        {
            return false;
        }

        for (int recordIndex = 0; recordIndex < _count; recordIndex++)
        {
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
                    if (SelectedIndex != recordIndex)
                    {
                        SelectedIndex = recordIndex;
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

        if (_buffer is null || !HasRecords || SelectedIndex < 0 || SelectedIndex >= _count)
        {
            Status = HasRecords ? "No file loaded" : NoDataMessage;
            return;
        }

        int recordStart = _baseOffset + (SelectedIndex * _stride);

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
                value = string.Join(" ", slice.Select(static b => b.ToString(CultureInfo.InvariantCulture)));
            }

            Fields.Add(new SimpleFieldValueViewModel(field.Name, (ulong)start, (ulong)Math.Max(1, field.Length), value));
        }

        Status = "Ready";
    }
}

public readonly record struct SimpleFieldDefinition(string Name, int RelativeOffset, int Length);

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
