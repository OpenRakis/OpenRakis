namespace DuneSaveDescriptor.Savegame;

public readonly record struct SaveStructure(int Address, string Name, int Length, byte[] Bytes, string Value, string Notes);