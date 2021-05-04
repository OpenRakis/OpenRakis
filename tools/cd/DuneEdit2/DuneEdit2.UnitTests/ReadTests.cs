namespace DuneEdit2.UnitTests
{
    using System;

    using DuneEdit2.Parsing;

    using FluentAssertions;

    using Xunit;

    public class ReadTests
    {
        [Fact]
        public void CanReadCharisma()
        {
            new SaveGameReader("Saves\\MidGameSave.SAV").GetCharisma().Should().Be(24);
        }
    }
}