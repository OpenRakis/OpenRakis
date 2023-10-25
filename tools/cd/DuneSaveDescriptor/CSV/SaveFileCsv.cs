using DuneEdit2.Models;
using DuneSaveDescriptor.Savegame;

namespace DuneSaveDescriptor.CSV;

using static System.FormattableString;
internal static class SaveFileCsv
{
    private const string Headers = "Address, Name, Length, Bytes, Value, Notes";
    private static readonly ISaveGameOffsets _dune37SaveGameOffsets = new Dune37Offsets();
    private const char Comma = ',';

    public static IEnumerable<string> GenerateLines(IDictionary<Range, DescribedSaveStructure> description)
    {
        yield return Headers;

        //TODO: Generate a Dictionary where the key is the structure address in the decompressed save
        foreach (var s in description.Values.OrderBy(x => x.Address))
        {
            yield return Invariant($"{s.Address}{Comma}{s.Length}{Comma}{Convert.ToHexString(s.Bytes)}{Comma}{s.Value}{Comma}{s.Notes}");
        }
    }
}