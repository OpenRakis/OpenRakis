namespace DuneSaveDescriptor.Savegame.Structures;

using System;
using System.Collections.Generic;

internal class GameStage : ISavegameStructureDescriptor
{
    public KeyValuePair<Range, DescribedSaveStructure> GetDescribedStructure(DecompressedSave decompressedSave)
    {
        throw new NotImplementedException();
    }
}
