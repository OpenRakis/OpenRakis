using System;
using System.IO;

LipsExtractor.LipsExtractor.Print_Welcome();
if (args.Length != 1)
{
    Console.WriteLine("FATAL ERROR: too much or not enough arguments.{Environment.NewLine}");
    LipsExtractor.LipsExtractor.Print_Help();
}
else
{
    string directory = Path.GetFullPath(args[0]);
    if (Directory.Exists(directory))
    {
        if (directory.EndsWith(Path.DirectorySeparatorChar) == false)
        {
            directory += Path.DirectorySeparatorChar;
        }
        LipsExtractor.LipsExtractor.ExtractLips(directory);
    }
    else
    {
        Console.WriteLine($"FATAL ERROR: {directory} directory does not exist !{{Environment.NewLine}}");
    }
}
Console.WriteLine("{Environment.NewLine}Press Enter to exit.");
Console.Read();