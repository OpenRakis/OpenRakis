namespace AdgPlayer.Views;

using Avalonia.ReactiveUI;

using AdgPlayer.ViewModels;

/// <summary>
/// The main application window for AdgPlayer.
/// </summary>
public sealed partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    /// <summary>
    /// Initializes a new instance of <see cref="MainWindow"/>.
    /// </summary>
    public MainWindow() => InitializeComponent();
}
