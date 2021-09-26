namespace DuneEdit2.Parsers
{
    internal static class TroopOriginFinder
    {
        public static string GetOrigin(byte status)
        {
            if(status > 100) { return "Southern Tribe"; }
            return "Northen Tribe";
        }
    }
}
