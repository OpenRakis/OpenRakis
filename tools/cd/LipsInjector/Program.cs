using System;
using System.IO;

LipsInjector.LipsInjector.Print_Welcome();
if (args.Length != 1)
{
    Console.WriteLine("FATAL ERROR: too much or not enough arguments.{Environment.NewLine}");
    LipsInjector.LipsInjector.Print_Help();
}
else
{
    string inputDirectory = Path.GetFullPath(args[0]);
    if (inputDirectory.EndsWith(Path.DirectorySeparatorChar) == false)
    {
        inputDirectory += Path.DirectorySeparatorChar;
    }
    if (!Directory.Exists(inputDirectory))
    {
        Console.WriteLine($"FATAL ERROR: {inputDirectory} directory does not exist !{Environment.NewLine}");
    }
    else if (!Directory.Exists(Path.Combine(inputDirectory, "LIPS")))
    {
        Console.WriteLine($"FATAL ERROR: {Path.Combine(inputDirectory, "LIPS")} directory does not exist !{Environment.NewLine}");
    }
    else
    {
        LipsInjector.LipsInjector.InjectLips(inputDirectory);
        LipsInjector.LipsInjector.DispatchVOC($"{Path.Combine(inputDirectory, "DONE")}{Path.DirectorySeparatorChar}");
    }
}
Console.WriteLine($"{Environment.NewLine}Press Enter to exit.");
Console.Read();