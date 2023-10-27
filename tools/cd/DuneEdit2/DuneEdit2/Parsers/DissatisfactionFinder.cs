namespace DuneEdit2.Parsers;

public class DissatisfactionFinder
{
    public const string UnknownValue = "Unknown Dissatisfaction value / Not used.";

    public DissatisfactionFinder()
    {
    }

    public static string GetStatusDesc(byte id)
    {
        string result = UnknownValue;
        switch (id)
        {
            case 1:
                result = "Unknown";
                break;

            case 2:
                result = "Small troop merged with this troop";
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