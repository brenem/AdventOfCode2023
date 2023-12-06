using AdventOfCode2023.Days;
using FluentAssertions;

namespace AdventOfCode2023.Tests
{
    public class Day3Tests
    {
        [Fact]
        public void Part1Tests()
        {
            var day = new Day3();

            var input = new[]
            {
                "467..114..",
                "...*......",
                "..35..633.",
                "......#...",
                "617*......",
                ".....+.58.",
                "..592.....",
                "......755.",
                "...$.*....",
                ".664.598.."
            };

            var result = day.Part1(input);

            result.Should().Be(4361);
        }
    }
}
