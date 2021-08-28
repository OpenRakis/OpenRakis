namespace DuneEdit2.Parsers
{
    public class ClsBitfield
    {
        private int bf;

        public int Bitfield
        {
            get
            {
                return bf;
            }
            set
            {
                bf = value;
            }
        }

        public ClsBitfield(int v = 0)
        {
            bf = 0;
            bf = v;
        }

        public byte GetBit(byte b = 0) => (byte)(0u - (((((bf & (1 << (int)b)) == 1 << (int)b) ? 1 : 0) != 0) ? 1u : 0u));

        public void SetBit(int b, bool v = true) => bf = (v ? (bf | (1 << b)) : (bf & ~(1 << b)));
    }
}