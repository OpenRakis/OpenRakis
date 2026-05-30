namespace DuneTools.ViewModels;

using System.Globalization;

using CommunityToolkit.Mvvm.ComponentModel;

public sealed partial class GeneralsViewModel : ViewModelBase
{
    internal const int CharismaOffset = 17480;
    internal const int ContactDistanceOffset = 21909;
    internal const int SpiceOffset = 17599;
    internal const int GameStageOffset = 17481;

    [ObservableProperty]
    private string _charismaValue = "n/a";

    [ObservableProperty]
    private string _contactDistanceValue = "n/a";

    [ObservableProperty]
    private string _spiceValue = "n/a";

    [ObservableProperty]
    private string _gameStageValue = "n/a";

    [ObservableProperty]
    private string _gameStageDescription = "n/a";

    public void UpdateFromBytes(byte[] bytes)
    {
        if (!HasRequiredBytes(bytes))
        {
            Reset();
            return;
        }

        byte charismaRaw = bytes[CharismaOffset];
        byte contactDistanceRaw = bytes[ContactDistanceOffset];
        byte spiceLowRaw = bytes[SpiceOffset];
        byte spiceHighRaw = bytes[SpiceOffset + 1];
        byte gameStageRaw = bytes[GameStageOffset];

        CharismaValue = $"{DecodeCharisma(charismaRaw)} (raw: {charismaRaw})";
        ContactDistanceValue = $"{DecodeContactDistance(contactDistanceRaw)} (raw: {contactDistanceRaw})";
        SpiceValue = $"{DecodeSpice(spiceLowRaw, spiceHighRaw)} (raw: {spiceHighRaw:X2}{spiceLowRaw:X2})";
        GameStageValue = FormatGameStage(gameStageRaw);
        GameStageDescription = DescribeGameStage(gameStageRaw);
    }

    public void Reset()
    {
        CharismaValue = "n/a";
        ContactDistanceValue = "n/a";
        SpiceValue = "n/a";
        GameStageValue = "n/a";
        GameStageDescription = "n/a";
    }

    internal static int DecodeCharisma(byte raw)
    {
        return raw <= 1 ? 0 : (int)(raw / 2.0);
    }

    internal static int DecodeContactDistance(byte raw)
    {
        return int.Parse(raw.ToString("X", CultureInfo.InvariantCulture), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
    }

    internal static int DecodeSpice(byte lowRaw, byte highRaw)
    {
        return ((highRaw << 8) | lowRaw) * 10;
    }

    internal static string FormatGameStage(byte raw)
    {
        return $"0x{raw:X2} ({raw})";
    }

    internal static string DescribeGameStage(byte id)
    {
        return id switch
        {
            0x00 => "Game start",
            0x01 => "met Gurney, Duncan appears in throne room",
            0x02 => "go find the stillsuit maker",
            0x04 => "find prospectors, visit sietch",
            0x05 => "go back home",
            0x06 => "looking for hidden comms room",
            0x08 => "getting warmer to the hidden comms room",
            0x0C => "found the comms room, go talk to Duncan",
            0x0D => "go find a harvester ?",
            0x10 => "found the harvester in Tuono Harg",
            0x14 => "go into the desert",
            0x18 => "look for Gurney",
            0x2C => "take Stilgar home to meet your folks",
            0x34 => "Leto is about to leave",
            0x35 => "Leto has left",
            0x39 => "Leto has left (why the different value then ?)",
            0x48 => "morning song starts playing",
            0x4F => "can ride worms",
            0x50 => "have ridden a worm, let's tell Thufir",
            0x51 => "look for hidden rooms (greenhouse)",
            0x54 => "show the greenhouse to Chani and Stilgar",
            0x55 => "go meet Liet Kynes",
            0x60 => "go find Chani",
            0x64 => "Chani has been kidnapped",
            0x68 => "Chani is back",
            0xC8 => "ending",
            _ => "Unused / not yet discovered."
        };
    }

    private static bool HasRequiredBytes(byte[] bytes)
    {
        return bytes.Length > ContactDistanceOffset
            && bytes.Length > SpiceOffset + 1
            && bytes.Length > GameStageOffset;
    }
}