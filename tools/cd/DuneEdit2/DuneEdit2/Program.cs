using System;

using CommandLine;

using DuneEdit2;

Parser.Default.ParseArguments<Options>(args).WithParsed((o) =>
    {
        if (o.ReadMode)
        {
            Console.Write(new SaveGameReaderCli(o).GetStandardOutput());
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
).WithNotParsed((h) => { Console.WriteLine("options parse error!"); });