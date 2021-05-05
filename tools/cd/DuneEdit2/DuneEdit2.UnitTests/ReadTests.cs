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
        public void CanReadCharismaForUI() => new SaveGameReader(Path.Combine(SavesFolder, MidGamesSaveFileName)).GetCharismaForUI().Should().Be(24);
    }
}