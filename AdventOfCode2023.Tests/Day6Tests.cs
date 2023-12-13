using FluentAssertions;

namespace AdventOfCode2023.Tests;

public class Day6Tests
{
    [Fact]
    public void Part1Test()
    {
        var day = new Day6();

        var input = new[]
        {
            "Time:      7  15   30",
            "Distance:  9  40  200"
        };

        var result = day.Part1(input);

        result.Should().Be(288);
    }

    [Fact]
    public void Part2Test()
    {
        var day = new Day6();

        var input = new[]
        {
            "Time:      7  15   30",
            "Distance:  9  40  200"
        };

        var result = day.Part2(input);

        result.Should().Be(71503);
    }
}
