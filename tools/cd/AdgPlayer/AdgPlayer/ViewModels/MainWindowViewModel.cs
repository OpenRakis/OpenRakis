namespace AdgPlayer.ViewModels;

using AdgPlayer.Audio;

/// <summary>
/// Root ViewModel for the AdgPlayer application window.
/// Combines the song browser and transport controls.
/// </summary>
public sealed class MainWindowViewModel : ViewModelBase
{
    private string _title;

    /// <summary>
    /// Initializes a new instance of <see cref="MainWindowViewModel"/>.
    /// </summary>
    /// <param name="catalog">The ADG song catalog to use.</param>
    public MainWindowViewModel(IAdgSongCatalog catalog)
    {
        var engine = new AdgPlayerEngine(catalog);
        Browser = new AdgBrowserViewModel(engine);
        Transport = new AdgTransportViewModel(engine);

        // Load first song automatically when available
        if (Browser.SelectedSong is not null)
        {
            engine.Load(Browser.SelectedSong);
        }

        // Initialise title from whatever song the browser selected on startup
        _title = FormatTitle(Browser.SelectedSongName);

        // Keep Title in sync with subsequent selection changes
        Browser.PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == nameof(AdgBrowserViewModel.SelectedSongName))
            {
                Title = FormatTitle(Browser.SelectedSongName);
            }
        };
    }

    /// <summary>
    /// Gets the browser ViewModel for selecting songs.
    /// </summary>
    public AdgBrowserViewModel Browser { get; }

    /// <summary>
    /// Gets the transport ViewModel for controlling playback.
    /// </summary>
    public AdgTransportViewModel Transport { get; }

    /// <summary>
    /// Gets the window title including the selected song name when available.
    /// </summary>
    public string Title
    {
        get => _title;
        private set => SetProperty(ref _title, value);
    }

    private static string FormatTitle(string? songName) =>
        string.IsNullOrEmpty(songName)
            ? "AdgPlayer – Dune CD Music"
            : $"AdgPlayer – {songName}";
}
