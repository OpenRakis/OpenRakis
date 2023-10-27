using DuneSaveDescriptor.Savegame;
using Csv;

namespace DuneSaveDescriptor.CSV;

using static System.FormattableString;
internal static class SaveFileCsv
{
    private static readonly string[] Headers = "Address, Category, Name, Length, Bytes, Value, Notes".Split(",");
    private const char Comma = ',';

    public static string GenerateLines(IDictionary<Range, DescribedSaveStructure> description)
    {
        return CsvWriter.WriteToText(Headers, GetLines(description.Values));
    }

    private static IEnumerable<string[]> GetLines(ICollection<DescribedSaveStructure> structs)
    {
        foreach (var s in structs.OrderBy(x => x.Address))
        {
            yield return Invariant($"{s.Address}{Comma}{s.Category}{Comma}{s.Name}{Comma}{s.Length}{Comma}{Convert.ToHexString(s.Bytes)}{Comma}{s.Value}{Comma}{s.Notes}").Split(Comma);
        }
    }
}