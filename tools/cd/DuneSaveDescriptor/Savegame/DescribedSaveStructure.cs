namespace DuneSaveDescriptor.Savegame;

public readonly record struct DescribedSaveStructure(int Address, string Category, string Name, int Length, byte[] Bytes, string Value, string Notes);