[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("DuneEdit2.UnitTests")]

namespace DuneEdit2.Parsing
{
    using DuneEdit2.Models;

    internal class SaveGameReader
    {
        private readonly SavegameItem _savegame;

        private readonly Generals _generals;
        private readonly string _saveFilePath;

        public SaveGameReader(string saveFilePath)
        {
            _savegame = new SavegameItem(saveFilePath);
            _generals = new Generals(_savegame.Uncompressed);
            _saveFilePath = saveFilePath;
        }

        public void WriteUncompressedSaveGameInTheSameFolder() => System.IO.File.WriteAllBytes($"{_saveFilePath}.UNCOMPRESSED", _savegame.Uncompressed.ToArray());

        public byte GetPlayerCharismaForUI() => _generals.CharismaGUI;

        internal int GetPlayerContactDistanceForUI() => _generals.ContactDistance;

        internal string GetPlayerContactDistanceHexValue() => $"0x{_generals.ContactDistance:X}";

        internal object GetPlayerCharismaPosition() => $"0x{Generals.CharismaStartOffset:X}";

        internal string GetPlayerCharismaHexValue() => $"0x{_generals.Charisma:X}";

        internal string GetDateHexValue() => $"0x{_generals.DateAsHex}";

        internal string GetDatePosition() => $"0x{Generals.DateTimeStartOffset:X}";

        internal int GetDateForUI() => _generals.DateGUI;

        internal string GetPlayerContactDistancePosition() => $"0x{Generals.ContactDistanceStartOffset:X}";

        internal string GetPlayerSpiceHexPosition() => $"0x{Generals.SpiceStartOffset:X}";

        internal string GetPlayerSpiceHexValue() => $"0x{_generals.SpiceAsHex}";

        internal int GetPlayerSpiceForUI()
        {
            return _generals.Spice;
        }
    }
}