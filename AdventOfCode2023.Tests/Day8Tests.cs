using FluentAssertions;

namespace AdventOfCode2023.Tests;

public class Day8Tests
{
    [Fact]
    public void Part1Test()
    {
        var day = new Day8();

        var result = day.Part1(new[]
        {
            "RL",
            "",
            "AAA = (BBB, CCC)",
            "BBB = (DDD, EEE)",
            "CCC = (ZZZ, GGG)",
            "DDD = (DDD, DDD)",
            "EEE = (EEE, EEE)",
            "GGG = (GGG, GGG)",
            "ZZZ = (ZZZ, ZZZ)"
        });

        result.Should().Be(2);
    }

    [Fact]
    public void Part1Test2()
    {
        var day = new Day8();

        var result = day.Part1(new string[]
        {
            "LLR",
            "",
            "AAA = (BBB, BBB)",
            "BBB = (AAA, ZZZ)",
            "ZZZ = (ZZZ, ZZZ)"
        });

        result.Should().Be(6);
    }

    [Fact]
    public void Part2Test()
    {
        var day = new Day8();

        var result = day.Part2(new string[]
        {
            "LR",
            "",
            "11A = (11B, XXX)",
            "11B = (XXX, 11Z)",
            "11Z = (11B, XXX)",
            "22A = (22B, XXX)",
            "22B = (22C, 22C)",
            "22C = (22Z, 22Z)",
            "22Z = (22B, 22B)",
            "XXX = (XXX, XXX)"
        });

        result.Should().Be(6);
    }
}
