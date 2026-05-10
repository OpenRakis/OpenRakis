namespace AdgPlayer.ViewModels;

using AdgPlayer.Audio;

using ReactiveUI;

using System.Windows.Input;

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

        PlayCommand = ReactiveCommand.Create(
            () =>
            {
                _engine.Play();
                this.RaisePropertyChanged(nameof(Status));
            },
            this.WhenAnyValue(x => x.Status, s => s != AdgPlaybackStatus.Playing));

        PauseCommand = ReactiveCommand.Create(
            () =>
            {
                _engine.Pause();
                this.RaisePropertyChanged(nameof(Status));
            },
            this.WhenAnyValue(x => x.Status, s => s == AdgPlaybackStatus.Playing));

        StopCommand = ReactiveCommand.Create(
            () =>
            {
                _engine.Stop();
                this.RaisePropertyChanged(nameof(Status));
            },
            this.WhenAnyValue(x => x.Status, s => s != AdgPlaybackStatus.Stopped));
    }

    /// <summary>
    /// Gets the command to start or resume playback.
    /// </summary>
    public ICommand PlayCommand { get; }

    /// <summary>
    /// Gets the command to pause playback.
    /// </summary>
    public ICommand PauseCommand { get; }

    /// <summary>
    /// Gets the command to stop playback and unload the current song.
    /// </summary>
    public ICommand StopCommand { get; }

    /// <summary>
    /// Gets the current playback status.
    /// </summary>
    public AdgPlaybackStatus Status => _engine.State.Status;
}
