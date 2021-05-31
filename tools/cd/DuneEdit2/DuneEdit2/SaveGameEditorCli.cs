namespace DuneEdit2
{
    using System;
    using System.IO;
    using System.Linq;

    internal class SaveGameEditorCli
    {
        private readonly Options _options;

        public SaveGameEditorCli(Options options)
        {
            this._options = options;
        }

        public string GetStandardOutput()
        {
            if (string.IsNullOrWhiteSpace(_options.Write) == false && string.IsNullOrWhiteSpace(_options.Compress) == false)
            {
                return $"Editing a savegame and recompression of another at the same time is not supported.{Environment.NewLine}";
            }

            if (string.IsNullOrWhiteSpace(_options.Compress) == false && string.IsNullOrWhiteSpace(_options.OutputSaveGameFile) == false)
            {
                if (_options.InputSaveGameFile.Contains(","))
                {
                    foreach (var inputFilePath in _options.InputSaveGameFile.Split(','))
                    {
                        return ReCompressUncompressedSavegameFile(inputFilePath);
                    }
                }
                else
                {
                    return ReCompressUncompressedSavegameFile(_options.InputSaveGameFile);
                }
            }
            return "";
        }

        private string ReCompressUncompressedSavegameFile(string inputFilePath)
        {
            var savegame = new Savegame(File.ReadAllBytes(inputFilePath).ToList(), false);
            savegame.SaveCompressedAs(_options.OutputSaveGameFile);
            return $"Compressed {_options.Compress} to {_options.OutputSaveGameFile}{Environment.NewLine}";
        }
    }
}