using DuneSaveDescriptor.Savegame;

/// <summary>
/// Gives the basic informations about the strucures in the uncompressed savegame data, but not much else.
/// </summary>
internal static class SavegamePhonebook
{
    public static IDictionary<Range, NamedSaveStructure> GetAllNamedDataStructures(DecompressedSave decompressedSave)
    {
        var basicStructure = new Dictionary<Range, NamedSaveStructure>();
        return basicStructure;
    }
}