using DuneEdit2.Models;

namespace DuneSaveDescriptor.Savegame;

internal static class SaveDescriptor
{
    public static IDictionary<Range, SaveStructure> GenerateDescription(DecompressedSave decompressedSave)
    {
        var description = new Dictionary<Range, SaveStructure>();

        foreach(var property in decompressedSave.Offsets.GetType().GetProperties())
        {
            var address = (int)property.GetValue(decompressedSave.Offsets)!;
            string structureName = property.Name;
            var structure = new SaveStructure(address, structureName, 1, decompressedSave.Data[address..1], "","");
            description.Add(new Range(address, 1), structure);
        }

        return description;
    }
}