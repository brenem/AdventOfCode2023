using FluentAssertions;

namespace AdventOfCode2023.Tests;

public class Day9Tests
{
    private static readonly string[] input =
        [
            "0 3 6 9 12 15",
            "1 3 6 10 15 21",
            "10 13 16 21 30 45"
        ];

    [Fact]
    public void Part1Test()
    {
        var day = new Day9();

        var result = day.Part1(input);

        result.Should().Be(114);
    }

    [Fact]
    public void Part2Test()
    {
        var day = new Day9();

        var result = day.Part2(input);

        result.Should().Be(5905);
    }
}
