namespace DuneEdit2.Models
{
    using DuneEdit2.Parsers;

    public record Troop
    {
        public ClsBitfield EquipmentBitField { get; set; }

        public Troop(byte equipment)
        {
            EquipmentBitField = new ClsBitfield(equipment);
        }

        public byte ArmySkill { get; set; }

        public bool Atomics
        {
            get => EquipmentBitField.GetBit(2) != 0;
            set => EquipmentBitField.SetBit(2, value);
        }

        public bool Bulbs
        {
            get => EquipmentBitField.GetBit(1) != 0;
            set => EquipmentBitField.SetBit(1, value);
        }

        public string? Coordinates { get; set; }

        public byte Dissatisfaction { get; set; }

        public byte EcologySkill { get; set; }

        public int Equipment
        {
            get => EquipmentBitField.Bitfield;
            set => EquipmentBitField.Bitfield = value;
        }

        public bool Harvesters
        {
            get => EquipmentBitField.GetBit(7) != 0;
            set => EquipmentBitField.SetBit(7, value);
        }

        public byte Job { get; set; }

        public bool KrysKnives
        {
            get => EquipmentBitField.GetBit(5) != 0;
            set => EquipmentBitField.SetBit(5, value);
        }

        public bool LaserGuns
        {
            get => EquipmentBitField.GetBit(4) != 0;
            set => EquipmentBitField.SetBit(4, value);
        }

        public byte Motivation { get; set; }

        public byte NextTroopInLocation { get; set; }

        public bool Ornithopters
        {
            get => EquipmentBitField.GetBit(6) != 0;
            set => EquipmentBitField.SetBit(6, value);
        }

        public int Population { get; set; }

        public byte Speech { get; set; }

        public byte SpiceSkill { get; set; }

        public int StartOffset { get; set; }

        public string TroopDesc => $"Troop {TroopID:D3}";

        public byte TroopID { get; set; }

        public bool Weirdings
        {
            get => EquipmentBitField.GetBit(3) != 0;
            set => EquipmentBitField.SetBit(3, value);
        }
    }
}