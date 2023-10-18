using System.Globalization;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace DuneSaveDescriptor.Decompression;

internal static class SequenceParser
{
    public static bool IsControlSequence(byte[] ba)
    {
        bool result = false;
        if ((ba[0x0] == 0xF7) & (ba[0x1] == 0x1) & (ba[0x2] == 0xF7))
        {
            result = true;
        }
        return result;
    }

    public static bool IsDeflateSequence(byte[] ba)
    {
        bool result = false;
        if ((ba[0x0] == 0xF7) & (ba[0x1] > 0x2))
        {
            result = true;
        }
        return result;
    }

    public static byte[] SplitTwo(string s)
    {
        List<byte> list = new();
        checked
        {
            int num = s.Length - 0x1;
            while (true)
            {
                int num2 = num;
                int num3 = 0x0;
                if (num2 < num3)
                {
                    break;
                }
                list.Add((byte)int.Parse(s.Substring(Conversions.ToInteger(Interaction.IIf(num > 0x0, num - 0x1, num)), Conversions.ToInteger(Interaction.IIf(num > 0x0, 0x2, 0x1))), NumberStyles.HexNumber));
                num += -0x2;
            }
            return list.ToArray();
        }
    }
}