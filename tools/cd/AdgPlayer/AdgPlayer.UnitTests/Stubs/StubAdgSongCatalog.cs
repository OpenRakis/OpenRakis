namespace AdgPlayer.UnitTests.Stubs;

using AdgPlayer.Audio;

/// <summary>
/// A test double for <see cref="IAdgSongCatalog"/> that returns a fixed set of predefined
/// songs without requiring any actual Dune CD game files (DUNE.DAT / DNCDPRG.EXE).
/// </summary>
public sealed class StubAdgSongCatalog : IAdgSongCatalog
{
    /// <summary>
    /// The predefined set of fake ADG songs exposed by this stub.
    /// </summary>
    public static readonly IReadOnlyList<AdgSong> FakeSongs = new[]
    {
        new AdgSong { Index = 0, Name = "Arrakis",      Data = new byte[] { 0x01, 0x02, 0x03 } },
        new AdgSong { Index = 1, Name = "Ornithopter",  Data = new byte[] { 0x04, 0x05, 0x06 } },
        new AdgSong { Index = 2, Name = "Sietch Tabr",  Data = new byte[] { 0x07, 0x08, 0x09 } },
        new AdgSong { Index = 3, Name = "Spice Fields",  Data = new byte[] { 0x0A, 0x0B, 0x0C } },
        new AdgSong { Index = 4, Name = "Fremen March", Data = new byte[] { 0x0D, 0x0E, 0x0F } },
    };

    /// <inheritdoc />
    public IReadOnlyList<AdgSong> GetSongs() => FakeSongs;
}
