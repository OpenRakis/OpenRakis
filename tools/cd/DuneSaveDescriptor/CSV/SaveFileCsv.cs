using DuneEdit2.Models;
using DuneSaveDescriptor.Savegame;
using Csv;

namespace DuneSaveDescriptor.CSV;

using static System.FormattableString;
internal static class SaveFileCsv
{
    private static readonly string[] Headers = "Address, Category, Name, Length, Bytes, Value, Notes".Split(",");
    private static readonly ISaveGameOffsets _dune37SaveGameOffsets = new Dune37Offsets();
    private const char Comma = ',';

    public static string GenerateLines(IDictionary<Range, DescribedSaveStructure> description)
    {
        return Csv.CsvWriter.WriteToText(Headers, GetLine(description.Values));
    }

    private static IEnumerable<string[]> GetLine(ICollection<DescribedSaveStructure> structs)
    {
        foreach (var s in structs.OrderBy(x => x.Address))
        {
            yield return Invariant($"{s.Address}{Comma}{s.Category}{Comma}{s.Length}{Comma}{Convert.ToHexString(s.Bytes)}{Comma}{s.Value}{Comma}{s.Notes}").Split(Comma);
        }
    }
}