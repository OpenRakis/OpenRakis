using System;

using CommandLine;
using DuneEdit2;

Parser.Default.ParseArguments<Options>(args).WithParsed(o => new SaveGameEditor(o).Run()).WithNotParsed((h) => { Console.WriteLine("options parse error!"); });