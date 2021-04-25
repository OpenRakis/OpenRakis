namespace DuneEdit2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CommandLine;

    class Options
    {
        [Option('i', "inputFile", Default = "", Required = true, HelpText = "The savegame to edit, for example DUNE37S1.SAV")]
        public string InputSaveGameFile { get; set; } = "";

        [Option('r', "readMode", Default = false, Required = false, HelpText = "Describe the entire save game and exit")]
        public bool ReadMode { get; set; }

        [Option('t', "timeOfDay", Default = null, Required = false, HelpText = "Set the time of day")]
        public int? TimeOfDay { get; set; }
    }
}
