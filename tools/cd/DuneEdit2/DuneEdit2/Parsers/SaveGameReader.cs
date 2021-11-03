[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("DuneEdit2.UnitTests")]

namespace DuneEdit2.Parsers
{
    using DuneEdit2.Enums;
    using DuneEdit2.Models;

    internal class SaveGameReader
    {
        private readonly SaveGameFile _savegame;
        private readonly Generals _generals;
        private readonly Dune37Offsets _offsets;
        private readonly string _saveFilePath;

        public string SaveFilePath => _saveFilePath;

        public SaveGameReader(string saveFilePath)
        {
            _savegame = new SaveGameFile(saveFilePath, SaveFileFormat.DUNE_37);
            _generals = new Generals(_savegame.UncompressedData, new Dune37Offsets());
            _offsets = new Dune37Offsets();
            _saveFilePath = saveFilePath;
        }

        public void WriteUncompressedSaveGameInTheSameFolder() => SaveGameFile.SaveUnCompressedAs($"{_saveFilePath}.UNCOMPRESSED", _savegame.UncompressedData);

        public string GetGameStageHexValue() => _generals.GameStageAsHex;

        public string GetGameStageExplained() => _generals.GetGameStageDesc();

        public static string GetGameStagePosition() => $"0x{(new Dune37Offsets() as ISaveGameOffsets).GameStage:X}";

        public byte GetPlayerCharismaForUI() => _generals.CharismaGUI;

        internal int GetPlayerContactDistanceForUI() => _generals.ContactDistance;

        internal string GetPlayerContactDistanceHexValue() => $"0x{_generals.ContactDistance:X}";

        internal static object GetPlayerCharismaPosition() => $"0x{(new Dune37Offsets() as ISaveGameOffsets).Charisma:X}";

        internal string GetPlayerCharismaHexValue() => $"0x{_generals.CharismaByte:X}";

        internal string GetDateHexValue() => $"0x{_generals.DateAsHex}";

        internal static string GetDatePosition() => $"0x{(new Dune37Offsets() as ISaveGameOffsets).DateTime:X}";

        internal static string GetDateForUI() => Generals.DateGUI;

        internal static string GetPlayerContactDistancePosition() => $"0x{(new Dune37Offsets() as ISaveGameOffsets).ContactDistance:X}";

        internal static string GetPlayerSpiceHexPosition() => $"0x{(new Dune37Offsets() as ISaveGameOffsets).Spice:X}";

        internal string GetPlayerSpiceHexValue() => $"0x{_generals.SpiceAsHex}";

        internal int GetPlayerSpiceForUI() => _generals.Spice;
    }
}