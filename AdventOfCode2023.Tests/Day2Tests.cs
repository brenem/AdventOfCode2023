using AdventOfCode2023.Days;
using FluentAssertions;

namespace AdventOfCode2023.Tests
{
    public class Day2Tests
    {
        [Fact]
        public void Part1Tests()
        {
            var day = new Day2();

            var cubeData = new CubeData { Red = 12, Green = 13, Blue = 14 };
            var input = new string[]
            {
                "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green",
                "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue",
                "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red",
                "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red",
                "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green"
            };

            var result = day.Part1(cubeData, input);

            result.Should().Be(8);
        }

        [Fact]
        public void Part2Tests()
        {
            var day = new Day2();

            var input = new string[]
            {
                "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green",
                "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue",
                "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red",
                "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red",
                "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green"
            };

            var result = day.Part2(input);

            result.Should().Be(2286);
        }
    }
}
