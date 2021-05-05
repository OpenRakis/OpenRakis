namespace DuneEdit2.Models
{
    using System;

    public class TroopsItem
    {
        private int _startOffset;

        private byte _troopID;

        private byte _nextTroopInSietch;

        private byte _speech;

        private byte _job;

        private byte _Dissatisfaction;

        private byte _Motivation;

        private byte _SpiceSkill;

        private byte _ArmySkill;

        private byte _EcologySkill;

        private byte _Equipment;

        private byte _Population;

        private string _Coordinates;

        public ClsBitfield Bitfield => new(_Equipment);

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

        public string TroopDesc => $"Troop {_troopID}00";

        public byte TroopID
        {
            get
            {
                return _troopID;
            }
            set
            {
                _troopID = value;
            }
        }

        public byte NextTroopInSietch
        {
            get
            {
                return _nextTroopInSietch;
            }
            set
            {
                _nextTroopInSietch = value;
            }
        }

        public byte Speech
        {
            get
            {
                return _speech;
            }
            set
            {
                _speech = value;
            }
        }

        public byte Job
        {
            get
            {
                return _job;
            }
            set
            {
                _job = value;
            }
        }

        public byte Dissatisfaction
        {
            get
            {
                return _Dissatisfaction;
            }
            set
            {
                _Dissatisfaction = value;
            }
        }

        public byte Motivation
        {
            get
            {
                return _Motivation;
            }
            set
            {
                _Motivation = value;
            }
        }

        public byte SpiceSkill
        {
            get
            {
                return _SpiceSkill;
            }
            set
            {
                _SpiceSkill = value;
            }
        }

        public byte ArmySkill
        {
            get
            {
                return _ArmySkill;
            }
            set
            {
                _ArmySkill = value;
            }
        }

        public byte EcologySkill
        {
            get
            {
                return _EcologySkill;
            }
            set
            {
                _EcologySkill = value;
            }
        }

        public byte Equipment
        {
            get
            {
                return _Equipment;
            }
            set
            {
                _Equipment = value;
            }
        }

        public int Population
        {
            get
            {
                checked
                {
                    return unchecked((int)_Population) * 10;
                }
            }
            set
            {
                _Population = checked((byte)Math.Round((double)value / 10.0));
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

        public bool Bulbs
        {
            get
            {
                ClsBitfield clsBitfield2 = new(_Equipment);
                return clsBitfield2.GetBit(1) != 0;
            }
        }

        public bool Atomics
        {
            get
            {
                ClsBitfield clsBitfield2 = new(_Equipment);
                return clsBitfield2.GetBit(2) != 0;
            }
        }

        public bool Weirdings
        {
            get
            {
                ClsBitfield clsBitfield2 = new(_Equipment);
                return clsBitfield2.GetBit(3) != 0;
            }
        }

        public bool LaserGuns
        {
            get
            {
                ClsBitfield clsBitfield2 = new(_Equipment);
                return clsBitfield2.GetBit(4) != 0;
            }
        }

        public bool KrysKnives
        {
            get
            {
                ClsBitfield clsBitfield2 = new(_Equipment);
                return clsBitfield2.GetBit(5) != 0;
            }
        }

        public bool Ornithopters
        {
            get
            {
                ClsBitfield clsBitfield2 = new(_Equipment);
                return clsBitfield2.GetBit(6) != 0;
            }
        }

        public bool Harvesters
        {
            get
            {
                ClsBitfield clsBitfield2 = new(_Equipment);
                return clsBitfield2.GetBit(7) != 0;
            }
        }

        public TroopsItem()
        {
        }
    }
}