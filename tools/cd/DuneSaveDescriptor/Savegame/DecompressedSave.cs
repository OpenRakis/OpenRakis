namespace DuneSaveDescriptor.Savegame;

using DuneEdit2.Models;

internal record DecompressedSave(byte[] Data, ISaveGameOffsets Offsets, List<int> ControlSequencesPositions);