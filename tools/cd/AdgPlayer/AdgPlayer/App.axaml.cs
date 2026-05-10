namespace AdgPlayer;

using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

using AdgPlayer.Audio;
using AdgPlayer.ViewModels;
using AdgPlayer.Views;

/// <summary>
/// Application entry point for the AdgPlayer Avalonia application.
/// </summary>
public sealed class App : Application
{
    /// <inheritdoc />
    public override void Initialize() => AvaloniaXamlLoader.Load(this);

    /// <inheritdoc />
    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(new NullAdgSongCatalog()),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
