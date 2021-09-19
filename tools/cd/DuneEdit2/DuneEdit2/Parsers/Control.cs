namespace DuneEdit2.Parsers
{
    using System;

    public class Control
    {
        public byte[] ControlType { get; set; } = Array.Empty<byte>();

        public int Offset { get; set; }
    }
}