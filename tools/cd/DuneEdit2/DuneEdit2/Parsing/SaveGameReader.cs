[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("DuneEdit2.UnitTests")]

namespace DuneEdit2.Parsing
{
    using DuneEdit2.Models;
    using DuneEdit2.Parsing;
    using DuneEdit2.Models.Enums;

    internal class SaveGameReader
    {
        private readonly SavegameItem _savegame;

        private readonly Generals _generals;

        public SaveGameReader(string saveFilePath)
        {
            _savegame = new SavegameItem(saveFilePath);
            _generals = new Generals(_savegame.Uncompressed);
        }

        public byte GetCharismaForUI()
        {
            return _generals.CharismaGUI;
        }
    }
}