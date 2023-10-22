using System.Text;

namespace DuneSaveDescriptor.CSV;

using static System.FormattableString;
internal static class SaveFileCsv
{
    private const string Headers = "Linear Address,Bytes (16), As ASCII";
    private const int BytesColumnLength = 16;

    private const char CsvSeparator = ',';

    public static IEnumerable<string> GenerateLines(DecompressedSave uncompressedSave)
    {
        yield return Headers;
        // TOOD: Display each new high level data start bytes on a new line, so the struct name sequence and other columns cells
        // only contain one value on each line.
        // Else, the CSV will not be readable.
        for (int i = 0; i < uncompressedSave.DecompressedData.Length; i += BytesColumnLength)
        {
            byte[] dataPart = uncompressedSave.DecompressedData[i..];
            yield return Invariant($"{i}{CsvSeparator}{GetBytesCell(dataPart)}{CsvSeparator}{GetAsciiCell(dataPart)}");
        }
    }
    
    private static string GetBytesCell(byte[] uncompressedData)
    {
        return Convert.ToHexString(uncompressedData[..Math.Min(uncompressedData.Length, BytesColumnLength)]);
    }
    
    /// <summary>
    /// TODO: Remove this column, and display high level data instead
    /// </summary>
    /// <param name="uncompressedData"></param>
    /// <returns></returns>
    private static string GetAsciiCell(byte[] uncompressedData)
    {
        var str = Encoding.ASCII.GetString(uncompressedData[..Math.Min(uncompressedData.Length, BytesColumnLength)]);
        var displayableStr = new StringBuilder();
        foreach(var character in str)
        {
            if(char.IsControl(character))
            {
                displayableStr.Append('.');
            }
            else
            {
                displayableStr.Append(character);
            }
        }
        return displayableStr.ToString();
    }
}