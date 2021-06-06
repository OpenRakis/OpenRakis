using DuneEdit2.Parsing;

using System.Text;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("DuneEdit2.UnitTests")]

namespace DuneEdit2
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    internal class SaveGameReaderCli
    {
        private readonly List<SaveGameReader> _readers = new();
        private readonly Options _options;

        public SaveGameReaderCli(Options options)
        {
            foreach (var inputFile in options.InputSaveGameFiles)
            {
                _readers.Add(new SaveGameReader(inputFile));
            }
            _options = options;
        }

        public string GetStandardOutput()
        {
            var stringBuilder = new StringBuilder();
            foreach (var reader in _readers)
            {
                stringBuilder.AppendLine(reader.SaveFilePath);
                stringBuilder.AppendLine("Player information");
                stringBuilder.AppendLine("------------------");
                stringBuilder.AppendLine(GetPlayerSpice(reader));
                stringBuilder.AppendLine(GetPlayerContactDistance(reader));
                stringBuilder.AppendLine(GetPlayerCharisma(reader));
                stringBuilder.AppendLine(GetDateAndTime(reader));
                stringBuilder.AppendLine(GetGameStage(reader));
                if (_options.Uncompress)
                {
                    reader.WriteUncompressedSaveGameInTheSameFolder();
                    stringBuilder.AppendLine($"{Environment.NewLine}Uncompressed savegame written to disk in another file");
                    stringBuilder.AppendLine(Environment.NewLine);
                }
            }
            return stringBuilder.ToString();
        }

        private static string GetDateAndTime(SaveGameReader reader) => $"Date and Time: {SaveGameReader.GetDateForUI()} ({reader.GetDateHexValue()}) - Position: {SaveGameReader.GetDatePosition()}";

        private static string GetGameStage(SaveGameReader reader) => $"Game Stage: {reader.GetGameStageExplained()} ({reader.GetGameStageHexValue()}) - Position: {SaveGameReader.GetGameStagePosition()}";

        private static string GetPlayerCharisma(SaveGameReader reader) => $"Charisma: {reader.GetPlayerCharismaForUI()} ({reader.GetPlayerCharismaHexValue()}) - Position: {SaveGameReader.GetPlayerCharismaPosition()}";

        private static string GetPlayerContactDistance(SaveGameReader reader) => $"Contact distance: {reader.GetPlayerContactDistanceForUI()} ({reader.GetPlayerContactDistanceHexValue()}) - Position: {SaveGameReader.GetPlayerContactDistancePosition()}";

        private static string GetPlayerSpice(SaveGameReader reader) => $"Spice: {reader.GetPlayerSpiceForUI()} Kg ({reader.GetPlayerSpiceHexValue()}) - Position: {SaveGameReader.GetPlayerSpiceHexPosition()}";
    }
}