namespace DuneEdit2.Models;

public record Trap
{
    public int Offset { get; set; }

    public int RealOffset { get; set; }

    public int Repeat { get; set; }

    public byte HexCode { get; set; }
}