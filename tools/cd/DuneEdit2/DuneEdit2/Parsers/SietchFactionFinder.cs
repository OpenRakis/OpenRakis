namespace DuneEdit2.Parsers
{
    internal static class SietchFactionFinder
    {
        // TODO: Verify it more closely...
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
