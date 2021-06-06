namespace DuneEdit2.Models
{
    using System;
    using System.Collections.Generic;

    public class TroopsList : List<Troops>
    {
        private const int StartOffSet = 19657;

        private const int CoordinatesOffset = 6;

        private const int TroopLength = 27;

        private const int TroopCount = 67;

        private readonly List<byte> _Data;

        public void Update(int troopID, byte job, byte dissatisfaction, byte motivation, byte spiceSkill, byte armySkill, byte ecologySkill, byte equipment, int population)
        {
            checked
            {
                using Enumerator enumerator = GetEnumerator();
                while (enumerator.MoveNext())
                {
                    Troops current = enumerator.Current;
                    if (current.TroopID == troopID)
                    {
                        Troops troopsItem = current;
                        troopsItem.Job = job;
                        troopsItem.Dissatisfaction = dissatisfaction;
                        troopsItem.Motivation = motivation;
                        troopsItem.SpiceSkill = spiceSkill;
                        troopsItem.ArmySkill = armySkill;
                        troopsItem.EcologySkill = ecologySkill;
                        troopsItem.Equipment = equipment;
                        troopsItem.Population = population;
                        int startOffset = current.StartOffset;
                        _Data[startOffset + 3] = job;
                        _Data[startOffset + 18] = dissatisfaction;
                        _Data[startOffset + 21] = motivation;
                        _Data[startOffset + 22] = spiceSkill;
                        _Data[startOffset + 23] = armySkill;
                        _Data[startOffset + 24] = ecologySkill;
                        _Data[startOffset + 25] = equipment;
                        _Data[startOffset + 26] = (byte)Math.Round((double)current.Population / 10.0);
                        break;
                    }
                }
            }
        }

        public TroopsList(List<byte> Data)
        {
            _Data = Data;
            int num = 0;
            checked
            {
                int num6;
                int num5;
                do
                {
                    int offset = StartOffSet + num * 27;
                    Troops troopsItem = new()
                    {
                        StartOffset = offset,
                        TroopID = Data[offset + 0],
                        NextTroopInSietch = Data[offset + 1],
                        Job = Data[offset + 3],
                        Dissatisfaction = Data[offset + 18],
                        Speech = Data[offset + 19],
                        Motivation = Data[offset + 21],
                        SpiceSkill = Data[offset + 22],
                        ArmySkill = Data[offset + 23],
                        EcologySkill = Data[offset + 24],
                        Equipment = Data[offset + 25],
                        Population = unchecked((int)Data[checked(offset + 26)]) * 10
                    };
                    int num3 = 0;
                    int num4;
                    do
                    {
                        troopsItem.Coordinates += Convert.ToString(Data[offset + 6 + num3]);
                        num3++;
                        num4 = num3;
                        num5 = 3;
                    }
                    while (num4 <= num5);
                    Add(troopsItem);
                    num++;
                    num6 = num;
                    num5 = 66;
                }
                while (num6 <= num5);
            }
        }
    }
}