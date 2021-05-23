namespace DuneEdit2.UnitTests
{
    using DuneEdit2.Parsing;

    using FluentAssertions;

    using System.IO;

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
        public void CanReadTheDateForUI() => new SaveGameReader(Path.Combine(SavesFolder, MidGamesSaveFileName)).GetDateForUI().Should().Be(23);
    }
}