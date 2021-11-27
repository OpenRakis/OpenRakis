namespace DuneEdit2.Models
{
    public record Smuggler
    {
        public int StartOffset { get; set; }

        /// <summary>
        /// 1st byte
        /// </summary>
        public byte Region { get; set; }

        /// <summary>
        /// 2nd byte
        /// </summary>
        public byte WillingnessToHaggle { get; set; }

        /// <summary>
        /// 3rd byte - Field C
        /// </summary>
        public byte UnknownByte1 { get; set; }


        /// <summary>
        /// 4th byte - Field D
        /// </summary>
        public byte UnknownByte2 { get; set; }

        /// <summary>
        /// 5th byte
        /// </summary>
        public byte Harvesters { get; set; }

        /// <summary>
        /// 6th byte
        /// </summary>
        public byte Ornithopters { get; set; }

        /// <summary>
        /// 7th byte
        /// </summary>
        public byte KrysKnives { get; set; }

        /// <summary>
        /// 8bth byte
        /// </summary>
        public byte LaserGuns { get; set; }

        /// <summary>
        /// 9th byte
        /// </summary>
        public byte WeirdingModules { get; set; }

        /// <summary>
        /// 10th byte
        /// </summary>
        public byte HarvestersPrice { get; set; }

        /// <summary>
        /// 11th byte
        /// </summary>
        public byte OrnithoptersPrice { get; set; }


        /// <summary>
        /// 12th byte
        /// </summary>
        public byte KrysKnivesPrice { get; set; }

        /// <summary>
        /// 13th byte
        /// </summary>
        public byte LaserGunsPrice { get; set; }

        /// <summary>
        /// 14th byte
        /// </summary>
        public byte WeirdingModulesPrice { get; set; }

    }
}
