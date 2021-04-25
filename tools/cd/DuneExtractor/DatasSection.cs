namespace DuneExtractor
{
    public class DatasSection
    {
        public string NameOfFile;
        public int SizeOfFile;
        public int OffsetOfFile;

        public DatasSection()
        {
            this.NameOfFile = "";
            this.SizeOfFile = 0;
            this.OffsetOfFile = 0;
        }
    }
}