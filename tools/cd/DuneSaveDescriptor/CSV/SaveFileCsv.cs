using System.Text;
using DuneEdit2.Models;

namespace DuneSaveDescriptor.CSV;

using static System.FormattableString;
internal static class SaveFileCsv
{
    private const string Headers = "Structure Name, Address, Length, Bytes, Value, Notes";
    private static readonly ISaveGameOffsets _dune37SaveGameOffsets = new Dune37Offsets();
    private const char Comma = ',';

    public static IEnumerable<string> GenerateLines(DecompressedSave uncompressedSave)
    {
        yield return Headers;
        //TODO: Generate a Dictionary where the key is the structure name
        foreach (var s in new (string name, int address)[] { (nameof(_dune37SaveGameOffsets.GameStage), _dune37SaveGameOffsets.GameStage) })
        {
            yield return Invariant($"{s.name}{Comma}{s.address}");
        }
    }
}