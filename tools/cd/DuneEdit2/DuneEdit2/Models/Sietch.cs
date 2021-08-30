using DuneEdit2.Parsers;

using System;

namespace DuneEdit2.Models
{
    public record Sietch
    {
        private ClsBitfield _statesField;

        public Sietch(int startOffset, byte region, byte subRegion, byte housedTroopID, byte status, byte spicefieldID, byte spiceDensity, byte harvesters, byte ornis, byte krys, byte laserGuns, byte weirdingMods, byte atomics, byte bulbs, byte water)
        {
            StartOffset = startOffset;
            Region = region;
            SubRegion = subRegion;
            RegionDesc = Regions.Region(Region);
            SubRegionDesc = Regions.Subregion(SubRegion);
            HousedTroopID = housedTroopID;
            Status = status;
            SpicefieldID = spicefieldID;
            SpiceDensity = spiceDensity;
            Harvesters = harvesters;
            Orni = ornis;
            Krys = krys;
            LaserGuns = laserGuns;
            WeirdingMod = weirdingMods;
            Atomics = atomics;
            Bulbs = bulbs;
            Water = water;
            _statesField = new ClsBitfield(status);
        }

        public byte Atomics { get; set; }

        public bool BattleWon
        {
            get => _statesField.GetBit(3) != 0;

            set
            {
                if (value)
                {
                    _statesField.SetBit(3);
                }
            }
        }

        public byte Bulbs { get; set; }

        public string? Coordinates { get; set; }

        public bool FremenFound
        {
            get => _statesField.GetBit(2) != 0;

            set
            {
                if (value)
                {
                    _statesField.SetBit(2);
                }
            }
        }

        public byte Harvesters { get; set; }

        public bool HasVegetation
        {
            get => _statesField.GetBit(0) != 0;

            set
            {
                if (value)
                {
                    _statesField.SetBit(0);
                }
            }
        }

        public bool HasWindtrap
        {
            get => _statesField.GetBit(5) != 0;

            set
            {
                if (value)
                {
                    _statesField.SetBit(5);
                }
            }
        }

        public byte HousedTroopID { get; set; }

        public string ID => $"{Convert.ToString(Region)},{Convert.ToString(SubRegion)}";

        public bool InBattle
        {
            get => _statesField.GetBit(1) != 0;

            set
            {
                if (value)
                {
                    _statesField.SetBit(1);
                }
            }
        }

        public byte Krys { get; set; }

        public byte LaserGuns { get; set; }

        public bool NotDiscovered
        {
            get => _statesField.GetBit(7) != 0;

            set
            {
                if (value)
                {
                    _statesField.SetBit(7);
                }
            }
        }

        public byte Orni { get; set; }

        public bool Prospected
        {
            get => _statesField.GetBit(6) != 0;

            set
            {
                if (value)
                {
                    _statesField.SetBit(6);
                }
            }
        }

        public byte Region { get; set; }

        public string RegionName => $"{Regions.Region(Region)} - {Regions.Subregion(SubRegion)}";

        public bool SeeInventory
        {
            get => _statesField.GetBit(4) != 0;

            set
            {
                if (value)
                {
                    _statesField.SetBit(4);
                }
            }
        }

        public byte SpiceDensity { get; set; }

        public byte SpicefieldID { get; set; }

        public int StartOffset { get; set; }

        public byte Status { get; set; }

        public byte SubRegion { get; set; }
        public string RegionDesc { get; private set; }
        public string SubRegionDesc { get; private set; }
        public byte Water { get; set; }

        public byte WeirdingMod { get; set; }
    }
}