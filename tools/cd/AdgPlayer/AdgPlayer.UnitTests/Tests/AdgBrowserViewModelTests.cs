namespace AdgPlayer.UnitTests.Tests;

using AdgPlayer.Audio;
using AdgPlayer.UnitTests.Stubs;
using AdgPlayer.ViewModels;

using FluentAssertions;

using Xunit;

/// <summary>
/// TDD tests for <see cref="AdgBrowserViewModel"/> using the stub catalog.
/// No DUNE.DAT or DNCDPRG.EXE is required.
/// </summary>
public sealed class AdgBrowserViewModelTests
{
    private static AdgBrowserViewModel CreateSut()
    {
        var catalog = new StubAdgSongCatalog();
        var engine = new AdgPlayerEngine(catalog);
        return new AdgBrowserViewModel(engine);
    }

    [Fact]
    public void Songs_ShouldContainAllCatalogSongs()
    {
        var sut = CreateSut();
        sut.Songs.Should().HaveCount(StubAdgSongCatalog.FakeSongs.Count);
    }

    [Fact]
    public void Songs_ShouldMatchCatalogByName()
    {
        var sut = CreateSut();
        sut.Songs.Select(s => s.Name)
           .Should().BeEquivalentTo(StubAdgSongCatalog.FakeSongs.Select(s => s.Name));
    }

    [Fact]
    public void SelectedSong_ShouldDefaultToFirstSong()
    {
        var sut = CreateSut();
        sut.SelectedSong.Should().Be(StubAdgSongCatalog.FakeSongs[0]);
    }

    [Fact]
    public void SelectedSongName_ShouldReturnFirstSongName()
    {
        var sut = CreateSut();
        sut.SelectedSongName.Should().Be(StubAdgSongCatalog.FakeSongs[0].Name);
    }

    [Fact]
    public void SettingSelectedSong_ShouldUpdateSelectedSongName()
    {
        var sut = CreateSut();
        var second = StubAdgSongCatalog.FakeSongs[1];

        sut.SelectedSong = second;

        sut.SelectedSongName.Should().Be(second.Name);
    }

    [Fact]
    public void SettingSelectedSong_ToNull_ShouldReturnEmptyName()
    {
        var sut = CreateSut();
        sut.SelectedSong = null;
        sut.SelectedSongName.Should().BeEmpty();
    }

    [Fact]
    public void Songs_ShouldBeEmpty_WhenCatalogIsEmpty()
    {
        var engine = new AdgPlayerEngine(new NullAdgSongCatalog());
        var sut = new AdgBrowserViewModel(engine);
        sut.Songs.Should().BeEmpty();
    }

    [Fact]
    public void SelectedSong_ShouldBeNull_WhenCatalogIsEmpty()
    {
        var engine = new AdgPlayerEngine(new NullAdgSongCatalog());
        var sut = new AdgBrowserViewModel(engine);
        sut.SelectedSong.Should().BeNull();
    }
}
