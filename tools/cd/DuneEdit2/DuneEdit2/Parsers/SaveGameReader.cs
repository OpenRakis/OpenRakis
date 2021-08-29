﻿[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("DuneEdit2.UnitTests")]

namespace DuneEdit2.Parsers
{
    using DuneEdit2.Enums;
    using DuneEdit2.Models;

    internal class SaveGameReader
    {
        private readonly SaveGame _savegame;

        private readonly Generals _generals;
        private readonly string _saveFilePath;

        public string SaveFilePath => _saveFilePath;

        public SaveGameReader(string saveFilePath)
        {
            _savegame = new SaveGame(saveFilePath);
            _generals = new Generals(_savegame.Uncompressed);
            _saveFilePath = saveFilePath;
        }

        public void WriteUncompressedSaveGameInTheSameFolder() => SaveGame.SaveUnCompressedAs($"{_saveFilePath}.UNCOMPRESSED", _savegame.Uncompressed);

        public string GetGameStageHexValue() => _generals.GameStageAsHex;

        public string GetGameStageExplained() => _generals.GameStage();

        public static string GetGameStagePosition() => $"0x{SaveGameIndex.GetFieldStartPos(FieldName.GameStage):X}";

        public byte GetPlayerCharismaForUI() => _generals.CharismaGUI;

        internal int GetPlayerContactDistanceForUI() => _generals.ContactDistance;

        internal string GetPlayerContactDistanceHexValue() => $"0x{_generals.ContactDistance:X}";

        internal static object GetPlayerCharismaPosition() => $"0x{SaveGameIndex.GetFieldStartPos(FieldName.Charisma):X}";

        internal string GetPlayerCharismaHexValue() => $"0x{_generals.CharismaByte:X}";

        internal string GetDateHexValue() => $"0x{_generals.DateAsHex}";

        internal static string GetDatePosition() => $"0x{SaveGameIndex.GetFieldStartPos(FieldName.DateTime):X}";

        internal static string GetDateForUI() => Generals.DateGUI;

        internal static string GetPlayerContactDistancePosition() => $"0x{SaveGameIndex.GetFieldStartPos(FieldName.ContactDistance):X}";

        internal static string GetPlayerSpiceHexPosition() => $"0x{SaveGameIndex.GetFieldStartPos(FieldName.Spice):X}";

        internal string GetPlayerSpiceHexValue() => $"0x{_generals.SpiceAsHex}";

        internal int GetPlayerSpiceForUI() => _generals.Spice;
    }
}