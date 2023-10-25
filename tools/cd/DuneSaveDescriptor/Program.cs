﻿using DuneSaveDescriptor.CLI;
using DuneSaveDescriptor.CSV;
using DuneSaveDescriptor.Decompression;
using DuneSaveDescriptor.Savegame;

using DuneEdit2.Enums;
using DuneEdit2.Models;

if (!args.Any())
{
    DisplayHelpAndExit();
}
var saveFile = CommandLine.Parser.Default.ParseArguments<Options>(args).Value.SaveFile;
if(string.IsNullOrWhiteSpace(saveFile))
{
    saveFile = args.Last();
}
if (!File.Exists(saveFile))
{
    Console.WriteLine($"File '{saveFile}' does not exist or is unreachable");
    DisplayHelpAndExit();
}

var compressedSaveFile = File.ReadAllBytes(saveFile);
var uncompressedSave = Decompressor.Decompress(compressedSaveFile, new Dune37Offsets());
var uncompressedFileName = $"{saveFile}.BIN";
if (File.Exists(uncompressedFileName))
{
    File.Delete(uncompressedFileName);
}
File.WriteAllBytes(uncompressedFileName, uncompressedSave.Data);

var csvFileName = $"{saveFile}.CSV";

if (File.Exists(csvFileName))
{
    File.Delete(csvFileName);
}

Dictionary<Range, SaveStructure> description = SaveDescriptor.GenerateDescription(uncompressedSave);

IEnumerable<string> csvData = SaveFileCsv.GenerateLines(description);

File.WriteAllLines(csvFileName, csvData);
foreach(var line in File.ReadAllLines(csvFileName))
{
    Console.WriteLine(line);
}

static void DisplayHelpAndExit()
{
    Console.WriteLine("Please specify a path to a DUNE 3.7 sav file as argument");
    Environment.FailFast(null);
}