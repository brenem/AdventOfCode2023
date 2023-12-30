using FluentAssertions;

namespace AdventOfCode2023.Tests;

public class Day20Tests
{
    [Fact]
    public void Part1Test()
    {
        var day = new Day20();

        var testInput = new[]
        {
            "broadcaster -> a, b, c",
            "%a -> b",
            "%b -> c",
            "%c -> inv",
            "&inv -> a"
        };

        var result = day.Part1(testInput);

        result.Should().Be(32000000);
    }

    [Fact]
    public void Part1Test2()
    {
        var day = new Day20();

        var testInput = new[]
        {
            "broadcaster -> a",
            "%a -> inv, con",
            "&inv -> b",
            "%b -> con",
            "&con -> output"
        };

        var result = day.Part1(testInput);

        result.Should().Be(11687500);
    }

    [Fact]
    public void Part1Test3()
    {
        var day = new Day20();

        var testInput = new[]
        {
            "broadcaster -> a",
            "%a -> output",
        };

        var result = day.Part1(testInput);

        result.Should().Be(2000000);
    }

    [Fact]
    public void Part2Test()
    {
        var day = new Day20();

        var testInput = new[]
        {
            ""
        };

        var result = day.Part2(testInput);

        result.Should().Be(5905);
    }
}
