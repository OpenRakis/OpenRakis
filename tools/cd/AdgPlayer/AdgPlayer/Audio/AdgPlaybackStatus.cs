namespace AdgPlayer.Audio;

/// <summary>
/// Describes the current playback status of the ADG driver.
/// </summary>
public enum AdgPlaybackStatus
{
    /// <summary>Playback is stopped and no song is loaded.</summary>
    Stopped,

    /// <summary>A song is loaded and currently playing.</summary>
    Playing,

    /// <summary>A song is loaded but playback is paused.</summary>
    Paused,
}
