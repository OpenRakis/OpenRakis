namespace DuneEdit2;

using Avalonia;
using Avalonia.ReactiveUI;
using CommandLine;

using DuneEdit2.CLI;

using System;
using System.Linq;

internal class Program
{
    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace()
            .WithInterFont()
            .UseReactiveUI();

    public static void Main(string[] args)
    {
        if (args.Any())
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed((o) =>
            {
                if (string.IsNullOrWhiteSpace(o.OutputSaveGameFile) && (string.IsNullOrWhiteSpace(o.Compress) == false || o.Write.Any()))
                {
                    Console.WriteLine("You must specify an output file path if you Compress an uncompressed save game file or Write to a savegame file.");
                }
                else
                {
                    Console.Write(new SaveGameEditorCli(o).GetStandardOutput());
                }
                if (o.WaitBeforeExit)
                {
                    Console.WriteLine("");
                    Console.WriteLine("Press any key to exit");
                    Console.Read();
                }
            }
            ).WithNotParsed((h) =>
            {
                Console.WriteLine("options parse error!");
            });
        }
        else
        {
            // Initialization code. Don't use any Avalonia, third-party APIs or any
            // SynchronizationContext-reliant code before AppMain is called: things aren't
            // initialized yet and stuff might break.
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }
    }
}