using System;

using System.Linq;

using CommandLine;

using DuneEdit2;

Parser.Default.ParseArguments<Options>(args).WithParsed((o) =>
    {
        if (o.Read)
        {
            Console.Write(new SaveGameReaderCli(o).GetStandardOutput());
        }
        if (string.IsNullOrWhiteSpace(o.OutputSaveGameFile) && (string.IsNullOrWhiteSpace(o.Compress) == false || o.Write.Any()))
        {
            Console.WriteLine("You must specify an output file path if you Compress an uncompressed save game file or Write to a savegame file.");
        }
        else
        {
            Console.Write(new SaveGameEditorCli(o).GetStandardOutput());
        }
        if (o.WaitBeforeExit)
        {
            Console.WriteLine("");
            Console.WriteLine("Press any key to exit");
            Console.Read();
        }
    }
).WithNotParsed((h) =>
{
    Console.WriteLine("options parse error!");
});