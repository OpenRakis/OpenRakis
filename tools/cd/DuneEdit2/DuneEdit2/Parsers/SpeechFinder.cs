
namespace DuneEdit2.Parsers
{
	public static class SpeechFinder
	{

		public static string GetSpeechDesc(byte id)
		{
			if (id == 1) { return "Unknown"; }
			if (id == 2) { return "Small group merged with this group"; }
			if (id == 4) { return "Troup is Ill"; }
			if (id == 8) { return "Unknown"; }
			if (id == 16) { return "Fortress transformed into a sietch"; }
			if (id == 32) { return "Spice skill showing in troop characteristics"; }
			if (id == 64) { return "Army skill showing in troop characteristics"; }
			if (id == 128) { return "Ecology skill showing in troop characteristics"; }
			return "Unknown / Not used.";
		}
	}
}
