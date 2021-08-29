using DuneEdit2.Parsers;

using System;

namespace DuneEdit2.Models
{
    public record Sietch
    {
        private readonly string _regionDesc = "";

        private readonly string _subRegionDesc = "";

        private byte _atomics;

        private ClsBitfield _bitfield;

        private byte _Bulbs;

        private string? _Coordinates;

        private byte _Harvesters;

        private byte _housedTroopID;

        private byte _krys;

        private byte _laserGuns;

        private byte _Orni;

        private byte _region;

        private byte _SpiceDensity;

        private byte _SpicefieldID;

        private int _startOffset;

        private byte _Status;

        private byte _subRegion;

        private byte _Water;

        private byte _weirdingMod;

        public Sietch(int startOffset, byte region, byte subRegion, byte housedTroopID, byte status, byte spicefieldID, byte spiceDensity, byte harvesters, byte ornis, byte krys, byte laserGuns, byte weirdingMods, byte atomics, byte bulbs, byte water)
        {
            _startOffset = startOffset;
            _region = region;
            _subRegion = subRegion;
            _regionDesc = Regions.Region(_region);
            _subRegionDesc = Regions.Subregion(_subRegion);
            _housedTroopID = housedTroopID;
            _Status = status;
            _SpicefieldID = spicefieldID;
            _SpiceDensity = spiceDensity;
            _Harvesters = harvesters;
            _Orni = ornis;
            _krys = krys;
            _laserGuns = laserGuns;
            _weirdingMod = weirdingMods;
            _atomics = atomics;
            _Bulbs = bulbs;
            _Water = water;
            _bitfield = new ClsBitfield(_Status);
        }

        public byte Atomics
        {
            get
            {
                return _atomics;
            }

            set
            {
                _atomics = value;
            }
        }

        public bool BattleWon
        {
            get
            {
                return _bitfield.GetBit(3) != 0;
            }

            set
            {
                if (value)
                {
                    _bitfield.SetBit(3);
                }
            }
        }

        public ClsBitfield BitField
        {
            get
            {
                return _bitfield;
            }

            set
            {
                _bitfield = value;
            }
        }

        public byte Bulbs
        {
            get
            {
                return _Bulbs;
            }

            set
            {
                _Bulbs = value;
            }
        }

        public string? Coordinates
        {
            get
            {
                return _Coordinates;
            }

            set
            {
                _Coordinates = value;
            }
        }

        public bool FremenFound
        {
            get
            {
                return _bitfield.GetBit(2) != 0;
            }

            set
            {
                if (value)
                {
                    _bitfield.SetBit(2);
                }
            }
        }

        public byte Harvesters
        {
            get
            {
                return _Harvesters;
            }

            set
            {
                _Harvesters = value;
            }
        }

        public bool HasVegetation
        {
            get
            {
                return _bitfield.GetBit(0) != 0;
            }

            set
            {
                if (value)
                {
                    _bitfield.SetBit(0);
                }
            }
        }

        public bool HasWindtrap
        {
            get
            {
                return _bitfield.GetBit(5) != 0;
            }

            set
            {
                if (value)
                {
                    _bitfield.SetBit(5);
                }
            }
        }

        public byte HousedTroopID
        {
            get
            {
                return _housedTroopID;
            }

            set
            {
                _housedTroopID = value;
            }
        }

        public string ID => $"{Convert.ToString(_region)},{Convert.ToString(_subRegion)}";

        public bool InBattle
        {
            get
            {
                return _bitfield.GetBit(1) != 0;
            }

            set
            {
                if (value)
                {
                    _bitfield.SetBit(1);
                }
            }
        }

        public byte Krys
        {
            get
            {
                return _krys;
            }

            set
            {
                _krys = value;
            }
        }

        public byte LaserGuns
        {
            get
            {
                return _laserGuns;
            }

            set
            {
                _laserGuns = value;
            }
        }

        public bool NotDiscovered
        {
            get
            {
                return _bitfield.GetBit(7) != 0;
            }

            set
            {
                if (value)
                {
                    _bitfield.SetBit(7);
                }
            }
        }

        public byte Orni
        {
            get
            {
                return _Orni;
            }

            set
            {
                _Orni = value;
            }
        }

        public bool Prospected
        {
            get
            {
                return _bitfield.GetBit(6) != 0;
            }

            set
            {
                if (value)
                {
                    _bitfield.SetBit(6);
                }
            }
        }

        public byte Region
        {
            get
            {
                return _region;
            }

            set
            {
                _region = value;
            }
        }

        public string RegionName => $"{Regions.Region(_region)} - {Regions.Subregion(_subRegion)}";

        public bool SeeInventory
        {
            get
            {
                return _bitfield.GetBit(4) != 0;
            }

            set
            {
                if (value)
                {
                    _bitfield.SetBit(4);
                }
            }
        }

        public byte SpiceDensity
        {
            get
            {
                return _SpiceDensity;
            }

            set
            {
                _SpiceDensity = value;
            }
        }

        public byte SpicefieldID
        {
            get
            {
                return _SpicefieldID;
            }

            set
            {
                _SpicefieldID = value;
            }
        }

        public int StartOffset
        {
            get
            {
                return _startOffset;
            }

            set
            {
                _startOffset = value;
            }
        }

        public byte Status
        {
            get
            {
                return _Status;
            }

            set
            {
                _Status = value;
            }
        }

        public byte SubRegion
        {
            get
            {
                return _subRegion;
            }

            set
            {
                _subRegion = value;
            }
        }

        public byte Water
        {
            get
            {
                return _Water;
            }

            set
            {
                _Water = value;
            }
        }

        public byte WeirdingMod
        {
            get
            {
                return _weirdingMod;
            }

            set
            {
                _weirdingMod = value;
            }
        }
    }
}