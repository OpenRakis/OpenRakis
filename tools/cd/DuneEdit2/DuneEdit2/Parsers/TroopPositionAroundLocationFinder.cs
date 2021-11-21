namespace DuneEdit2.Parsers
{
    /// <summary>
    /// See: https://sites.google.com/site/duneeditor/savegame-editing#TOC-SIETCHES-CHEATS
    /// </summary>
    internal static class TroopPositionAroundLocationFinder
    {
        private const string UnknownValue = "Unused/Unusual position on top of the location";

        public static string GetDescriptionOfTroopAroundLocation(int id)
        {
            return id switch
            {
                0x01 => "South of the location",
                0x02 => "South East of the location",
                0x03 => "South West of the location",
                0x04 => "East of the location",
                0x05 => "West of the location",
                0x06 => "North East of the location",
                0x07 => "North West of the location",
                0x08 => "North of the location",
                _ => UnknownValue,
            };
        }
    }
}
