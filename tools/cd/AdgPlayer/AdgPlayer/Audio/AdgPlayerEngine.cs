namespace AdgPlayer.Audio;

/// <summary>
/// Controls loading and playback of ADG (HERAD format) songs using an OPL2 driver.
/// </summary>
public sealed class AdgPlayerEngine
{
    private readonly IAdgSongCatalog _catalog;
    private readonly AdgDriverState _state = new();
    private AdgSong? _currentSong;

    /// <summary>
    /// Initializes a new instance of <see cref="AdgPlayerEngine"/> with the given song catalog.
    /// </summary>
    /// <param name="catalog">The catalog used to resolve ADG songs.</param>
    public AdgPlayerEngine(IAdgSongCatalog catalog)
    {
        _catalog = catalog;
    }

    /// <summary>
    /// Gets the current playback state.
    /// </summary>
    public AdgDriverState State => _state;

    /// <summary>
    /// Gets the song that is currently loaded, or <c>null</c> if none.
    /// </summary>
    public AdgSong? CurrentSong => _currentSong;

    /// <summary>
    /// Loads the specified song and prepares it for playback.
    /// </summary>
    /// <param name="song">The song to load.</param>
    public void Load(AdgSong song)
    {
        _currentSong = song;
        _state.Reset();
        _state.Status = AdgPlaybackStatus.Stopped;
    }

    /// <summary>
    /// Starts or resumes playback of the currently loaded song.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when no song is loaded.</exception>
    public void Play()
    {
        if (_currentSong is null)
        {
            throw new InvalidOperationException("No song is loaded. Call Load() first.");
        }

        _state.Status = AdgPlaybackStatus.Playing;
    }

    /// <summary>
    /// Pauses playback. Has no effect when already stopped or paused.
    /// </summary>
    public void Pause()
    {
        if (_state.Status == AdgPlaybackStatus.Playing)
        {
            _state.Status = AdgPlaybackStatus.Paused;
        }
    }

    /// <summary>
    /// Stops playback and resets the playback position.
    /// </summary>
    public void Stop()
    {
        _state.Reset();
        _currentSong = null;
    }

    /// <summary>
    /// Returns all available songs from the catalog.
    /// </summary>
    public IReadOnlyList<AdgSong> GetCatalog() => _catalog.GetSongs();
}
