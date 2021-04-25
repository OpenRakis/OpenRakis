namespace DuneImpactor
{
    public class DatasSection
    {
        public byte[] NameOfFile;
        public byte[] SizeOfFile;
        public byte[] OffsetOfFile;
        public byte[] Unused;

        public DatasSection()
        {
            this.NameOfFile = new byte[16];
            this.SizeOfFile = new byte[5];
            this.OffsetOfFile = new byte[5];
            this.Unused = new byte[2];
        }
    }
}