namespace DuneEdit2.Editing
{
    using System;

    using DuneEdit2.Models;
    using DuneEdit2.Parsers;

    internal class SaveGameEditor
    {
        private readonly string _fileName = "";

        public SaveGameEditor(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            _fileName = fileName;
        }

        public void EditAtPosition(byte value, int position)
        {
            var savegame = new SaveGameFile(_fileName);
            if (position < 0 || position > savegame.Uncompressed.Count)
            {
                throw new ArgumentException($"{nameof(position)} {position} is out of range in the uncompressed data !");
            }
            savegame.ModifyByteAtAddressInUncompressedData(value, position);
        }
    }
}