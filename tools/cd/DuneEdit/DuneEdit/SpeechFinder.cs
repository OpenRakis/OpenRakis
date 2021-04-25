using System.Diagnostics;

namespace DuneEdit
{
	public class SpeechFinder
	{
		[DebuggerNonUserCode]
		public SpeechFinder()
		{
		}

		public static string Speech(byte id)
		{
			string result = string.Empty;
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
			}
			return result;
		}
	}
}
