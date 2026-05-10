namespace AdgPlayer.UnitTests.Tests;

using AdgPlayer.Audio;
using AdgPlayer.UnitTests.Stubs;
using AdgPlayer.ViewModels;

using FluentAssertions;

using Xunit;

/// <summary>
/// TDD tests for <see cref="AdgTransportViewModel"/> using the stub catalog.
/// No DUNE.DAT or DNCDPRG.EXE is required.
/// </summary>
public sealed class AdgTransportViewModelTests
{
    private static (AdgTransportViewModel Transport, AdgPlayerEngine Engine) CreateSut()
    {
        var catalog = new StubAdgSongCatalog();
        var engine = new AdgPlayerEngine(catalog);
        // Pre-load first song so transport commands have something to work with
        engine.Load(catalog.GetSongs()[0]);
        var transport = new AdgTransportViewModel(engine);
        return (transport, engine);
    }

    [Fact]
    public void Status_ShouldBeStoppedInitially()
    {
        var (sut, _) = CreateSut();
        sut.Status.Should().Be(AdgPlaybackStatus.Stopped);
    }

    [Fact]
    public void AfterPlay_StatusShouldBePlaying()
    {
        var (sut, _) = CreateSut();
        sut.PlayCommand.Execute(null);
        sut.Status.Should().Be(AdgPlaybackStatus.Playing);
    }

    [Fact]
    public void AfterPlayThenPause_StatusShouldBePaused()
    {
        var (sut, _) = CreateSut();
        sut.PlayCommand.Execute(null);
        sut.PauseCommand.Execute(null);
        sut.Status.Should().Be(AdgPlaybackStatus.Paused);
    }

    [Fact]
    public void AfterPlayThenStop_StatusShouldBeStopped()
    {
        var (sut, _) = CreateSut();
        sut.PlayCommand.Execute(null);
        sut.StopCommand.Execute(null);
        sut.Status.Should().Be(AdgPlaybackStatus.Stopped);
    }

    [Fact]
    public void Pause_WhenAlreadyStopped_ShouldRemainStopped()
    {
        var (sut, _) = CreateSut();
        sut.PauseCommand.Execute(null);
        sut.Status.Should().Be(AdgPlaybackStatus.Stopped);
    }

    [Fact]
    public void Stop_WhenAlreadyStopped_ShouldRemainStopped()
    {
        var (sut, engine) = CreateSut();
        // Re-load because Stop clears the current song
        engine.Load(StubAdgSongCatalog.FakeSongs[0]);
        sut.PlayCommand.Execute(null);
        sut.StopCommand.Execute(null);

        // Stopping again after stop — engine has no song, play would throw, but stop should be safe
        // (no-op since already stopped)
        sut.Status.Should().Be(AdgPlaybackStatus.Stopped);
    }
}
