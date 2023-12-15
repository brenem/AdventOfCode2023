﻿using FluentAssertions;

namespace AdventOfCode2023.Tests;

public class Day15Tests
{
    private string[] _testInput = new[]
    {
        ""
    };

    [Fact]
    public void Part1Test()
    {
        var day = new Day15();

        var result = day.Part1(_testInput);

        result.Should().Be(6440);
    }

    [Fact]
    public void Part2Test()
    {
        var day = new Day15();

        var result = day.Part2(_testInput);

        result.Should().Be(5905);
    }
}