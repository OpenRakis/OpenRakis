namespace AdgPlayer.UnitTests.Tests;

using AdgPlayer.Audio;
using AdgPlayer.UnitTests.Stubs;

using FluentAssertions;

using Xunit;

/// <summary>
/// TDD tests for <see cref="AdgPlayerEngine"/> using the stub catalog.
/// No DUNE.DAT or DNCDPRG.EXE is required.
/// </summary>
public sealed class AdgPlayerEngineTests
{
    private static AdgPlayerEngine CreateSut() =>
        new AdgPlayerEngine(new StubAdgSongCatalog());

    [Fact]
    public void GetCatalog_ShouldReturnAllStubbedSongs()
    {
        var sut = CreateSut();
        sut.GetCatalog().Should().HaveCount(StubAdgSongCatalog.FakeSongs.Count);
    }

    [Fact]
    public void CurrentSong_ShouldBeNull_BeforeLoad()
    {
        var sut = CreateSut();
        sut.CurrentSong.Should().BeNull();
    }

    [Fact]
    public void AfterLoad_CurrentSong_ShouldMatchLoadedSong()
    {
        var sut = CreateSut();
        var song = StubAdgSongCatalog.FakeSongs[0];
        sut.Load(song);
        sut.CurrentSong.Should().Be(song);
    }

    [Fact]
    public void State_ShouldBeStoppedAfterLoad()
    {
        var sut = CreateSut();
        sut.Load(StubAdgSongCatalog.FakeSongs[0]);
        sut.State.Status.Should().Be(AdgPlaybackStatus.Stopped);
    }

    [Fact]
    public void Play_AfterLoad_ShouldTransitionToPlaying()
    {
        var sut = CreateSut();
        sut.Load(StubAdgSongCatalog.FakeSongs[0]);
        sut.Play();
        sut.State.Status.Should().Be(AdgPlaybackStatus.Playing);
    }

    [Fact]
    public void Play_WithoutLoad_ShouldThrowInvalidOperationException()
    {
        var sut = CreateSut();
        sut.Invoking(e => e.Play())
           .Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Pause_WhilePlaying_ShouldTransitionToPaused()
    {
        var sut = CreateSut();
        sut.Load(StubAdgSongCatalog.FakeSongs[0]);
        sut.Play();
        sut.Pause();
        sut.State.Status.Should().Be(AdgPlaybackStatus.Paused);
    }

    [Fact]
    public void Pause_WhileStopped_ShouldRemainStopped()
    {
        var sut = CreateSut();
        sut.Load(StubAdgSongCatalog.FakeSongs[0]);
        sut.Pause();
        sut.State.Status.Should().Be(AdgPlaybackStatus.Stopped);
    }

    [Fact]
    public void Stop_ShouldClearCurrentSong()
    {
        var sut = CreateSut();
        sut.Load(StubAdgSongCatalog.FakeSongs[0]);
        sut.Play();
        sut.Stop();
        sut.CurrentSong.Should().BeNull();
    }

    [Fact]
    public void Stop_ShouldTransitionToStopped()
    {
        var sut = CreateSut();
        sut.Load(StubAdgSongCatalog.FakeSongs[0]);
        sut.Play();
        sut.Stop();
        sut.State.Status.Should().Be(AdgPlaybackStatus.Stopped);
    }
}
