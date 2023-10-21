// See https://aka.ms/new-console-template for more information

using DuneSaveDescriptor.Decompression;

if (!args.Any())
{
    Console.WriteLine("Please specify a path to a DUNE 3.7 sav file as argument");
}
var saveFile = args[0];
if (!File.Exists(saveFile))
{
    Console.WriteLine("File does not exit or is unreachable");
}

var compressedSaveFile = File.ReadAllBytes(saveFile);
var uncompressedData = Decompressor.Decompress(compressedSaveFile);
File.WriteAllBytes($"{saveFile}.BIN", uncompressedData);