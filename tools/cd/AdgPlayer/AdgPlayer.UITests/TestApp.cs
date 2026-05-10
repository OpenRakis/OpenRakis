namespace AdgPlayer.UITests;

using Avalonia;
using Avalonia.Headless;

/// <summary>
/// Configures the Avalonia application for headless UI testing.
/// </summary>
public static class TestAppBuilder
{
    /// <summary>
    /// Builds the Avalonia application configured for headless testing.
    /// </summary>
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UseHeadless(new AvaloniaHeadlessPlatformOptions());
}
