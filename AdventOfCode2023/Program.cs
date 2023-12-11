// See https://aka.ms/new-console-template for more information
using System.Reflection;

Console.WriteLine("Hello, World!");

var day = 5;
var part = 1;

var input = File.ReadAllLines(Path.Combine("InputData", $"Day{day}.txt"));

var dayType = Assembly.GetExecutingAssembly().GetTypes().Single(x => x.Name == $"Day{day}");
var partMethod = dayType!.GetMethod($"Part{part}");

var dayInstance = Activator.CreateInstance(dayType!);

Console.WriteLine("Invoking...");
var result = partMethod!.Invoke(dayInstance, new[] { input });

Console.WriteLine($"Day{day} result: {result}");
