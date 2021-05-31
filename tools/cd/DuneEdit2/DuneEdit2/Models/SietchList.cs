namespace DuneEdit2.Models
{
    using System;
    using System.Collections.Generic;

    public class SietchList : List<Sietch>
    {
        private const int StartOffSet = 17695;

        private const int SietchLength = 28;

        private const int SietchCount = 70;

        private const int CoordinatesOffset = 2;

        private readonly List<byte> _Data;

        public void Update(byte region, byte subRegion, byte status, byte spiceDensity, byte harvesters, byte ornis, byte krys, byte laserGuns, byte weirdings, byte atoms, byte bulbs, byte water)
        {
            checked
            {
                using Enumerator enumerator = GetEnumerator();
                while (enumerator.MoveNext())
                {
                    Sietch current = enumerator.Current;
                    if ((current.Region == region) & (current.SubRegion == subRegion))
                    {
                        Sietch sietch_Item = current;
                        sietch_Item.Status = status;
                        sietch_Item.SpiceDensity = spiceDensity;
                        sietch_Item.Harvesters = harvesters;
                        sietch_Item.Orni = ornis;
                        sietch_Item.Krys = krys;
                        sietch_Item.LaserGuns = laserGuns;
                        sietch_Item.WeirdingMod = weirdings;
                        sietch_Item.Atomics = atoms;
                        sietch_Item.Bulbs = bulbs;
                        sietch_Item.Water = water;
                        int startOffset = current.StartOffset;
                        _Data[startOffset + 10] = status;
                        _Data[startOffset + 18] = spiceDensity;
                        _Data[startOffset + 20] = harvesters;
                        _Data[startOffset + 21] = ornis;
                        _Data[startOffset + 22] = krys;
                        _Data[startOffset + 23] = laserGuns;
                        _Data[startOffset + 24] = weirdings;
                        _Data[startOffset + 25] = atoms;
                        _Data[startOffset + 26] = bulbs;
                        _Data[startOffset + 27] = water;
                        break;
                    }
                }
            }
        }

        public SietchList(List<byte> data)
        {
            _Data = data;
            int num = 0;
            checked
            {
                int num6;
                int num5;
                do
                {
                    int num2 = 17695 + num * 28;
                    Sietch sietch_Item = new(
                        num2,
                        data[num2 + 0],
                        data[num2 + 1],
                        data[num2 + 9],
                        data[num2 + 10],
                        data[num2 + 16],
                        data[num2 + 18],
                        data[num2 + 20],
                        data[num2 + 21],
                        data[num2 + 22],
                        data[num2 + 23],
                        data[num2 + 24],
                        data[num2 + 25],
                        data[num2 + 26],
                        data[num2 + 27]);
                    int num3 = 0;
                    int num4;
                    do
                    {
                        sietch_Item.Coordinates += Convert.ToString(data[num2 + 2 + num3]);
                        num3++;
                        num4 = num3;
                        num5 = 3;
                    }
                    while (num4 <= num5);
                    Add(sietch_Item);
                    num++;
                    num6 = num;
                    num5 = 69;
                }
                while (num6 <= num5);
            }
        }
    }
}