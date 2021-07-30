namespace DuneEdit2
{
    using System;
    using Microsoft.Win32;
    using Avalonia;
    using Avalonia.Controls.ApplicationLifetimes;
    using Avalonia.Markup.Xaml;
    using DuneEdit2.ViewModels;
    using DuneEdit2.Views;
    using System.Runtime.Versioning;

    public class App : Application
    {
        private const string RegistryKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";

        private const string RegistryValueName = "AppsUseLightTheme";

        public override void Initialize() => AvaloniaXamlLoader.Load(this);

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                if (IsInDarkMode() == false)
                {
                    this.Styles.RemoveAt(1);
                }

                var mainWindow = new MainWindow();
                mainWindow.DataContext = MainWindowViewModel.Create(mainWindow);
                desktop.MainWindow = mainWindow;
            }

            base.OnFrameworkInitializationCompleted();
        }

        [SupportedOSPlatform("windows")]
        private static bool GetIsWindowsInDarkMode()
        {
            var key = Registry.CurrentUser.OpenSubKey(RegistryKeyPath);
            var registryValueObject = key?.GetValue(RegistryValueName);
            if (registryValueObject == null)
            {
                return false;
            }

            var registryValue = (int)registryValueObject;
            return registryValue > 0 ? false : true;
        }

        private bool IsInDarkMode()
        {
            try
            {
                if (OperatingSystem.IsWindows())
                {
                    return GetIsWindowsInDarkMode();
                }
            }
            catch
            {
                //No OS support for themes. Not worth crashing for.
            }
            return false;
        }
    }
}