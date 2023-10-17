namespace DuneSaveDescriptor.Decompression;

public class Control
{
    public byte[] ControlType { get; set; } = Array.Empty<byte>();

    public int Offset { get; set; }
}