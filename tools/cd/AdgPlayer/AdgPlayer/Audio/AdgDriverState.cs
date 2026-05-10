namespace AdgPlayer.Audio;

/// <summary>
/// Holds the current playback state for the HERAD ADG driver.
/// </summary>
public sealed class AdgDriverState
{
    /// <summary>
    /// Gets or sets the byte offset into the current song's ADG data.
    /// </summary>
    public int DataOffset { get; set; }

    /// <summary>
    /// Gets or sets the remaining number of ticks before the next event.
    /// </summary>
    public int TickDelay { get; set; }

    /// <summary>
    /// Gets or sets whether the driver is currently looping the song.
    /// </summary>
    public bool IsLooping { get; set; }

    /// <summary>
    /// Gets or sets the current playback status.
    /// </summary>
    public AdgPlaybackStatus Status { get; set; } = AdgPlaybackStatus.Stopped;

    /// <summary>
    /// Gets the channel routing table mapping OPL2 channels to song channels.
    /// </summary>
    public AdgChannelRoutingTable ChannelRouting { get; } = new AdgChannelRoutingTable();

    /// <summary>
    /// Resets the driver state to its initial values.
    /// </summary>
    public void Reset()
    {
        DataOffset = 0;
        TickDelay = 0;
        IsLooping = false;
        Status = AdgPlaybackStatus.Stopped;
        ChannelRouting.Reset();
    }
}
