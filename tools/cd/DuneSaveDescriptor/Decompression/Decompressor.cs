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
            if (firstByte == 0xF7 && secondByte == thirdByte)
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
    
    public static byte[] Decompress(byte[] compressedData)
    {
        var uncompressedData = new List<byte>();
        int length = compressedData.Length - 3;
        var traps = DetectTraps(compressedData);
        var controls = new List<Control>();
        int offset = 0;
        while (true)
        {
            int innerLength = length;
            if (offset <= innerLength)
            {
                byte firstByte = compressedData[offset];
                byte secondByte = compressedData[offset + 1];
                byte thirdByte = compressedData[offset + 2];
                byte[] byteArray = new byte[3] { firstByte, secondByte, thirdByte };
                if (SequenceParser.IsControlSequence(byteArray))
                {
                    Control control = new(new byte[3] { firstByte, secondByte, thirdByte }, uncompressedData.Count);
                    controls.Add(control);
                    uncompressedData.Add(0xF7);
                    offset += 2;
                }
                else
                {
                    if (SequenceParser.IsDeflateSequence(byteArray))
                    {
                        if (IsTrap(traps, offset))
                        {
                            SetRealOffset(traps, offset, uncompressedData.Count);
                        }
                        for(int y = 0; y < secondByte; y++)
                        {
                            uncompressedData.Add(thirdByte);
                        }
                        offset += 2;
                    }
                    else
                    {
                        uncompressedData.Add(firstByte);
                        if (offset == length)
                        {
                            uncompressedData.Add(secondByte);
                            uncompressedData.Add(thirdByte);
                        }
                    }
                }
                offset++;
            }
            else
            {
                break;
            }
        }
        return uncompressedData.ToArray();
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