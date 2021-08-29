namespace DuneEdit2
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;

    using DuneEdit2.Models;

    internal class SaveGameEditorCli
    {
        private readonly Options _options;

        public SaveGameEditorCli(Options options)
        {
            this._options = options;
        }

        public string GetStandardOutput()
        {
            if (_options.Write.Any() && string.IsNullOrWhiteSpace(_options.Compress) == false)
            {
                return $"Editing a savegame and recompression of another at the same time is not supported.{Environment.NewLine}";
            }

            if (_options.Write.Any() && _options.InputSaveGameFiles.Any())
            {
                var inputFile = _options.InputSaveGameFiles.ElementAt(0);
                var savegame = new SaveGame(inputFile);
                EditSavegame(savegame);
            }

            if (string.IsNullOrWhiteSpace(_options.Compress) == false && string.IsNullOrWhiteSpace(_options.OutputSaveGameFile) == false)
            {
                foreach (var inputFilePath in _options.InputSaveGameFiles)
                {
                    return ReCompressUncompressedSavegameFile(inputFilePath);
                }
            }
            return "";
        }

        private void EditSavegame(SaveGame savegame)
        {
            for (int i = 0; i < _options.Write.Count(); i++)
            {
                var edit = _options.Write.ElementAt(i);
                if (string.IsNullOrWhiteSpace(edit) || edit.Contains(',') == false)
                {
                    throw new ArgumentException($"{nameof(_options.Write)} invalid date found: {edit}");
                }
                var splittedEdit = edit.Split(",");
                var pos = 0;
                byte value = 0;
                if (!byte.TryParse(splittedEdit[0], NumberStyles.HexNumber, CultureInfo.InstalledUICulture, out value))
                {
                    throw new ArgumentException($"{nameof(_options.Write)} invalid date found: {edit} for value part");
                }
                if (!int.TryParse(splittedEdit[1], NumberStyles.HexNumber, CultureInfo.InstalledUICulture, out pos))
                {
                    throw new ArgumentException($"{nameof(_options.Write)} invalid date found: {edit} for position part");
                }
                savegame.ModifyByteAtAddressInUncompressedData(value, pos);
                Console.WriteLine($"Written byte 0x{value:X2} at position 0x{pos:X2}");
            }
            savegame.SaveCompressedAs(_options.OutputSaveGameFile);
            Console.WriteLine($"Modified and compressed savegame written at {_options.OutputSaveGameFile}");
        }

        private string ReCompressUncompressedSavegameFile(string inputFilePath)
        {
            var savegame = new SaveGame(File.ReadAllBytes(inputFilePath).ToList(), false);
            savegame.SaveCompressedAs(_options.OutputSaveGameFile);
            return $"Compressed {_options.Compress} to {_options.OutputSaveGameFile}{Environment.NewLine}";
        }
    }
}