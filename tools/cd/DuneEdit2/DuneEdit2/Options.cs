namespace DuneEdit2
{
    using CommandLine;

    internal class Options
    {
        [Option('c', "Compress", Default = "", Required = false, HelpText = "Compress the specified file")]
        public string Compress { get; set; } = "";

        [Option('i', "InputFile", Default = "", Required = true, HelpText = "The savegame to edit or describe on the standard output, for example DUNE37S1.SAV")]
        public string InputSaveGameFile { get; set; } = "";

        [Option('o', "OutputFile", Default = "", Required = false, HelpText = "Savegame output file name after a Compress or Write")]
        public string OutputSaveGameFile { get; set; } = "";

        [Option('r', "Read", Default = true, Required = false, HelpText = "Describe the entire save game on the standard input")]
        public bool Read { get; set; }

        [Option('t', "TimeOfDay", Default = null, Required = false, HelpText = "Edit the time of day in the input savegame file")]
        public int? TimeOfDay { get; set; }

        [Option('u', "Uncompress", Default = false, Required = false, HelpText = "Save the uncompressed savegame to disk as [Filename.SAV.UNCOMPRESSED]")]
        public bool Uncompress { get; set; }

        [Option('p', "PauseBeforeExit", Default = false, Required = false, HelpText = "Wait a key press before exiting")]
        public bool WaitBeforeExit { get; set; }

        [Option('w', "Write", Default = "", Required = false, HelpText = "[Not implemented yet!] Write hex at position to the save game and exit. Format: ByteHexValue,UncompressedSaveGameHexOffset")]
        public string Write { get; set; } = "";
    }
}