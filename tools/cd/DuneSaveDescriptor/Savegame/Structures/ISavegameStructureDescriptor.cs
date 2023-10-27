using DuneSaveDescriptor.Savegame;

internal interface ISavegameStructureDescriptor
{
    public abstract KeyValuePair<Range, DescribedSaveStructure> GetDescribedStructure(DecompressedSave decompressedSave);
}