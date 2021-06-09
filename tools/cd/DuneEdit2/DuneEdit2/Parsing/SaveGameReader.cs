[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("DuneEdit2.UnitTests")]

namespace DuneEdit2.Parsing
{
    using DuneEdit2.Models;

    internal class SaveGameReader
    {
        private readonly Savegame _savegame;

        private readonly Generals _generals;
        private readonly string _saveFilePath;

        public string SaveFilePath => _saveFilePath;

        public SaveGameReader(string saveFilePath)
        {
            _savegame = new Savegame(saveFilePath);
            _generals = new Generals(_savegame.Uncompressed);
            _saveFilePath = saveFilePath;
        }

        public void WriteUncompressedSaveGameInTheSameFolder() => Savegame.SaveUnCompressedAs($"{_saveFilePath}.UNCOMPRESSED", _savegame.Uncompressed);

        public string GetGameStageHexValue() => _generals.GameStageAsHex;

        public string GetGameStageExplained() => _generals.GetGameStageExplained();

        public static string GetGameStagePosition() => $"0x{Generals.GameStageOffset:X}";

        public byte GetPlayerCharismaForUI() => _generals.CharismaGUI;

        internal int GetPlayerContactDistanceForUI() => _generals.ContactDistanceGUI;

        internal string GetPlayerContactDistanceHexValue() => $"0x{_generals.ContactDistanceGUI:X}";

        internal static object GetPlayerCharismaPosition() => $"0x{Generals.CharismaStartOffset:X}";

        internal string GetPlayerCharismaHexValue() => $"0x{_generals.Charisma:X}";

        internal string GetDateHexValue() => $"0x{_generals.DateAsHex}";

        internal static string GetDatePosition() => $"0x{Generals.DateTimeStartOffset:X}";

        internal static string GetDateForUI() => Generals.DateGUI;

        internal static string GetPlayerContactDistancePosition() => $"0x{Generals.ContactDistanceStartOffset:X}";

        internal static string GetPlayerSpiceHexPosition() => $"0x{Generals.SpiceStartOffset:X}";

        internal string GetPlayerSpiceHexValue() => $"0x{_generals.SpiceAsHex}";

        internal int GetPlayerSpiceForUI()
        {
            return _generals.SpiceGUI;
        }
    }
}