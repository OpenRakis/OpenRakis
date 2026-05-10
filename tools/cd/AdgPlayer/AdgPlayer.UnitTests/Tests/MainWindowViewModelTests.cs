namespace AdgPlayer.UnitTests.Tests;

using AdgPlayer.Audio;
using AdgPlayer.UnitTests.Stubs;
using AdgPlayer.ViewModels;

using FluentAssertions;

using Xunit;

/// <summary>
/// TDD tests for <see cref="MainWindowViewModel"/> using the stub catalog.
/// No DUNE.DAT or DNCDPRG.EXE is required.
/// </summary>
public sealed class MainWindowViewModelTests
{
    [Fact]
    public void Title_ShouldIncludeAppName()
    {
        var sut = new MainWindowViewModel(new StubAdgSongCatalog());
        sut.Title.Should().Contain("AdgPlayer");
    }

    [Fact]
    public void Title_ShouldIncludeSelectedSongName_WhenCatalogHasSongs()
    {
        var sut = new MainWindowViewModel(new StubAdgSongCatalog());
        sut.Title.Should().Contain(StubAdgSongCatalog.FakeSongs[0].Name);
    }

    [Fact]
    public void Title_ShouldOnlyContainAppName_WhenCatalogIsEmpty()
    {
        var sut = new MainWindowViewModel(new NullAdgSongCatalog());
        sut.Title.Should().Be("AdgPlayer – Dune CD Music");
    }

    [Fact]
    public void Browser_ShouldNotBeNull()
    {
        var sut = new MainWindowViewModel(new StubAdgSongCatalog());
        sut.Browser.Should().NotBeNull();
    }

    [Fact]
    public void Transport_ShouldNotBeNull()
    {
        var sut = new MainWindowViewModel(new StubAdgSongCatalog());
        sut.Transport.Should().NotBeNull();
    }

    [Fact]
    public void Transport_StatusShouldBeStoppedInitially()
    {
        var sut = new MainWindowViewModel(new StubAdgSongCatalog());
        sut.Transport.Status.Should().Be(AdgPlaybackStatus.Stopped);
    }

    [Fact]
    public void Title_ShouldUpdate_WhenBrowserSelectionChanges()
    {
        var sut = new MainWindowViewModel(new StubAdgSongCatalog());
        var second = StubAdgSongCatalog.FakeSongs[1];

        sut.Browser.SelectedSong = second;

        sut.Title.Should().Contain(second.Name);
    }
}
