using DuneEdit2.Models;

namespace DuneSaveDescriptor.Savegame;

internal static class SaveDescriptor
{
    public static IDictionary<Range, DescribedSaveStructure> GenerateDescription(DecompressedSave decompressedSave)
    {
        var description = new Dictionary<Range, DescribedSaveStructure>();

        foreach(var property in decompressedSave.Offsets.GetType().GetProperties())
        {
            //TODO: Replace ISaveGameOffset with a collection of NamedSaveStructure, and add the Control Sequences structures
            var address = (int)property.GetValue(decompressedSave.Offsets)!;
            string structureName = property.Name;
            var structure = new DescribedSaveStructure(address, structureName, 1, decompressedSave.Data[address..1], "","");
            description.Add(new Range(address, 1), structure);
        }

        return description;
    }
}