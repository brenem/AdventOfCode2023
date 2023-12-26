using FluentAssertions;

namespace AdventOfCode2023.Tests;

public class Day18Tests
{
    [Fact]
    public void Part1Test()
    {
        var day = new Day18();

        var testInput = new[]
        {
            "R 6 (#70c710)",
            "D 5 (#0dc571)",
            "L 2 (#5713f0)",
            "D 2 (#d2c081)",
            "R 2 (#59c680)",
            "D 2 (#411b91)",
            "L 5 (#8ceee2)",
            "U 2 (#caa173)",
            "L 1 (#1b58a2)",
            "U 2 (#caa171)",
            "R 2 (#7807d2)",
            "U 3 (#a77fa3)",
            "L 2 (#015232)",
            "U 2 (#7a21e3)"
        };

        var result = day.Part1(testInput);

        result.Should().Be(62);
    }

    [Fact]
    public void Part2Test()
    {
        var day = new Day18();

        var testInput = new[]
        {
            "R 6 (#70c710)",
            "D 5 (#0dc571)",
            "L 2 (#5713f0)",
            "D 2 (#d2c081)",
            "R 2 (#59c680)",
            "D 2 (#411b91)",
            "L 5 (#8ceee2)",
            "U 2 (#caa173)",
            "L 1 (#1b58a2)",
            "U 2 (#caa171)",
            "R 2 (#7807d2)",
            "U 3 (#a77fa3)",
            "L 2 (#015232)",
            "U 2 (#7a21e3)"
        };

        var result = day.Part2(testInput);

        result.Should().Be(952408144115);
    }
}
