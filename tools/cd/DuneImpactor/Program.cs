using System;
using System.IO;

DuneImpactor.DuneImpactor.Print_Welcome();
if (args.Length != 1)
{
    Console.WriteLine($"FATAL Error: too much or not enough arguments.{Environment.NewLine}");
    DuneImpactor.DuneImpactor.Print_Help();
}
else
{
    string str = Path.GetFullPath(args[0]);
    if (Directory.Exists(str))
    {
        if (str.EndsWith(Path.DirectorySeparatorChar) == false)
        {
            str += Path.DirectorySeparatorChar;
        }
        DuneImpactor.DuneImpactor.WriteDuneDatFile(str);
    }
    else
    {
        Console.WriteLine($"FATAL ERROR: {str} directory does not exist !{Environment.NewLine}");
    }
}
Console.WriteLine($"{Environment.NewLine}Press Enter to exit.");
Console.Read();