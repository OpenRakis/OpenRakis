using DuneSaveDescriptor.Savegame;

internal interface ISavegameStructureDescriptor
{
    KeyValuePair<Range, DescribedSaveStructure> GetDescribedStructure(DecompressedSave decompressedSave);
}