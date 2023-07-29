namespace DuneEdit2.CLI;

using CommandLine;

using System.Collections.Generic;

internal class Options
{
    [Option('c', "Compress", Default = "", Required = false, HelpText = "Compress the specified file")]
    public string Compress { get; set; } = "";

    [Option('i', "InputFile", Required = true, Separator = ' ', HelpText = "The savegame to edit or describe on the standard output, for example DUNE37S1.SAV. For reading, it can be several, separated by a space")]
    public IEnumerable<string> InputSaveGameFiles { get; set; } = new List<string>();

    [Option('o', "OutputFile", Default = "", Required = false, HelpText = "Savegame output file name after a Compress or Write")]
    public string OutputSaveGameFile { get; set; } = "";

    [Option('u', "Uncompress", Default = false, Required = false, HelpText = "Save the uncompressed savegame to disk as [Filename.SAV.UNCOMPRESSED]")]
    public bool Uncompress { get; set; }

    [Option('p', "PauseBeforeExit", Default = false, Required = false, HelpText = "Wait a key press before exiting")]
    public bool WaitBeforeExit { get; set; }

    [Option('w', "Write", Separator = ' ', Required = false, HelpText = "Write hex at position to the FIRST input file, before recompressing it. Format: ByteHexValue,UncompressedSaveGameHexPosition. Can be several Byte and Position couples, separated by a space. For example: 0x00,0x1 0x01,0x2")]
    public IEnumerable<string> Write { get; set; } = new List<string>();
}