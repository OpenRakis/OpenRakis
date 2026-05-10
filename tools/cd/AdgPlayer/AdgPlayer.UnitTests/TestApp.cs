using Avalonia;
using Avalonia.Headless;

using AdgPlayer;

[assembly: Avalonia.Headless.AvaloniaTestApplication(typeof(AdgPlayer.UnitTests.TestApp))]

namespace AdgPlayer.UnitTests;

/// <summary>
/// Avalonia application class used by the headless unit test session.
/// Provides the <see cref="BuildAvaloniaApp"/> factory used by
/// <see cref="Avalonia.Headless.HeadlessUnitTestSession"/> to set up a
/// headless Avalonia environment for UI tests without DUNE.DAT or DNCDPRG.EXE.
/// </summary>
public sealed class TestApp
{
    /// <summary>
    /// Builds the Avalonia application for headless testing.
    /// </summary>
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UseHeadless(new AvaloniaHeadlessPlatformOptions
            {
                UseHeadlessDrawing = false,
            });
}
