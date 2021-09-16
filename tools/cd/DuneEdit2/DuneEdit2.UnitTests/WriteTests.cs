namespace DuneEdit2.UnitTests
{
    using DuneEdit2.Models;
    using DuneEdit2.Parsers;

    using FluentAssertions;

    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;

    using Xunit;

    public class WriteTests
    {
        private const string SavesFolder = "Saves";
        private const string MidGamesSaveFileName = "MidGameSave.SAV";

        [Fact]
        public void CanEditArbitraryLocation()
        {
            var save = Path.Combine(SavesFolder, MidGamesSaveFileName);
            var output = Path.GetTempFileName();
            var writer = new SaveGameEditorCli(new Options { InputSaveGameFiles = new string[] { save }, Write = new string[] { "FF,CC", "11,01" }, OutputSaveGameFile = output });
            writer.GetStandardOutput();
            var bytes = new SaveGameFile(output).UncompressedData;
            bytes.Should().HaveElementAt(0xCC, 0xFF);
            bytes.Should().HaveElementAt(0x01, 0x11);
        }

        [Fact]
        public async Task CanProcessMultipleEditArguments()
        {
            var executablePath = "DuneEdit2";
            var output = Path.GetTempFileName();
            var processStartInfo = new ProcessStartInfo(executablePath, $"-i {Path.Combine(SavesFolder, MidGamesSaveFileName)} -w FF,CC 11,01 -o {output}")
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };
            var process = new Process
            {
                StartInfo = processStartInfo,
                EnableRaisingEvents = true
            };
            var standardOutput = "";
            process.Exited += async (s, e) =>
            {
                standardOutput = await process.StandardOutput.ReadToEndAsync();
            };
            process.Start();
            while (process.HasExited == false)
            {
                await Task.Yield();
            }
            var bytes = new SaveGameFile(output).UncompressedData;
            bytes.Should().HaveElementAt(0xCC, 0xFF);
            bytes.Should().HaveElementAt(0x01, 0x11);
        }
    }
}