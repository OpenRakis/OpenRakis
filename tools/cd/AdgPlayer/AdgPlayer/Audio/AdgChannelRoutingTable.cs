namespace AdgPlayer.Audio;

/// <summary>
/// Tracks the current state of each OPL2 channel during HERAD ADG playback.
/// </summary>
public sealed class AdgChannelRoutingTable
{
    private const int ChannelCount = 9;

    private readonly int[] _songChannels = new int[ChannelCount];

    /// <summary>
    /// Gets the number of OPL2 channels (always 9 for OPL2).
    /// </summary>
    public int Count => ChannelCount;

    /// <summary>
    /// Gets or sets the song-level channel index assigned to the given OPL2 channel.
    /// </summary>
    /// <param name="oplChannel">The OPL2 channel index (0-8).</param>
    public int this[int oplChannel]
    {
        get => _songChannels[oplChannel];
        set => _songChannels[oplChannel] = value;
    }

    /// <summary>
    /// Resets all channel assignments to their unrouted state (-1).
    /// </summary>
    public void Reset()
    {
        for (int i = 0; i < ChannelCount; i++)
        {
            _songChannels[i] = -1;
        }
    }
}
