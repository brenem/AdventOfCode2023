using FluentAssertions;

namespace AdventOfCode2023.Tests;

public class Day14Tests
{
    [Fact]
    public void Part1Test()
    {
        var day = new Day14();

        var testInput = new[]
        {
            "O....#....",
            "O.OO#....#",
            ".....##...",
            "OO.#O....O",
            ".O.....O#.",
            "O.#..O.#.#",
            "..O..#O..O",
            ".......O..",
            "#....###..",
            "#OO..#...."
        };

        var result = day.Part1(testInput);

        result.Should().Be(136);
    }

    [Fact]
    public void Part2Test()
    {
        var day = new Day14();

        var testInput = new[]
        {
            "O....#....",
            "O.OO#....#",
            ".....##...",
            "OO.#O....O",
            ".O.....O#.",
            "O.#..O.#.#",
            "..O..#O..O",
            ".......O..",
            "#....###..",
            "#OO..#...."
        };

        var result = day.Part2(testInput);

        result.Should().Be(64);
    }
}
