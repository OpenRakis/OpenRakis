[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("DuneEdit2.UnitTests")]

namespace DuneEdit2.Parsing
{
    using DuneEdit2.Models;
    using DuneEdit2.Parsing;
    using DuneEdit2.Models.Enums;
    using System;

    internal class SaveGameReader
    {
        private readonly SavegameItem _savegame;

        private readonly Generals _generals;

        public SaveGameReader(string saveFilePath)
        {
            _savegame = new SavegameItem(saveFilePath);
            _generals = new Generals(_savegame.Uncompressed);
        }

        public byte GetPlayerCharismaForUI()
        {
            return _generals.CharismaGUI;
        }

        internal int GetPlayerContactDistanceForUI()
        {
            return _generals.ContactDistance;
        }

        internal int GetPlayerSpiceForUI()
        {
            return _generals.Spice;
        }
    }
}