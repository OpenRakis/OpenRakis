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
            if (num3 <= num4)
            {
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
                continue;
            }
            break;
        }

        return traps;
    }
    
    public static byte[] Decompress(byte[] compressedData)
    {
        var uncompressedData = new List<byte>();
        bool result = true;
        checked
        {
            int overallLength = compressedData.Length - 0x3;
            var traps = DetectTraps(compressedData);
            var controls = new List<Control>();
            try
            {
                int length = overallLength;
                int offset = 0x0;
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
                    if (!SequenceParser.IsControlSequence(byteArray))
                    {
                        Trap t = new();
                        bool trap = GetTrap(traps, offset);
                        if (!SequenceParser.IsDeflateSequence(byteArray))
                        {
                            uncompressedData.Add(firstByte);
                            if (offset == overallLength)
                            {
                                uncompressedData.Add(secondByte);
                                uncompressedData.Add(thirdByte);
                            }
                        }
                        else
                        {
                            if (trap)
                            {
                                SetRealOffset(traps, offset, uncompressedData.Count);
                            }
                            int count = 0x1;
                            while (true)
                            {
                                innerLength = secondByte;
                                if (count > innerLength)
                                {
                                    break;
                                }
                                uncompressedData.Add(thirdByte);
                                count++;
                            }
                            offset += 0x2;
                        }
                    }
                    else
                    {
                        Control control = new();
                        control.Offset = uncompressedData.Count;
                        control.ControlType = new byte[0x3] { firstByte, secondByte, thirdByte };
                        controls.Add(control);
                        uncompressedData.Add(0xF7);
                        offset += 0x2;
                    }
                    offset++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetBaseException().Message);
                throw;
            }
            return uncompressedData.ToArray();
        }
    }
    
    private static bool GetTrap(IReadOnlyList<Trap> traps, int index)
    {
        bool result = false;
        for (int i = 0x0; i < traps.Count; i++)
        {
            Trap trap = traps[i];
            if (trap.Offset == index)
            {
                result = true;
                break;
            }
        }
        return result;
    }
    
    
    private static void SetRealOffset(IReadOnlyList<Trap> traps, int index, int value)
    {
        checked
        {
            int length = traps.Count - 0x1;
            int count = 0x0;
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
}