namespace DuneEdit2.Models.Enums
{
    internal sealed partial class SequenceParser
    {
        public enum SietchStatusEnum
        {
            Visible = 0,
            Vegetation = 1,
            InBattle = 2,
            Unknown1 = 4,
            Unknown2 = 8,
            SeeInventoryOfSietch = 0x10,
            WindTrapConstructed = 0x20,
            AreaProspected = 0x40,
            NotDiscovered = 0x80,
            NotDiscoveredWithWindtrap = 160,
            HiddenBeforeStillSuitMission = 224
        }
    }
}