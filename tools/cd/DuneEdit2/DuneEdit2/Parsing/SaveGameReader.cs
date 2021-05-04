[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("DuneEdit2.UnitTests")]

namespace DuneEdit2.Parsing
{
    using DuneEdit2.Models;
    using DuneEdit2.Parsing;
    using DuneEdit2.Models.Enums;

    internal class SaveGameReader
    {
        private readonly string _saveFilePath = "";

        private readonly SavegameItem _savegame;

        private readonly Generals _generals;

        public SaveGameReader(string saveFilePath)
        {
            _saveFilePath = saveFilePath;
            _savegame = new SavegameItem(_saveFilePath);
            _generals = new Generals(_savegame.Uncompressed);
        }

        public byte GetCharisma()
        {
            return _generals.Charisma;
        }
    }
}