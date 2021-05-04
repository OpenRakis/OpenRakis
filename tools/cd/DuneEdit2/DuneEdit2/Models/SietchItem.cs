using Microsoft.VisualBasic.CompilerServices;

namespace DuneEdit2.Models
{
    public class SietchItem
    {
        private int _startOffset;

        private byte _region;

        private byte _subRegion;

        private readonly string _regionDesc;

        private readonly string _subRegionDesc;

        private ClsBitfield _bitfield;

        private byte _housedTroopID;

        private byte _Status;

        private byte _SpicefieldID;

        private byte _SpiceDensity;

        private byte _Harvesters;

        private byte _Orni;

        private byte _krys;

        private byte _laserGuns;

        private byte _weirdingMod;

        private byte _atomics;

        private byte _Bulbs;

        private byte _Water;

        private string _Coordinates;

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

        public string ID => Conversions.ToString(_region) + "," + Conversions.ToString(_subRegion);

        public string RegionName => Regions.Region(_region) + " - " + Regions.Subregion(_subRegion);

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

        public string Coordinates
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

        public bool HasVegetation
        {
            get
            {
                return _bitfield.getBit(0) != 0;
            }
            set
            {
                if (value)
                {
                    _bitfield.setBit(0);
                }
            }
        }

        public bool InBattle
        {
            get
            {
                return _bitfield.getBit(1) != 0;
            }
            set
            {
                if (value)
                {
                    _bitfield.setBit(1);
                }
            }
        }

        public bool FremenFound
        {
            get
            {
                return _bitfield.getBit(2) != 0;
            }
            set
            {
                if (value)
                {
                    _bitfield.setBit(2);
                }
            }
        }

        public bool BattleWon
        {
            get
            {
                return _bitfield.getBit(3) != 0;
            }
            set
            {
                if (value)
                {
                    _bitfield.setBit(3);
                }
            }
        }

        public bool SeeInventory
        {
            get
            {
                return _bitfield.getBit(4) != 0;
            }
            set
            {
                if (value)
                {
                    _bitfield.setBit(4);
                }
            }
        }

        public bool HasWindtrap
        {
            get
            {
                return _bitfield.getBit(5) != 0;
            }
            set
            {
                if (value)
                {
                    _bitfield.setBit(5);
                }
            }
        }

        public bool Prospected
        {
            get
            {
                return _bitfield.getBit(6) != 0;
            }
            set
            {
                if (value)
                {
                    _bitfield.setBit(6);
                }
            }
        }

        public bool NotDiscovered
        {
            get
            {
                return _bitfield.getBit(7) != 0;
            }
            set
            {
                if (value)
                {
                    _bitfield.setBit(7);
                }
            }
        }

        public SietchItem()
        {
        }

        public SietchItem(int startOffset, byte region, byte subRegion, byte housedTroopID, byte status, byte spicefieldID, byte spiceDensity, byte harvesters, byte ornis, byte krys, byte laserGuns, byte weirdingMods, byte atomics, byte bulbs, byte water)
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
    }
}