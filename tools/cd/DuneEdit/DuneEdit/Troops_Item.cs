using System;
using System.Diagnostics;

using Microsoft.VisualBasic;

namespace DuneEdit
{
    public class Troops_Item
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

        private clsBitfield bitfield => new clsBitfield(_Equipment);

        public int startOffset
        {
            get => _startOffset;
            set => _startOffset = value;
        }

        public string troopDesc => "Troop " + Strings.Format(_troopID, "00");

        public byte troopID
        {
            get => _troopID;
            set => _troopID = value;
        }

        public byte nextTroopInSietch
        {
            get => _nextTroopInSietch;
            set => _nextTroopInSietch = value;
        }

        public byte speech
        {
            get => _speech;
            set => _speech = value;
        }

        public byte job
        {
            get => _job;
            set => _job = value;
        }

        public byte Dissatisfaction
        {
            get => _Dissatisfaction;
            set => _Dissatisfaction = value;
        }

        public byte Motivation
        {
            get => _Motivation;
            set => _Motivation = value;
        }

        public byte SpiceSkill
        {
            get => _SpiceSkill;
            set => _SpiceSkill = value;
        }

        public byte ArmySkill
        {
            get => _ArmySkill;
            set => _ArmySkill = value;
        }

        public byte EcologySkill
        {
            get => _EcologySkill;
            set => _EcologySkill = value;
        }

        public byte Equipment
        {
            get => _Equipment;
            set => _Equipment = value;
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
            set => _Population = checked((byte)Math.Round((double)value / 10.0));
        }

        public string Coordinates
        {
            get => _Coordinates;
            set => _Coordinates = value;
        }

        public bool Bulbs
        {
            get
            {
                clsBitfield clsBitfield2 = new clsBitfield(_Equipment);
                return clsBitfield2.getBit(1) != 0;
            }
        }

        public bool Atomics
        {
            get
            {
                clsBitfield clsBitfield2 = new clsBitfield(_Equipment);
                return clsBitfield2.getBit(2) != 0;
            }
        }

        public bool Weirdings
        {
            get
            {
                clsBitfield clsBitfield2 = new clsBitfield(_Equipment);
                return clsBitfield2.getBit(3) != 0;
            }
        }

        public bool LaserGuns
        {
            get
            {
                clsBitfield clsBitfield2 = new clsBitfield(_Equipment);
                return clsBitfield2.getBit(4) != 0;
            }
        }

        public bool KrysKnives
        {
            get
            {
                clsBitfield clsBitfield2 = new clsBitfield(_Equipment);
                return clsBitfield2.getBit(5) != 0;
            }
        }

        public bool Ornithopters
        {
            get
            {
                clsBitfield clsBitfield2 = new clsBitfield(_Equipment);
                return clsBitfield2.getBit(6) != 0;
            }
        }

        public bool Harvesters
        {
            get
            {
                clsBitfield clsBitfield2 = new clsBitfield(_Equipment);
                return clsBitfield2.getBit(7) != 0;
            }
        }

        [DebuggerNonUserCode]
        public Troops_Item()
        {
        }
    }
}