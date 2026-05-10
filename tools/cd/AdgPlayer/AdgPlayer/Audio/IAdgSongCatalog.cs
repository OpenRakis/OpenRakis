namespace AdgPlayer.Audio;

/// <summary>
/// Provides access to the collection of ADG songs available for playback.
/// </summary>
public interface IAdgSongCatalog
{
    /// <summary>
    /// Returns all songs in the catalog.
    /// </summary>
    IReadOnlyList<AdgSong> GetSongs();
}
