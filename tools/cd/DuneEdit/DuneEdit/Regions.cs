using System.Diagnostics;

namespace DuneEdit
{
	public class Regions
	{
		[DebuggerNonUserCode]
		public Regions()
		{
		}

		public static string region(byte id)
		{
			string result = string.Empty;
			switch (id)
			{
			case 1:
				result = "Arrakeen";
				break;
			case 2:
				result = "Carthag";
				break;
			case 3:
				result = "Tuono";
				break;
			case 4:
				result = "Habbanya";
				break;
			case 5:
				result = "Oxtyn";
				break;
			case 6:
				result = "Tsympo";
				break;
			case 7:
				result = "Bledan";
				break;
			case 8:
				result = "Ergsun";
				break;
			case 9:
				result = "Haga";
				break;
			case 10:
				result = "Cielago";
				break;
			case 11:
				result = "Sihaya";
				break;
			case 12:
				result = "Celimyn";
				break;
			}
			return result;
		}

		public static string subregion(byte id)
		{
			string result = string.Empty;
			switch (id)
			{
			case 1:
				result = "Atreides Palace";
				break;
			case 2:
				result = "Harkonnen Palace";
				break;
			case 3:
				result = "Tabr";
				break;
			case 4:
				result = "Timin";
				break;
			case 5:
				result = "Tuek";
				break;
			case 6:
				result = "Harg";
				break;
			case 7:
				result = "Clam";
				break;
			case 8:
				result = "Tsymyn";
				break;
			case 9:
				result = "Siet";
				break;
			case 10:
				result = "Pyons";
				break;
			case 11:
				result = "Pyort";
				break;
			}
			return result;
		}
	}
}
