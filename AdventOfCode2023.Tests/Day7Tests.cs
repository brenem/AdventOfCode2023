using FluentAssertions;

namespace AdventOfCode2023.Tests;

public class Day7Tests
{
    [Fact]
    public void Part1Test()
    {
        var day = new Day7();

        var input = new[]
        {
            "32T3K 765",
            "T55J5 684",
            "KK677 28",
            "KTJJT 220",
            "QQQJA 483"
        };

        var result = day.Part1(input);

        result.Should().Be(6440);
    }

    [Fact]
    public void Part2Test()
    {
        var day = new Day7();

        var input = new[]
        {
            "Time:      7  15   30",
            "Distance:  9  40  200"
        };

        var result = day.Part2(input);

        result.Should().Be(71503);
    }
}
