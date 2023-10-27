using DuneSaveDescriptor.Savegame.Structures;

namespace DuneSaveDescriptor.Savegame;

internal static class SaveDescriptor
{
    public static IDictionary<Range, DescribedSaveStructure> GenerateDescription(DecompressedSave decompressedSave)
    {
        var description = new Dictionary<Range, DescribedSaveStructure>();
        var charisma = new Charisma().GetDescribedStructure(decompressedSave);
        description.Add(charisma.Key, charisma.Value);
        return description;
    }
}