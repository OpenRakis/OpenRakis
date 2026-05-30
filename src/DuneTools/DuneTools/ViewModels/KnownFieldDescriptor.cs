namespace DuneTools.ViewModels;

using System;

public sealed record KnownFieldDescriptor(
    string Name,
    int Offset,
    int Length,
    string Endianness,
    string Signedness,
    string DecoderDescription,
    Func<byte[], KnownFieldDecodeResult> Decoder);

public sealed record KnownFieldDecodeResult(string Value, string RawValue, string? Description);