using FluentAssertions;

namespace AdventOfCode2023.Tests;

public class Day13Tests
{
    [Fact]
    public void Part1Test()
    {
        var day = new Day13();

        var testInput = new[]
        {
            "#.##..##.",
            "..#.##.#.",
            "##......#",
            "##......#",
            "..#.##.#.",
            "..##..##.",
            "#.#.##.#.",
            "",
            "#...##..#",
            "#....#..#",
            "..##..###",
            "#####.##.",
            "#####.##.",
            "..##..###",
            "#....#..#"
        };

        var result = day.Part1(testInput);

        result.Should().Be(405);
    }

    [Fact]
    public void Part1Test2()
    {
        var day = new Day13();

        var input = new[]
        {
            "###.##.##",
            "##.####.#",
            "##.#..#.#",
            "####..###",
            "....##...",
            "##.#..#.#",
            "...#..#..",
            "##..###.#",
            "##......#",
            "##......#",
            "..#.##.#.",
            "...#..#..",
            "##.####.#",
            "....##...",
            "...####..",
            "....##...",
            "##.####.#"
        };

        var lines = input.Select((x, i) => (i, x)).ToList();

        var result = day.FindLinesOfReflection(lines);

        result.Should().Be(10);
    }

    [Fact]
    public void Part2Test()
    {
        var day = new Day13();

        var testInput = new[]
        {
            "#.##..##.",
            "..#.##.#.",
            "##......#",
            "##......#",
            "..#.##.#.",
            "..##..##.",
            "#.#.##.#.",
            "",
            "#...##..#",
            "#....#..#",
            "..##..###",
            "#####.##.",
            "#####.##.",
            "..##..###",
            "#....#..#"
        };

        var result = day.Part2(testInput);

        result.Should().Be(400);
    }
}
