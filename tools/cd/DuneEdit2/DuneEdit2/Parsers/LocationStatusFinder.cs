namespace DuneEdit2.Parsers
{
    internal static class LocationStatusFinder
    {
        public static string GetSietchStatusDescription(int id)
        {
            if (id == 224) return "Hidden before Stillsuit Mission";
            return "";
        }
    }
}
