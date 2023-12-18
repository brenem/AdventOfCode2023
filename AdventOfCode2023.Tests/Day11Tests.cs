using FluentAssertions;

namespace AdventOfCode2023.Tests;

public class Day11Tests
{
    [Fact]
    public void Part1Test()
    {
        var day = new Day11();

        var testInput = new[]
        {
            "...#......",
            ".......#..",
            "#.........",
            "..........",
            "......#...",
            ".#........",
            ".........#",
            "..........",
            ".......#..",
            "#...#....."
        };

        var result = day.Part1(testInput);

        result.Should().Be(374);
    }

    [Fact]
    public void Part2Test1()
    {
        var day = new Day11();

        var testInput = new[]
        {
            "...#......",
            ".......#..",
            "#.........",
            "..........",
            "......#...",
            ".#........",
            ".........#",
            "..........",
            ".......#..",
            "#...#....."
        };
        var multiplier = 10;

        var result = day.CalculateWithMultipler(testInput, multiplier);

        result.Should().Be(1030);
    }

    [Fact]
    public void Part2Test2()
    {
        var day = new Day11();

        var testInput = new[]
        {
            "...#......",
            ".......#..",
            "#.........",
            "..........",
            "......#...",
            ".#........",
            ".........#",
            "..........",
            ".......#..",
            "#...#....."
        };
        var multiplier = 100;

        var result = day.CalculateWithMultipler(testInput, multiplier);

        result.Should().Be(8410);
    }
}
