namespace DuneEdit2.Editing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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
            var savegame = new Savegame(_fileName);
            if (position < 0 || position > savegame.Uncompressed.Count)
            {
                throw new ArgumentException($"{nameof(position)} {position} is out of range in the uncompressed data !");
            }
            savegame.ModiifyByteAtAddressInUncompressedData(value, position);
        }
    }
}