namespace AdgPlayer.UITests.Tests;

using Avalonia.Headless.XUnit;

using AdgPlayer.Audio;
using AdgPlayer.UnitTests.Stubs;
using AdgPlayer.ViewModels;
using AdgPlayer.Views;

using FluentAssertions;

/// <summary>
/// Headless Avalonia UI tests that verify the <see cref="MainWindow"/> renders correctly
/// with <see cref="StubAdgSongCatalog"/> data – no DUNE.DAT or DNCDPRG.EXE required.
/// </summary>
public sealed class MainWindowHeadlessTests
{
    [AvaloniaFact]
    public void Window_ShouldHaveCorrectTitle_WhenSongsArePresent()
    {
        var vm = new MainWindowViewModel(new StubAdgSongCatalog());
        var window = new MainWindow { DataContext = vm };
        window.Show();
        window.Title.Should().Contain("AdgPlayer");
        window.Close();
    }

    [AvaloniaFact]
    public void Window_ShouldBindBrowserViewModel()
    {
        var vm = new MainWindowViewModel(new StubAdgSongCatalog());
        var window = new MainWindow { DataContext = vm };
        window.Show();
        vm.Browser.Songs.Should().HaveCount(StubAdgSongCatalog.FakeSongs.Count);
        window.Close();
    }

    [AvaloniaFact]
    public void Window_TransportStatus_ShouldBeStoppedInitially()
    {
        var vm = new MainWindowViewModel(new StubAdgSongCatalog());
        var window = new MainWindow { DataContext = vm };
        window.Show();
        vm.Transport.Status.Should().Be(AdgPlaybackStatus.Stopped);
        window.Close();
    }

    [AvaloniaFact]
    public void Window_AfterPlayCommand_StatusShouldBePlaying()
    {
        var vm = new MainWindowViewModel(new StubAdgSongCatalog());
        var window = new MainWindow { DataContext = vm };
        window.Show();

        vm.Transport.PlayCommand.Execute(null);

        vm.Transport.Status.Should().Be(AdgPlaybackStatus.Playing);
        window.Close();
    }

    [AvaloniaFact]
    public void Window_SelectSecondSong_UpdatesSelectedSongName()
    {
        var vm = new MainWindowViewModel(new StubAdgSongCatalog());
        var window = new MainWindow { DataContext = vm };
        window.Show();

        vm.Browser.SelectedSong = StubAdgSongCatalog.FakeSongs[1];

        vm.Browser.SelectedSongName.Should().Be(StubAdgSongCatalog.FakeSongs[1].Name);
        window.Close();
    }
}
