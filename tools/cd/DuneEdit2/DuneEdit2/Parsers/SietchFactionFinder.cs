namespace DuneEdit2.Parsers
{
    internal static class SietchFactionFinder
    {
        public static string GetFaction(int value)
        {
            if (value > 100)
            {
                return "Harkonnen";
            }
            return "Fremen";
        }
    }
}
