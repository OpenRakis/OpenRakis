namespace DuneEdit2.Parsers
{
    /// <summary>
    /// From ODRADE: https://github.com/debrouxl/odrade
    /// </summary>
    internal static class NPCNameFinder
    {
        private const string UnknownValue = "Unknown/Unused NPC Name byte";

        public static string GetNPCName(int id)
        {
            return id switch
            {
                0x1 => "Duke Leto Atreides",
                0x2 => "Jessica Atreides",
                0x3 => "Thufir Hawat",
                0x4 => "Duncan Idaho",
                0x5 => "Gurney Halleck",
                0x6 => "Stilgar",
                0x7 => "Liet Kynes",
                0x8 => "Chani",
                0x9 => "Harah",
                0x0a => "Baron Vladimir Harkonnen",
                0x0b => "Feyd-Rautha Harkonnen",
                0x0c => "Emperor Shaddam IV",
                0x0d => "Harkonnen Captains",
                0x0e => "Smugglers",
                0x0f => "Fremen Chef",
                0x10 => "Fremen Chef",
                _ => UnknownValue,
            };
        }
    }
}
