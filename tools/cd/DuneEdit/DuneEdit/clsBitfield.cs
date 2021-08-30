namespace DuneEdit
{
	public class clsBitfield
	{
		private int bf;

		public int bitfield
        {
            get => bf;
            set => bf = value;
        }

        public clsBitfield(int v = 0)
		{
			bf = 0;
			bf = v;
		}

		public byte getBit(byte b = 0)
		{
			return (byte)(0u - (((((bf & (1 << (int)b)) == 1 << (int)b) ? 1 : 0) != 0) ? 1u : 0u));
		}

		public void setBit(int b, bool v = true)
		{
			bf = (v ? (bf | (1 << b)) : (bf & ~(1 << b)));
		}
	}
}
