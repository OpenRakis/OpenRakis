namespace AdgPlayer.UnitTests.Tests;

using AdgPlayer.Audio;

using FluentAssertions;

using Xunit;

/// <summary>
/// TDD tests for <see cref="AdgDriverState"/> and <see cref="AdgChannelRoutingTable"/>.
/// </summary>
public sealed class AdgDriverStateTests
{
    [Fact]
    public void InitialStatus_ShouldBeStopped()
    {
        var sut = new AdgDriverState();
        sut.Status.Should().Be(AdgPlaybackStatus.Stopped);
    }

    [Fact]
    public void InitialDataOffset_ShouldBeZero()
    {
        var sut = new AdgDriverState();
        sut.DataOffset.Should().Be(0);
    }

    [Fact]
    public void InitialTickDelay_ShouldBeZero()
    {
        var sut = new AdgDriverState();
        sut.TickDelay.Should().Be(0);
    }

    [Fact]
    public void InitialIsLooping_ShouldBeFalse()
    {
        var sut = new AdgDriverState();
        sut.IsLooping.Should().BeFalse();
    }

    [Fact]
    public void Reset_ShouldRestoreAllFieldsToDefaults()
    {
        var sut = new AdgDriverState
        {
            DataOffset = 42,
            TickDelay = 10,
            IsLooping = true,
            Status = AdgPlaybackStatus.Playing,
        };

        sut.Reset();

        sut.DataOffset.Should().Be(0);
        sut.TickDelay.Should().Be(0);
        sut.IsLooping.Should().BeFalse();
        sut.Status.Should().Be(AdgPlaybackStatus.Stopped);
    }

    [Fact]
    public void ChannelRouting_Count_ShouldBeNine()
    {
        var sut = new AdgDriverState();
        sut.ChannelRouting.Count.Should().Be(9);
    }

    [Fact]
    public void ChannelRouting_Reset_ShouldSetAllChannelsToMinusOne()
    {
        var routing = new AdgChannelRoutingTable();
        routing[0] = 5;
        routing[3] = 2;

        routing.Reset();

        for (int i = 0; i < routing.Count; i++)
        {
            routing[i].Should().Be(-1, because: $"channel {i} should be unrouted after Reset()");
        }
    }
}
