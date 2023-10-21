using System.Text;

namespace DuneSaveDescriptor.CSV;

using static System.FormattableString;
public static class SaveFileCsv
{
    private const string Headers = "Linear Address,Bytes (16), As ASCII";
    private const int BytesColumnLength = 16;
    public static IEnumerable<string> GenerateLines(byte[] uncompressedData)
    {
        yield return Headers;
        for (int i = 0; i < uncompressedData.Length; i++)
        {
            yield return Invariant($"{i},{GetBytesColumn(uncompressedData[i..])},{GetAsciiColumn(uncompressedData[i..])}");
        }
    }
    
    private static IEnumerable<string> GetBytesColumn(byte[] uncompressedData)
    {
        yield return Invariant($"{Convert.ToHexString(uncompressedData[..Math.Min(uncompressedData.Length, BytesColumnLength)])}");
    }
    
    private static IEnumerable<string> GetAsciiColumn(byte[] uncompressedData)
    {
        yield return Invariant($"{Encoding.ASCII.GetString(uncompressedData[..Math.Min(uncompressedData.Length, BytesColumnLength)])}");
    }
}