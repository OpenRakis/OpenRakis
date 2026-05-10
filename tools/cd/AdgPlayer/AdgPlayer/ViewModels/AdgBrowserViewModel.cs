namespace AdgPlayer.ViewModels;

using AdgPlayer.Audio;

using ReactiveUI;

/// <summary>
/// ViewModel responsible for browsing the ADG song catalog and selecting a song.
/// </summary>
public sealed class AdgBrowserViewModel : ViewModelBase
{
    private readonly AdgPlayerEngine _engine;
    private AdgSong? _selectedSong;
    private IReadOnlyList<AdgSong> _songs = Array.Empty<AdgSong>();

    /// <summary>
    /// Initializes a new instance of <see cref="AdgBrowserViewModel"/>.
    /// </summary>
    /// <param name="engine">The player engine providing the song catalog.</param>
    public AdgBrowserViewModel(AdgPlayerEngine engine)
    {
        _engine = engine;
        _songs = _engine.GetCatalog();
        _selectedSong = _songs.Count > 0 ? _songs[0] : null;
    }

    /// <summary>
    /// Gets the list of all available songs.
    /// </summary>
    public IReadOnlyList<AdgSong> Songs
    {
        get => _songs;
        private set => this.RaiseAndSetIfChanged(ref _songs, value);
    }

    /// <summary>
    /// Gets or sets the currently selected song.
    /// Setting this value also loads the song into the player engine.
    /// </summary>
    public AdgSong? SelectedSong
    {
        get => _selectedSong;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedSong, value);
            this.RaisePropertyChanged(nameof(SelectedSongName));
            if (value is not null)
            {
                _engine.Load(value);
            }
        }
    }

    /// <summary>
    /// Gets the display name of the selected song, or an empty string when none is selected.
    /// </summary>
    public string SelectedSongName => _selectedSong?.Name ?? string.Empty;
}
