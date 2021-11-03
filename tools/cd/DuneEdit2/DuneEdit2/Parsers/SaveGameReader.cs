[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("DuneEdit2.UnitTests")]

namespace DuneEdit2.Parsers
{
    using DuneEdit2.Enums;
    using DuneEdit2.Models;

    internal class SaveGameReader
    {
        private readonly SaveGameFile _savegame;

        private readonly Generals _generals;
        private readonly string _saveFilePath;

        public string SaveFilePath => _saveFilePath;

        public SaveGameReader(string saveFilePath)
        {
            _savegame = new SaveGameFile(saveFilePath);
            _generals = new Generals(_savegame.UncompressedData);
            _saveFilePath = saveFilePath;
        }

        public void WriteUncompressedSaveGameInTheSameFolder() => SaveGameFile.SaveUnCompressedAs($"{_saveFilePath}.UNCOMPRESSED", _savegame.UncompressedData);

        public string GetGameStageHexValue() => _generals.GameStageAsHex;

        public string GetGameStageExplained() => _generals.GetGameStageDesc();

        public static string GetGameStagePosition() => $"0x{Dune37Offsets.GameStage:X}";

        public byte GetPlayerCharismaForUI() => _generals.CharismaGUI;

        internal int GetPlayerContactDistanceForUI() => _generals.ContactDistance;

        internal string GetPlayerContactDistanceHexValue() => $"0x{_generals.ContactDistance:X}";

        internal static object GetPlayerCharismaPosition() => $"0x{Dune37Offsets.Charisma:X}";

        internal string GetPlayerCharismaHexValue() => $"0x{_generals.CharismaByte:X}";

        internal string GetDateHexValue() => $"0x{_generals.DateAsHex}";

        internal static string GetDatePosition() => $"0x{Dune37Offsets.DateTime:X}";

        internal static string GetDateForUI() => Generals.DateGUI;

        internal static string GetPlayerContactDistancePosition() => $"0x{Dune37Offsets.ContactDistance:X}";

        internal static string GetPlayerSpiceHexPosition() => $"0x{Dune37Offsets.Spice:X}";

        internal string GetPlayerSpiceHexValue() => $"0x{_generals.SpiceAsHex}";

        internal int GetPlayerSpiceForUI() => _generals.Spice;
    }
}