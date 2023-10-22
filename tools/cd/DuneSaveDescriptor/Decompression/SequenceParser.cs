using System.Globalization;

namespace DuneSaveDescriptor.Decompression;

internal static class SequenceParser
{
    public static bool IsControlSequence(byte[] ba)
    {
        bool result = false;
        if ((ba[0x0] == 0xF7) && (ba[0x1] == 0x1) && (ba[0x2] == 0xF7))
        {
            result = true;
        }
        return result;
    }

    public static bool IsDeflateSequence(byte[] ba)
    {
        bool result = false;
        if ((ba[0x0] == 0xF7) && (ba[0x1] > 0x2))
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
                const int num3 = 0x0;
                if (num2 < num3)
                {
                    break;
                }

                byte item = (byte)int.Parse(s.Substring(Convert.ToInt32(num > 0x0 ? num - 0x1 : num), Convert.ToInt32(num > 0x0 ? 0x2 : 0x1)), NumberStyles.HexNumber);
                list.Add(item);
                num += -0x2;
            }
            return list.ToArray();
        }
    }
}