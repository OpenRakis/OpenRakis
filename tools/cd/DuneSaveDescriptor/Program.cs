using DuneSaveDescriptor.CLI;
using DuneSaveDescriptor.CSV;
using DuneSaveDescriptor.Decompression;

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
foreach(var line in File.ReadAllLines(csvFileName))
{
    Console.WriteLine(line);
}

static void DisplayHelpAndExit()
{
    Console.WriteLine("Please specify a path to a DUNE 3.7 sav file as argument");
    Environment.FailFast(null);
}