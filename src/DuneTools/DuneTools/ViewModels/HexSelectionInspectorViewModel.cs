namespace DuneTools.ViewModels;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

public sealed class HexSelectionInspectorViewModel : ViewModelBase
{
    private byte[]? _buffer;
    private IReadOnlyDictionary<int, KnownFieldDescriptor> _knownFields = new Dictionary<int, KnownFieldDescriptor>();

    public HexSelectionInfo Info { get; } = new();

    public void SetBuffer(byte[]? buffer, IReadOnlyDictionary<int, KnownFieldDescriptor> knownFields, bool noFileLoaded)
    {
        _buffer = buffer;
        _knownFields = knownFields;

        if (buffer is null || buffer.Length == 0)
        {
            SetPlaceholder(noFileLoaded ? "No file loaded" : "Empty buffer");
            return;
        }

        SetPlaceholder("No selection");
    }

    public void ClearSelection()
    {
        if (_buffer is null)
        {
            SetPlaceholder("No file loaded");
            return;
        }

        if (_buffer.Length == 0)
        {
            SetPlaceholder("Empty buffer");
            return;
        }

        SetPlaceholder("No selection");
    }

    public void UpdateFromSelection(ulong offset, ulong length)
    {
        if (_buffer is null)
        {
            SetPlaceholder("No file loaded");
            return;
        }

        if (_buffer.Length == 0)
        {
            SetPlaceholder("Empty buffer");
            return;
        }

        ulong clampedOffset = Math.Min(offset, (ulong)Math.Max(0, _buffer.Length - 1));
        ulong requestedLength = Math.Max(1UL, length);
        ulong maxLength = (ulong)_buffer.Length - clampedOffset;
        ulong clampedLength = Math.Min(requestedLength, maxLength);
        bool truncated = clampedLength != requestedLength;

        byte[] selectionBytes = _buffer
            .Skip((int)clampedOffset)
            .Take((int)clampedLength)
            .ToArray();

        Info.StatusMessage = null;
        Info.OffsetHex = $"0x{clampedOffset:X}";
        Info.OffsetDecimal = clampedOffset.ToString(CultureInfo.InvariantCulture);
        Info.SelectionLength = clampedLength.ToString(CultureInfo.InvariantCulture);
        Info.IsTruncated = truncated;
        Info.RawHex = string.Join(" ", selectionBytes.Select(static b => b.ToString("X2", CultureInfo.InvariantCulture)));
        Info.RawDecimal = string.Join(" ", selectionBytes.Select(static b => b.ToString(CultureInfo.InvariantCulture)));
        Info.AsciiPreview = string.Concat(selectionBytes.Select(static b => b is >= 0x20 and <= 0x7E ? (char)b : '.'));
        Info.Uint16LittleEndian = clampedLength >= 2
            ? ((selectionBytes[0] | (selectionBytes[1] << 8))).ToString(CultureInfo.InvariantCulture)
            : null;
        Info.Uint32LittleEndian = clampedLength >= 4
            ? ((uint)(selectionBytes[0]
                | (selectionBytes[1] << 8)
                | (selectionBytes[2] << 16)
                | (selectionBytes[3] << 24))).ToString(CultureInfo.InvariantCulture)
            : null;

        List<KnownFieldMatch> matches = FindIntersectingFields(clampedOffset, clampedLength);
        Info.IntersectingFields = matches.Count == 0
            ? null
            : string.Join(Environment.NewLine, matches.Select(static match => match.DisplayText));

        KnownFieldMatch? primary = matches.FirstOrDefault();
        if (primary is null)
        {
            Info.PrimaryFieldName = null;
            Info.PrimaryFieldEncoding = null;
            Info.PrimaryFieldValue = null;
            Info.PrimaryFieldDescription = null;
        }
        else
        {
            Info.PrimaryFieldName = primary.Descriptor.Name;
            Info.PrimaryFieldEncoding = $"{primary.Descriptor.Signedness}, {primary.Descriptor.Endianness}, len={primary.Descriptor.Length}";
            Info.PrimaryFieldValue = primary.DecodedValue;
            Info.PrimaryFieldDescription = primary.Description;
        }
    }

    private List<KnownFieldMatch> FindIntersectingFields(ulong offset, ulong length)
    {
        byte[]? buffer = _buffer;
        if (buffer is null)
        {
            return [];
        }

        ulong end = offset + length;

        return _knownFields.Values
            .Where(descriptor => offset < (ulong)(descriptor.Offset + descriptor.Length) && end > (ulong)descriptor.Offset)
            .OrderBy(descriptor => descriptor.Length)
            .ThenBy(descriptor => descriptor.Offset)
            .ThenBy(descriptor => descriptor.Name, StringComparer.Ordinal)
            .Select(descriptor => DecodeKnownField(buffer, descriptor, offset, end))
            .ToList();
    }

    private static KnownFieldMatch DecodeKnownField(byte[] buffer, KnownFieldDescriptor descriptor, ulong selectionStart, ulong selectionEnd)
    {
        byte[] fieldBytes = buffer
            .Skip(descriptor.Offset)
            .Take(descriptor.Length)
            .ToArray();

        bool partial = selectionStart > (ulong)descriptor.Offset || selectionEnd < (ulong)(descriptor.Offset + descriptor.Length);

        try
        {
            KnownFieldDecodeResult decoded = descriptor.Decoder(fieldBytes);
            string description = decoded.Description ?? string.Empty;
            return new KnownFieldMatch(
                descriptor,
                decoded.Value,
                string.IsNullOrWhiteSpace(description) ? null : description,
                $"{descriptor.Name} @ 0x{descriptor.Offset:X} = {decoded.Value}{(partial ? " [partial]" : string.Empty)}");
        }
        catch
        {
            string raw = string.Join(" ", fieldBytes.Select(static b => b.ToString("X2", CultureInfo.InvariantCulture)));
            return new KnownFieldMatch(
                descriptor,
                raw,
                "Unknown",
                $"{descriptor.Name} @ 0x{descriptor.Offset:X} = {raw}{(partial ? " [partial]" : string.Empty)}");
        }
    }

    private void SetPlaceholder(string message)
    {
        Info.StatusMessage = message;
        Info.OffsetHex = null;
        Info.OffsetDecimal = null;
        Info.SelectionLength = null;
        Info.IsTruncated = false;
        Info.RawHex = null;
        Info.RawDecimal = null;
        Info.AsciiPreview = null;
        Info.Uint16LittleEndian = null;
        Info.Uint32LittleEndian = null;
        Info.IntersectingFields = null;
        Info.PrimaryFieldName = null;
        Info.PrimaryFieldEncoding = null;
        Info.PrimaryFieldValue = null;
        Info.PrimaryFieldDescription = null;
    }

    private sealed record KnownFieldMatch(KnownFieldDescriptor Descriptor, string DecodedValue, string? Description, string DisplayText);
}
