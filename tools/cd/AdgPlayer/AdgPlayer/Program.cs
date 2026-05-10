using Avalonia;

using AdgPlayer;

/// <summary>
/// Application entry point.
/// </summary>
internal sealed class Program
{
    /// <summary>
    /// Main entry point – do not use Avalonia, third-party APIs, or any SynchronizationContext-reliant code before
    /// the app builder is called.
    /// </summary>
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);

    /// <summary>
    /// Avalonia configuration – don't remove, also used by visual designer.
    /// </summary>
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}
