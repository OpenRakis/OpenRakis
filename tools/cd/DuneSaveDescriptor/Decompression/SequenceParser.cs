using System.Globalization;

namespace DuneSaveDescriptor.Decompression;

internal static class SequenceParser
{
    public static bool IsControlSequence(byte[] sequence) => sequence[0] == 0xF7 && sequence[1] == 1 && sequence[2] == 0xF7;

    public static bool IsDeflateSequence(byte[] sequence) => sequence[0] == 0xF7 && sequence[1] > 2;

    public static byte[] SplitTwo(string s)
    {
        List<byte> list = new();
        int length = s.Length - 1;
        while (true)
        {
            int count = length;
            const int start = 0;
            if (count >= start)
            {
                byte item = (byte)int.Parse(s.Substring(length > 0 ? length - 1 : length, length > 0 ? 2 : 1), NumberStyles.HexNumber);
                list.Add(item);
                length -= 2;
            }
            else
            {
                break;
            }
        }
        return list.ToArray();
    }
}