namespace DuneEdit2.Models
{
    using DuneEdit2.Parsers;

    public record Troop
    {
        private ClsBitfield _equipment;

        public Troop(byte equipment) => _equipment = new ClsBitfield(equipment);

        public byte ArmySkill { get; set; }

        public bool Atomics
        {
            get => _equipment.GetBit(2) != 0;
            set => _equipment.SetBit(2, value);
        }

        public bool Bulbs
        {
            get => _equipment.GetBit(1) != 0;
            set => _equipment.SetBit(1, value);
        }

        public string? Coordinates { get; set; }

        public byte Dissatisfaction { get; set; }

        public byte EcologySkill { get; set; }

        public byte Equipment { get; set; }

        public bool Harvesters
        {
            get => _equipment.GetBit(7) != 0;
            set => _equipment.SetBit(7, value);
        }

        public byte Job { get; set; }

        public bool KrysKnives
        {
            get => _equipment.GetBit(5) != 0;
            set => _equipment.SetBit(5, value);
        }

        public bool LaserGuns
        {
            get => _equipment.GetBit(4) != 0;
            set => _equipment.SetBit(4, value);
        }

        public byte Motivation { get; set; }

        public byte NextTroopInSietch { get; set; }

        public bool Ornithopters
        {
            get => _equipment.GetBit(6) != 0;
            set => _equipment.SetBit(6, value);
        }

        public int Population { get; set; }

        public byte Speech { get; set; }

        public byte SpiceSkill { get; set; }

        public int StartOffset { get; set; }

        public string TroopDesc => $"Troop {TroopID:D3}";

        public byte TroopID { get; set; }

        public bool Weirdings
        {
            get => _equipment.GetBit(3) != 0;
            set => _equipment.SetBit(3, value);
        }
    }
}