using DuneEdit2.Models;
using DuneSaveDescriptor.Savegame;

namespace DuneSaveDescriptor.Decompression;

internal static class Decompressor
{
    private static List<Trap> DetectTraps(IReadOnlyList<byte> compressedData)
    {
        int length = compressedData.Count - 4;
        var traps = new List<Trap>();
        for(int i = 0; i <= length; i++)
        {
            byte firstByte = compressedData[i];
            byte repeatByte = compressedData[i + 1];
            byte secondByte = compressedData[i + 2];
            byte thirdByte = compressedData[i + 3];
            if (SequenceParser.IsTrapSequence(new byte[]{firstByte, secondByte, thirdByte }))
            {
                Trap trap = new()
                {
                    Offset = i,
                    Repeat = repeatByte,
                    HexCode = secondByte
                };
                traps.Add(trap);
            }
        }

        return traps;
    }
    
    public static DecompressedSave Decompress(byte[] compressedData, ISaveGameOffsets savegameOffsets)
    {
        var uncompressedData = new List<byte>();
        int length = compressedData.Length - 3;
        var traps = DetectTraps(compressedData);
        var controls = new List<Control>();
        var controlSequencesPositions = new List<int>();
        for (int i = 0; i <= length; i++)
        {
            byte firstByte = compressedData[i];
            byte secondByte = compressedData[i + 1];
            byte thirdByte = compressedData[i + 2];
            byte[] byteArray = new byte[3] { firstByte, secondByte, thirdByte };
            if (SequenceParser.IsControlSequence(byteArray))
            {
                Control control = new(new byte[3] { firstByte, secondByte, thirdByte }, uncompressedData.Count);
                controls.Add(control);
                uncompressedData.Add(SequenceParser.SaveCompressionSequenceStart);
                controlSequencesPositions.Add(i);
                i += 2;
            }
            else
            {
                if (SequenceParser.IsDeflateSequence(byteArray))
                {
                    if (IsTrap(traps, i))
                    {
                        SetRealOffset(traps, i, uncompressedData.Count);
                    }
                    for (int y = 0; y < secondByte; y++)
                    {
                        uncompressedData.Add(thirdByte);
                    }
                    i += 2;
                }
                else
                {
                    uncompressedData.Add(firstByte);
                    if (i == length)
                    {
                        uncompressedData.Add(secondByte);
                        uncompressedData.Add(thirdByte);
                    }
                }
            }
        }
        return new (uncompressedData.ToArray(), savegameOffsets, controlSequencesPositions);
    }

    private static bool IsTrap(IReadOnlyList<Trap> traps, int index) => traps.Any(x => x.Offset == index);

    private static void SetRealOffset(IReadOnlyList<Trap> traps, int index, int value)
    {
        var trapToSet = traps.FirstOrDefault(x => x.Offset == index);
        if(trapToSet is not null) {
            trapToSet.RealOffset = value;
        }
    }
}