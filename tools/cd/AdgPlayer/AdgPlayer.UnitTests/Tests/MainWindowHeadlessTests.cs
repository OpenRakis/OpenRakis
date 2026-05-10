namespace AdgPlayer.UnitTests.Tests;

using System.Reflection;

using Avalonia.Headless;

using AdgPlayer.Audio;
using AdgPlayer.UnitTests.Stubs;
using AdgPlayer.ViewModels;
using AdgPlayer.Views;

using FluentAssertions;

using Xunit;

/// <summary>
/// Headless Avalonia UI tests that verify the <see cref="MainWindow"/> renders correctly
/// with <see cref="StubAdgSongCatalog"/> data – no DUNE.DAT or DNCDPRG.EXE required.
/// Uses <see cref="HeadlessUnitTestSession"/> to run assertions on the Avalonia UI thread.
/// </summary>
public sealed class MainWindowHeadlessTests
{
    private static HeadlessUnitTestSession Session =>
        HeadlessUnitTestSession.GetOrStartForAssembly(
            Assembly.GetExecutingAssembly());

    [Fact]
    public void Window_ShouldHaveCorrectTitle_WhenSongsArePresent()
    {
        Session.Dispatch(() =>
        {
            var vm = new MainWindowViewModel(new StubAdgSongCatalog());
            var window = new MainWindow { DataContext = vm };
            window.Show();
            window.Title.Should().Contain("AdgPlayer");
            window.Close();
        }, CancellationToken.None);
    }

    [Fact]
    public void Window_ShouldBindBrowserViewModel()
    {
        Session.Dispatch(() =>
        {
            var vm = new MainWindowViewModel(new StubAdgSongCatalog());
            var window = new MainWindow { DataContext = vm };
            window.Show();
            vm.Browser.Songs.Should().HaveCount(StubAdgSongCatalog.FakeSongs.Count);
            window.Close();
        }, CancellationToken.None);
    }

    [Fact]
    public void Window_TransportStatus_ShouldBeStoppedInitially()
    {
        Session.Dispatch(() =>
        {
            var vm = new MainWindowViewModel(new StubAdgSongCatalog());
            var window = new MainWindow { DataContext = vm };
            window.Show();
            vm.Transport.Status.Should().Be(AdgPlaybackStatus.Stopped);
            window.Close();
        }, CancellationToken.None);
    }

    [Fact]
    public void Window_AfterPlayCommand_StatusShouldBePlaying()
    {
        Session.Dispatch(() =>
        {
            var vm = new MainWindowViewModel(new StubAdgSongCatalog());
            var window = new MainWindow { DataContext = vm };
            window.Show();

            vm.Transport.PlayCommand.Execute(null);

            vm.Transport.Status.Should().Be(AdgPlaybackStatus.Playing);
            window.Close();
        }, CancellationToken.None);
    }

    [Fact]
    public void Window_SelectSecondSong_UpdatesSelectedSongName()
    {
        Session.Dispatch(() =>
        {
            var vm = new MainWindowViewModel(new StubAdgSongCatalog());
            var window = new MainWindow { DataContext = vm };
            window.Show();

            vm.Browser.SelectedSong = StubAdgSongCatalog.FakeSongs[1];

            vm.Browser.SelectedSongName.Should().Be(StubAdgSongCatalog.FakeSongs[1].Name);
            window.Close();
        }, CancellationToken.None);
    }
}
