
namespace DuneEdit2.Parsers
{
	public static class SpeechFinder
	{

		public static string GetSpeechDesc(byte id)
		{
            string result = "Unknown / Not used.";
            switch (id)
            {
                case 1:
                    result = "Unknown";
                    break;
                case 2:
                    result = "Small group merged with this group";
                    break;
                case 4:
                    result = "Troup is Ill";
                    break;
                case 8:
                    result = "Unknown";
                    break;
                case 16:
                    result = "Fortress transformed into a sietch";
                    break;
                case 32:
                    result = "Spice skill showing in troop characteristics";
                    break;
                case 64:
                    result = "Army skill showing in troop characteristics";
                    break;
                case 128:
                    result = "Ecology skill showing in troop characteristics";
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}
