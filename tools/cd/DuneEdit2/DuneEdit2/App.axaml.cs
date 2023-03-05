namespace DuneEdit2;

using System;
using Microsoft.Win32;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using DuneEdit2.ViewModels;
using DuneEdit2.Views;
using System.Runtime.Versioning;
using Avalonia.Controls;

public class App : Application
{
    private const string RegistryKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";

    private const string RegistryValueName = "AppsUseLightTheme";

    public override void Initialize() => AvaloniaXamlLoader.Load(this);

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop)
        {
            return;
        }
        if (IsInDarkMode() == false)
        {
            this.Styles.RemoveAt(1);
        }

        // Debugging requires pdb loading etc, so we disable live reloading
        // during a test run with an attached debugger.
        var mainWindow = new MainWindow();
        mainWindow.DataContext = MainWindowViewModel.Create(mainWindow);
        mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        desktop.MainWindow = mainWindow;
        mainWindow.Show();
        base.OnFrameworkInitializationCompleted();
    }

    [SupportedOSPlatform("windows")]
    private static bool GetIsWindowsInDarkMode()
    {
        RegistryKey? key = Registry.CurrentUser.OpenSubKey(RegistryKeyPath);
        var registryValueObject = key?.GetValue(RegistryValueName);
        if (registryValueObject == null)
        {
            return false;
        }

        var registryValue = (int)registryValueObject;
        return registryValue <= 0;
    }

    private static bool IsInDarkMode()
    {
#pragma warning disable ERP022 // Unobserved exception in generic exception handler
        try
        {
            if (OperatingSystem.IsWindows())
            {
                return GetIsWindowsInDarkMode();
            }
        }
        catch
        {
            //No OS support for themes. Not worth crashing for. Not worth reporting.
        }
#pragma warning restore ERP022 // Unobserved exception in generic exception handler
        return false;
    }
}