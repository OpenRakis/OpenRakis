namespace DuneEdit2;

using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using DuneEdit2.ViewModels;
using DuneEdit2.Views;
using Avalonia.Controls;

public class App : Application
{
    public override void Initialize() => AvaloniaXamlLoader.Load(this);

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop)
        {
            return;
        }

        var mainWindow = new MainWindow();
        mainWindow.DataContext = new MainWindowViewModel(mainWindow);
        mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        desktop.MainWindow = mainWindow;
        mainWindow.Show();
    }
}