// See https://aka.ms/new-console-template for more information

using DuneSaveDescriptor.CSV;
using DuneSaveDescriptor.Decompression;

if (!args.Any())
{
    Console.WriteLine("Please specify a path to a DUNE 3.7 sav file as argument");
}
var saveFile = args[0];
if (!File.Exists(saveFile))
{
    Console.WriteLine("File does not exist or is unreachable");
}

var compressedSaveFile = File.ReadAllBytes(saveFile);
var uncompressedSave = Decompressor.Decompress(compressedSaveFile);
var uncompressedFileName = $"{saveFile}.BIN";
if (File.Exists(uncompressedFileName))
{
    File.Delete(uncompressedFileName);
}
File.WriteAllBytes(uncompressedFileName, uncompressedSave.DecompressedData);

var csvFileName = $"{saveFile}.CSV";

if (File.Exists(csvFileName))
{
    File.Delete(csvFileName);
}

IEnumerable<string> csvData = SaveFileCsv.GenerateLines(uncompressedSave);

File.WriteAllLines(csvFileName, csvData);