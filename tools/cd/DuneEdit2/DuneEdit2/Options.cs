namespace DuneEdit2
{
    using CommandLine;

    internal class Options
    {
        [Option('i', "InputFile", Default = "", Required = true, HelpText = "The savegame to edit or describe on the standard output, for example DUNE37S1.SAV")]
        public string InputSaveGameFile { get; set; } = "";

        [Option('r', "ReadMode", Default = false, Required = false, HelpText = "Describe the entire save game on the standard input")]
        public bool ReadMode { get; set; }

        [Option('t', "TimeOfDay", Default = null, Required = false, HelpText = "Edit the time of day in the input savegame file")]
        public int? TimeOfDay { get; set; }

        [Option('w', "WaitBeforeExit", Default = false, Required = false, HelpText = "Wait a key press before exiting")]
        public bool WaitBeforeExit { get; set; }
    }
}