namespace DuneEdit2.UnitTests
{
    using DuneEdit2.Parsing;

    using FluentAssertions;

    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;

    using Xunit;

    public class ReadTests
    {
        private const string SavesFolder = "Saves";
        private const string MidGamesSaveFileName = "MidGameSave.SAV";

        [Fact]
        public void CanReadPlayerCharismaForUI() => new SaveGameReader(Path.Combine(SavesFolder, MidGamesSaveFileName)).GetPlayerCharismaForUI().Should().Be(24);

        [Fact]
        public void CanReadPlayerContactDistanceForUI() => new SaveGameReader(Path.Combine(SavesFolder, MidGamesSaveFileName)).GetPlayerContactDistanceForUI().Should().Be(50);

        [Fact]
        public void CanReadPlayerSpiceForUI() => new SaveGameReader(Path.Combine(SavesFolder, MidGamesSaveFileName)).GetPlayerSpiceForUI().Should().Be(43270);

        [Fact]
        public void CanReadTheDateForUI() => new SaveGameReader(Path.Combine(SavesFolder, MidGamesSaveFileName)).GetDateForUI().Should().Be(99);

        [Fact]
        public async Task ProcessReadSeveralFilesAsync()
        {
            var executablePath = "DuneEdit2";
            var processStartInfo = new ProcessStartInfo(executablePath, $"-r -i {Path.Combine(SavesFolder, MidGamesSaveFileName)} {Path.Combine(SavesFolder, MidGamesSaveFileName)}")
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };
            var process = new Process
            {
                StartInfo = processStartInfo,
                EnableRaisingEvents = true
            };
            var output = "";
            process.Exited += async (s, e) =>
            {
                output = await process.StandardOutput.ReadToEndAsync();
            };
            process.Start();
            while (process.HasExited == false)
            {
                await Task.Yield();
            }
            output.Should().Contain(MidGamesSaveFileName);
        }
    }
}