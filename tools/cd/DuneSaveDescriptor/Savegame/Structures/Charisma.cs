
namespace DuneSaveDescriptor.Savegame.Structures;

internal class Charisma : ISavegameStructureDescriptor
{
    private const int Start = 17480;

    private const int End = 17480;

    public KeyValuePair<Range, DescribedSaveStructure> GetDescribedStructure(DecompressedSave decompressedSave)
    {
        byte charisma = decompressedSave.Data[Start];
        return new KeyValuePair<Range, DescribedSaveStructure>(new Range(Start, End), new DescribedSaveStructure(
            Start, "General", nameof(Charisma), End - Start, decompressedSave.Data[Start..End],
            $"{Convert.ToByte(charisma <= 1 ? 0 : charisma / 2.0)}", "None"
        ));
    }
}