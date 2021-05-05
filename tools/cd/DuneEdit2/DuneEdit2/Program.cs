﻿using System;

using CommandLine;
using DuneEdit2;

Parser.Default.ParseArguments<Options>(args).WithParsed((o) =>
    {
        if (o.ReadMode)
        {
            new SaveGameReaderCli().GetStandardOutput();
        }
        else
        {
            new SaveGameEditorCli(o).GetStandardOutput();
        }
        if(o.WaitBeforeExit)
        {
            Console.WriteLine("Press any key to exit");
            Console.Read();
        }
    }
).WithNotParsed((h) => { Console.WriteLine("options parse error!"); });