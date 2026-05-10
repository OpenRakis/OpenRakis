namespace AdgPlayer.Audio;

/// <summary>
/// An <see cref="IAdgSongCatalog"/> that returns an empty list of songs.
/// Used as the default catalog when no game data files are present.
/// </summary>
public sealed class NullAdgSongCatalog : IAdgSongCatalog
{
    /// <inheritdoc />
    public IReadOnlyList<AdgSong> GetSongs() => Array.Empty<AdgSong>();
}
