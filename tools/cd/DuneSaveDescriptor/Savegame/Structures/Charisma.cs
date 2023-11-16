namespace DuneSaveDescriptor.Savegame.Structures;

internal class Charisma : ISavegameStructureDescriptor
{
    private const int Start = 17480;

    private const int End = 17480;

    public KeyValuePair<Range, DescribedSaveStructure> GetDescribedStructure(DecompressedSave decompressedSave)
    {
        byte charisma = decompressedSave.Data[Start];
        return new KeyValuePair<Range, DescribedSaveStructure>(new Range(start: Start, end: End), new DescribedSaveStructure(
            Address: Start,
            Category: "General",
            Name: nameof(Charisma),
            Length: 1,
            Bytes: decompressedSave.Data[Start..End],
            Value: $"{Convert.ToByte(charisma <= 1 ? 0 : charisma / 2.0)}",
            Notes: "Charisma is used to convince Fremen troops to rally the player. Some rare troops have a higher requirement than the vast majority. The unique prospector troop uses the Game Stage instead to decide if they will join you."));
    }
}