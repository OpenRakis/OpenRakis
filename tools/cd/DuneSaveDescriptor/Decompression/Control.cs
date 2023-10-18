namespace DuneSaveDescriptor.Decompression;

internal record Control
{
    public byte[] ControlType { get; set; } = Array.Empty<byte>();

    public int Offset { get; set; }
}