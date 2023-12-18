using FluentAssertions;

namespace AdventOfCode2023.Tests;

public class Day10Tests
{
    [Fact]
    public void Part1Test()
    {
        var day = new Day10();

        var input = new[]
        {
            ".....",
            ".S-7.",
            ".|.|.",
            ".L-J.",
            "....."
        };

        var result = day.Part1(input);

        result.Should().Be(4);
    }

    [Fact]
    public void Part1Test2()
    {
        var day = new Day10();

        var input = new[]
        {
            "..F7.",
            ".FJ|.",
            "SJ.L7",
            "|F--J",
            "LJ..."
        };

        var result = day.Part1(input);

        result.Should().Be(8);
    }

    [Fact]
    public void Part2Test1()
    {
        var day = new Day10();

        var input1 = new[]
        {
            "...........",
            ".S-------7.",
            ".|F-----7|.",
            ".||.....||.",
            ".||.....||.",
            ".|L-7.F-J|.",
            ".|..|.|..|.",
            ".L--J.L--J.",
            "..........."
        };

        var input2 = new[]
        {
            "..........",
            ".S------7.",
            ".|F----7|.",
            ".||....||.",
            ".||....||.",
            ".|L-7F-J|.",
            ".|..||..|.",
            ".L--JL--J.",
            ".........."
        };

        var result1 = day.Part2(input1);
        var result2 = day.Part2(input2);

        result1.Should().Be(4);
        result2.Should().Be(4);
    }

    [Fact]
    public void Part2Test2()
    {
        var day = new Day10();

        var input = new[]
        {
            "...........",
            ".S-------7.",
            ".|F-----7|.",
            ".||.....||.",
            ".||.....||.",
            ".|L-7.F-J|.",
            ".|..|.|..|.",
            ".L--J.L--J.",
            "..........."
        };

        var result = day.Part2(input);

        result.Should().Be(4);
    }

    [Fact]
    public void Part2Test3()
    {
        var day = new Day10();

        var input = new[]
        {
            ".F----7F7F7F7F-7....",
            ".|F--7||||||||FJ....",
            ".||.FJ||||||||L7....",
            "FJL7L7LJLJ||LJ.L-7..",
            "L--J.L7...LJS7F-7L7.",
            "....F-J..F7FJ|L7L7L7",
            "....L7.F7||L7|.L7L7|",
            ".....|FJLJ|FJ|F7|.LJ",
            "....FJL-7.||.||||...",
            "....L---J.LJ.LJLJ..."
        };

        var result = day.Part2(input);

        result.Should().Be(8);
    }

    [Fact]
    public void Part2Test4()
    {
        var day = new Day10();

        var input = new[]
        {
            "FF7FSF7F7F7F7F7F---7",
            "L|LJ||||||||||||F--J",
            "FL-7LJLJ||||||LJL-77",
            "F--JF--7||LJLJ7F7FJ-",
            "L---JF-JLJ.||-FJLJJ7",
            "|F|F-JF---7F7-L7L|7|",
            "|FFJF7L7F-JF7|JL---7",
            "7-L-JL7||F7|L7F-7F7|",
            "L.L7LFJ|||||FJL7||LJ",
            "L7JLJL-JLJLJL--JLJ.L"
        };

        var result = day.Part2(input);

        result.Should().Be(10);
    }
}
