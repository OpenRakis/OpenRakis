namespace AdgPlayer.Audio;

/// <summary>
/// Represents a single ADG (HERAD format) song from the Dune CD game.
/// </summary>
public sealed record AdgSong
{
    /// <summary>
    /// Gets the zero-based index of the song in the catalog.
    /// </summary>
    public int Index { get; init; }

    /// <summary>
    /// Gets the human-readable name of the song.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets the raw ADG (HERAD) data bytes for this song.
    /// </summary>
    public byte[] Data { get; init; } = Array.Empty<byte>();
}
