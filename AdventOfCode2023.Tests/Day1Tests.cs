using AdventOfCode2023.Days;
using FluentAssertions;

namespace AdventOfCode2023.Tests
{
    public class Day1Tests
    {
        [Fact]
        public void Part1Test()
        {
            var day = new Day1();

            var input = new string[]
            {
                "1abc2",
                "pqr3stu8vwx",
                "a1b2c3d4e5f",
                "treb7uchet"
            };

            var result = day.Part1(input);

            result.Should().Be(142);
        }

        [Fact]
        public void Part2Test()
        {
            var day = new Day1();

            var input = new string[]
            {
                "two1nine",
                "eightwothree",
                "abcone2threexyz",
                "xtwone3four",
                "4nineeightseven2",
                "zoneight234",
                "7pqrstsixteen",
            };

            var result = day.Part2(input);

            result.Should().Be(281);
        }
    }
}