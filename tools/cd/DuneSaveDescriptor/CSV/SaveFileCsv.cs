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
            yield return Invariant($"{i},{GetBytesCell(uncompressedData[i..])},{GetAsciiCell(uncompressedData[i..])}");
        }
    }
    
    private static string GetBytesCell(byte[] uncompressedData)
    {
        return $"{Convert.ToHexString(uncompressedData[..Math.Min(uncompressedData.Length, BytesColumnLength)])}";
    }
    
    private static string GetAsciiCell(byte[] uncompressedData)
    {
        return $"{Encoding.ASCII.GetString(uncompressedData[..Math.Min(uncompressedData.Length, BytesColumnLength)])}";
    }
}