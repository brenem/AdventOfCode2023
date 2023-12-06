// See https://aka.ms/new-console-template for more information
using AdventOfCode2023.Days;

Console.WriteLine("Hello, World!");

var input = File.ReadAllLines(Path.Combine("InputData", "Day2.txt"));

Console.WriteLine($"{nameof(Day2)} result: {new Day2().Part2(input)}");
