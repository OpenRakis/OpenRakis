namespace DuneEdit2.Models
{
    public record NPC
    {
        public int StartOffset { get; set; }

        /// <summary>
        /// 1st byte
        /// </summary>
        public byte SpriteId { get; set; }

        /// <summary>
        /// 2nd byte (Field B)
        /// </summary>
        public byte UnknownByte1 { get; set; }

        /// <summary>
        /// 3rd byte
        /// </summary>
        public byte RoomLocation { get; set; }

        /// <summary>
        /// 4th byte
        /// </summary>
        public byte TypeOfPlace { get; set; }

        /// <summary>
        /// 5th byte (Field E)
        /// </summary>
        public byte DialogueAvailable { get; set; }

        /// <summary>
        /// 6th byte
        /// </summary>
        public byte ExactPlace { get; set; }

        /// <summary>
        /// 7th byte
        /// </summary>
        public byte ForDialogue { get; set; }

        /// <summary>
        /// 8th byte
        /// </summary>
        public byte UnknownByte3 { get; set; }
    }
}
