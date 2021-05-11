using DuneEdit2.Parsing;

using System.Text;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("DuneEdit2.UnitTests")]

namespace DuneEdit2
{
    internal class SaveGameReaderCli
    {
        private Options _options;
        private SaveGameReader _reader;

        public SaveGameReaderCli(Options options)
        {
            _options = options;
            _reader = new SaveGameReader(options.InputSaveGameFile);
        }

        public string GetStandardOutput()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(GetPlayerSpiceValue());
            return stringBuilder.ToString();
        }

        internal string GetPlayerSpiceValue() => $"Spice: {_reader.GetPlayerSpiceForUI()} Kg - Position: {_reader.GetPlayerSpiceHexPosition()} - HexValue: {_reader.GetPlayerSpiceHexValue()}";
    }
}