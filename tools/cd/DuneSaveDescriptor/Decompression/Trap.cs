namespace DuneSaveDescriptor.Decompression;

internal record Trap
{
    public int Offset { get; init; }

    public int RealOffset { get; set; }

    public int Repeat { get; init; }

    public byte HexCode { get; init; }
}