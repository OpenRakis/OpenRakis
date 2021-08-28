namespace DuneEdit2.Parsers
{
    using Microsoft.VisualBasic;
    using Microsoft.VisualBasic.CompilerServices;

    using System.Collections.Generic;
    using System.Globalization;

    internal sealed partial class SequenceParser
    {
        public static bool IsControlSequence( byte[] ba)
        {
            bool result = false;
            if ((ba[0] == 247) & (ba[1] == 1) & (ba[2] == 247))
            {
                result = true;
            }
            return result;
        }

        public static bool IsDeflateSequence( byte[] ba)
        {
            bool result = false;
            if ((ba[0] == 247) & (ba[1] > 2))
            {
                result = true;
            }
            return result;
        }

        public static byte[] SplitTwo( string s)
        {
            List<byte> list = new();
            checked
            {
                int num = s.Length - 1;
                while (true)
                {
                    int num2 = num;
                    int num3 = 0;
                    if (num2 < num3)
                    {
                        break;
                    }
                    list.Add((byte)int.Parse(s.Substring(Conversions.ToInteger(Interaction.IIf(num > 0, num - 1, num)), Conversions.ToInteger(Interaction.IIf(num > 0, 2, 1))), NumberStyles.HexNumber));
                    num += -2;
                }
                return list.ToArray();
            }
        }
    }
}