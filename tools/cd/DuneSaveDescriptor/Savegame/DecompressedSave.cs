namespace DuneSaveDescriptor.Savegame;

internal record DecompressedSave(byte[] Data, List<int> ControlSequencesPositions);