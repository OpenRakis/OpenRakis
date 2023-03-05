namespace DuneEdit2.UnitTests;

using System.IO;

using DuneEdit2.CLI;
using DuneEdit2.Parsers;

using FluentAssertions;

using Xunit;

public class WriteTests
{
    private const string SavesFolder = "Saves";
    private const string MidGamesSaveFileName = "MidGameSaveWrite.SAV";

    [Fact]
    public void CanEditArbitraryLocation()
    {
        var save = Path.Combine(SavesFolder, MidGamesSaveFileName);
        var output = Path.GetTempFileName();
        var writer = new SaveGameEditorCli(new Options { InputSaveGameFiles = new string[] { save }, Write = new string[] { "FF,CC", "11,01" }, OutputSaveGameFile = output });
        writer.GetStandardOutput();
        var bytes = new SaveGameFile(output, Enums.SaveFileFormat.DUNE_37).UncompressedData;
        bytes.Should().HaveElementAt(0xCC, 0xFF);
        bytes.Should().HaveElementAt(0x01, 0x11);
    }
}