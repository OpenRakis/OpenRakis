using DuneEdit2.Parsers;

using System;

namespace DuneEdit2.Models
{
    public record Sietch
    {
        public ClsBitfield? StatusBitField { get; set; }

        public byte Atomics { get; set; }

        public bool BattleWon
        {
            get => StatusBitField?.GetBit(3) != 0;

            set => StatusBitField?.SetBit(3, value);
        }

        public byte Bulbs { get; set; }

        public string? Coordinates { get; set; }

        public bool Infiltrated
        {
            get => StatusBitField?.GetBit(2) != 0;

            set => StatusBitField?.SetBit(2, value);
        }

        public byte Harvesters { get; set; }

        public bool HasVegetation
        {
            get => StatusBitField?.GetBit(0) != 0;

            set => StatusBitField?.SetBit(0, value);
        }

        public bool HasWindtrap
        {
            get => StatusBitField?.GetBit(5) != 0;

            set => StatusBitField?.SetBit(5, value);
        }

        public byte HousedTroopID { get; set; }

        public string ID => $"{Convert.ToString(Region)},{Convert.ToString(SubRegion)}";

        public bool InBattle
        {
            get => StatusBitField?.GetBit(1) != 0;

            set => StatusBitField?.SetBit(1, value);
        }

        public byte Krys { get; set; }

        public byte LaserGuns { get; set; }

        public bool NotDiscovered
        {
            get => StatusBitField?.GetBit(7) != 0;

            set => StatusBitField?.SetBit(7, value);
        }

        public byte Ornis { get; set; }

        public bool Prospected
        {
            get => StatusBitField?.GetBit(6) != 0;

            set => StatusBitField?.SetBit(6, value);
        }

        public byte Region { get; set; }

        public string RegionName => $"{Regions.Region(Region)} - {Regions.Subregion(SubRegion)}";

        public bool SeeInventory
        {
            get => StatusBitField?.GetBit(4) != 0;

            set => StatusBitField?.SetBit(4, value);
        }

        public byte SpiceDensity { get; set; }

        public byte SpicefieldID { get; set; }

        public int StartOffset { get; set; }

        public byte Type { get; set; }

        public int Status
        {
            get { if (StatusBitField != null) { return StatusBitField.Bitfield; } else { return 0; } }
            set { if (StatusBitField != null) { StatusBitField.Bitfield = value; } }
        }

        public byte SubRegion { get; set; }
        public string RegionDesc => Regions.Region(Region);
        public string SubRegionDesc => Regions.Subregion(SubRegion);
        public byte Water { get; set; }

        public byte Spice { get; set; }
        public byte WeirdingMod { get; set; }
    }
}