using FluentAssertions;
using PostSharp.Patterns.Caching;
using PostSharp.Patterns.Caching.Backends;

namespace AdventOfCode2023.Tests;

public class Day12Tests
{
    [Fact]
    public void Part1Test()
    {
        CachingServices.DefaultBackend = new MemoryCachingBackend();

        var day = new Day12();

        var testInput = new[]
        {
            "???.### 1,1,3",
            ".??..??...?##. 1,1,3",
            "?#?#?#?#?#?#?#? 1,3,1,6",
            "????.#...#... 4,1,1",
            "????.######..#####. 1,6,5",
            "?###???????? 3,2,1"
        };

        var result = day.Part1(testInput);

        result.Should().Be(21);
    }

    [Fact]
    public void Part2Test()
    {
        CachingServices.DefaultBackend = new MemoryCachingBackend();

        var day = new Day12();

        var testInput = new[]
        {
            "???.### 1,1,3",
            ".??..??...?##. 1,1,3",
            "?#?#?#?#?#?#?#? 1,3,1,6",
            "????.#...#... 4,1,1",
            "????.######..#####. 1,6,5",
            "?###???????? 3,2,1"
        };

        var result = day.Part2(testInput);

        result.Should().Be(525152);
    }
}
