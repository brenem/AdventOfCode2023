using FluentAssertions;

namespace AdventOfCode2023.Tests;

public class Day16Tests
{
    [Fact]
    public void Part1Test()
    {
        var day = new Day16();

        var testInput = new[]
        {
            ".|...\\....",
            "|.-.\\.....",
            ".....|-...",
            "........|.",
            "..........",
            ".........\\",
            "..../.\\\\..",
            ".-.-/..|..",
            ".|....-|.\\",
            "..//.|...."
        };

        var result = day.Part1(testInput);

        result.Should().Be(46);
    }

    [Fact]
    public async Task Part2Test()
    {
        var day = new Day16();

        var testInput = new[]
        {
            ".|...\\....",
            "|.-.\\.....",
            ".....|-...",
            "........|.",
            "..........",
            ".........\\",
            "..../.\\\\..",
            ".-.-/..|..",
            ".|....-|.\\",
            "..//.|...."
        };

        var result = await day.Part2(testInput);

        result.Should().Be(51);
    }
}
