namespace DuneSaveDescriptor.Decompression;

internal record ClsBitfield
{
    private int bf;

    public int Bitfield
    {
        get => bf;
        set => bf = value;
    }

    public ClsBitfield(int v = 0x0)
    {
        bf = 0x0;
        bf = v;
    }

    public byte GetBit(byte b = 0x0) => (byte)(0x0u - (((bf & (0x1 << b)) == 0x1 << b ? 0x1 : 0x0) != 0x0 ? 0x1u : 0x0u));

    public void SetBit(int b, bool v = true) => bf = v ? bf | (0x1 << b) : bf & ~(0x1 << b);
}