using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace DuneEdit
{
    [StandardModule]
    internal sealed class ThreadingModule
    {
        private delegate void SetProgressBarValue_CallBack(ref ProgressBar pb, int newValue);

        public enum SavegameOffsets
        {
            sietchStartOffset = 6038,
            sietchLength = 28
        }

        public enum SietchStatus
        {
            Visible = 0,
            Vegetation = 1,
            InBattle = 2,
            Unknown1 = 4,
            Unknown2 = 8,
            SeeInventoryOfSietch = 0x10,
            WindTrapConstructed = 0x20,
            AreaProspected = 0x40,
            NotDiscovered = 0x80,
            NotDiscoveredWithWindtrap = 160,
            HiddenBeforeStillSuitMission = 224
        }

        public enum SietchProperties
        {
            Region = 0,
            SubRegion = 1,
            HousedTroop = 9,
            Status = 10,
            SpiceFieldID = 0x10,
            SpiceDensity = 18,
            Harvesters = 20,
            Ornis = 21,
            Krys = 22,
            LaserGuns = 23,
            WeirdingModules = 24,
            Atomics = 25,
            Bulbs = 26,
            Water = 27
        }

        public enum TroopProperties
        {
            TroopID = 0,
            NextTroopID = 1,
            Position = 2,
            Job = 3,
            Dissatisfaction = 18,
            Speech = 19,
            MissYouMsg = 20,
            Motivation = 21,
            SpiceSkill = 22,
            ArmySkill = 23,
            EcologySkill = 24,
            Equipment = 25,
            Population = 26
        }

        public static void _Value(ref ProgressBar pb, int newValue)
        {
            if (pb.InvokeRequired)
            {
                SetProgressBarValue_CallBack method = _Value;
                pb.Invoke(method, pb, newValue);
            }
            else
            {
                pb.Value = newValue;
            }
        }

        public static bool IsControlSequence(ref byte[] ba)
        {
            bool result = false;
            if ((ba[0] == 247) & (ba[1] == 1) & (ba[2] == 247))
            {
                result = true;
            }
            return result;
        }

        public static bool isDeflateSequence(ref byte[] ba)
        {
            bool result = false;
            if ((ba[0] == 247) & (ba[1] > 2))
            {
                result = true;
            }
            return result;
        }

        public static byte[] SplitTwo(ref string s)
        {
            List<byte> list = new List<byte>();
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