using FluentAssertions;

namespace AdventOfCode2023.Tests;

public class Day15Tests
{
    [Fact]
    public void Part1Test()
    {
        var day = new Day15();

        var testInput = "HASH";

        var result = day.CalculateHashSum(testInput);

        result.Should().Be(52);
    }

    [Fact]
    public void Part1Test2()
    {
        var day = new Day15();

        var testInput = new[] { "rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7" };

        var result = day.Part1(testInput);

        result.Should().Be(1320);
    }

    [Fact]
    public void Part2Test()
    {
        var day = new Day15();

        var testInput = new[] { "rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7" };

        var result = day.Part2(testInput);

        result.Should().Be(145);
    }
}
