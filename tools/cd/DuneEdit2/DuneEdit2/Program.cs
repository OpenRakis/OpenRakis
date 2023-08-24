using System;

namespace DuneEdit2;

using Avalonia;
using Avalonia.ReactiveUI;

internal class Program
{
    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace()
            .WithInterFont()
            .UseReactiveUI();

    [STAThread]
    public static void Main(string[] args)
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't
        // initialized yet and stuff might break.
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }
}