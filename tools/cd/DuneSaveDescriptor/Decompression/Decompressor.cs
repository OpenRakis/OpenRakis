namespace DuneSaveDescriptor.Decompression;

internal static class Decompressor
{
    private static List<Trap> DetectTraps(IReadOnlyList<byte> compressedData)
    {
        int length = compressedData.Count - 0x4;
        int count = 0x0;
        var traps = new List<Trap>();
        while (true)
        {
            int num3 = count;
            int num4 = length;
            if (num3 > num4) break;
            byte firstByte = compressedData[count];
            byte repeatByte = compressedData[count + 0x1];
            byte secondByte = compressedData[count + 0x2];
            byte thirdByte = compressedData[count + 0x3];
            if (firstByte == 0xF7 && secondByte == thirdByte)
            {
                Trap trap = new()
                {
                    Offset = count,
                    Repeat = repeatByte,
                    HexCode = secondByte
                };
                traps.Add(trap);
            }
            count++;
        }

        return traps;
    }
    
    public static byte[] Decompress(byte[] compressedData)
    {
        var uncompressedData = new List<byte>();
        int overallLength = compressedData.Length - 0x3;
        var traps = DetectTraps(compressedData);
        var controls = new List<Control>();
        int length = overallLength;
        int offset = 0;
        while (true)
        {
            int innerLength = length;
            if (offset > innerLength)
            {
                break;
            }
            byte firstByte = compressedData[offset];
            byte secondByte = compressedData[offset + 0x1];
            byte thirdByte = compressedData[offset + 0x2];
            byte[] byteArray = new byte[0x3] { firstByte, secondByte, thirdByte };
            if (SequenceParser.IsControlSequence(byteArray))
            {
                Control control = new(new byte[0x3] { firstByte, secondByte, thirdByte }, uncompressedData.Count);
                controls.Add(control);
                uncompressedData.Add(0xF7);
                offset += 0x2;
            }
            else
            {
                bool isTrap = IsTrap(traps, offset);
                if (SequenceParser.IsDeflateSequence(byteArray))
                {
                    if (isTrap)
                    {
                        SetRealOffset(traps, offset, uncompressedData.Count);
                    }

                    int count = 0x1;
                    while (true)
                    {
                        innerLength = secondByte;
                        if (count <= innerLength)
                        {
                            uncompressedData.Add(thirdByte);
                            count++;
                        }
                        else
                        {
                            break;
                        }
                    }

                    offset += 0x2;
                }
                else
                {
                    uncompressedData.Add(firstByte);
                    if (offset == overallLength)
                    {
                        uncompressedData.Add(secondByte);
                        uncompressedData.Add(thirdByte);
                    }
                }
            }

            offset++;
        }
        return uncompressedData.ToArray();
    }
    
    private static bool IsTrap(IReadOnlyList<Trap> traps, int index)
    {
        for (int i = 0; i < traps.Count; i++)
        {
            Trap trap = traps[i];
            if (trap.Offset == index)
            {
                return true;
            }
        }
        return false;
    }
    
    private static void SetRealOffset(IReadOnlyList<Trap> traps, int index, int value)
    {
        int length = traps.Count - 1;
        int count = 0;
        while (true)
        {
            if (count <= length)
            {
                if (traps[count].Offset == index)
                {
                    traps[count].RealOffset = value;
                    break;
                }
                count++;
                continue;
            }
            break;
        }
    }
}