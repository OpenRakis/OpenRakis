namespace AdgPlayer.ViewModels;

using AdgPlayer.Audio;

using CommunityToolkit.Mvvm.Input;

/// <summary>
/// ViewModel for the transport controls (play, pause, stop) of the ADG player.
/// </summary>
public sealed class AdgTransportViewModel : ViewModelBase
{
    private readonly AdgPlayerEngine _engine;

    /// <summary>
    /// Initializes a new instance of <see cref="AdgTransportViewModel"/>.
    /// </summary>
    /// <param name="engine">The player engine to control.</param>
    public AdgTransportViewModel(AdgPlayerEngine engine)
    {
        _engine = engine;

        PlayCommand  = new RelayCommand(Play,  CanPlay);
        PauseCommand = new RelayCommand(Pause, CanPause);
        StopCommand  = new RelayCommand(Stop,  CanStop);
    }

    /// <summary>
    /// Gets the command to start or resume playback.
    /// </summary>
    public IRelayCommand PlayCommand { get; }

    /// <summary>
    /// Gets the command to pause playback.
    /// </summary>
    public IRelayCommand PauseCommand { get; }

    /// <summary>
    /// Gets the command to stop playback and unload the current song.
    /// </summary>
    public IRelayCommand StopCommand { get; }

    /// <summary>
    /// Gets the current playback status.
    /// </summary>
    public AdgPlaybackStatus Status => _engine.State.Status;

    private void Play()
    {
        _engine.Play();
        NotifyStatusChanged();
    }

    private void Pause()
    {
        _engine.Pause();
        NotifyStatusChanged();
    }

    private void Stop()
    {
        _engine.Stop();
        NotifyStatusChanged();
    }

    private bool CanPlay()  => Status != AdgPlaybackStatus.Playing;
    private bool CanPause() => Status == AdgPlaybackStatus.Playing;
    private bool CanStop()  => Status != AdgPlaybackStatus.Stopped;

    private void NotifyStatusChanged()
    {
        OnPropertyChanged(nameof(Status));
        PlayCommand.NotifyCanExecuteChanged();
        PauseCommand.NotifyCanExecuteChanged();
        StopCommand.NotifyCanExecuteChanged();
    }
}
