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
    if (str.EndsWith("\\") == false)
    {
        str += "\\";
    }

    if (Directory.Exists(str))
    {
        DuneImpactor.DuneImpactor.WriteDuneDatFile(str);
    }
    else
    {
        Console.WriteLine($"FATAL ERROR: " + str + " directory not exist !{Environment.NewLine}");
    }
}
Console.WriteLine($"{Environment.NewLine}Press Enter to exit.");
Console.Read();