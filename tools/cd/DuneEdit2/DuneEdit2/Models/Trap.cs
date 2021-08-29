namespace DuneEdit2.Models
{
    public record Trap
    {
        public int Offset;

        public int realOffset;

        public int Repeat;

        public byte HexCode;

        public Trap()
        {
        }

        public void SetRealOffset(int v)
        {
            realOffset = v;
        }
    }
}