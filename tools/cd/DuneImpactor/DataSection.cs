namespace DuneImpactor;

public record DataSection
{
    public byte[] NameOfFile { get; set; }
    public byte[] SizeOfFile { get; set; }
    public byte[] OffsetOfFile { get; set; }
    public byte[] Unused { get; set; }

    public DataSection()
    {
        this.NameOfFile = new byte[16];
        this.SizeOfFile = new byte[5];
        this.OffsetOfFile = new byte[5];
        this.Unused = new byte[2];
    }
}