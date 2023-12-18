// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using System.Reflection;
using AdventOfCode2023;


Console.WriteLine("Hello, World!");

var day = 11;
var part = 2;

var input = File.ReadAllLines(Path.Combine("InputData", $"Day{day}.txt"));

var dayType = Assembly.GetExecutingAssembly().GetTypes().Single(x => x.Name == $"Day{day}");
var partMethod = dayType!.GetMethod($"Part{part}");

var dayInstance = Activator.CreateInstance(dayType!);

object? result;

if (partMethod!.ReturnType.BaseType == typeof(Task))
{
    Console.WriteLine("Invoking async...");

    var sw = Stopwatch.StartNew();
    result = await partMethod!.InvokeAsync(dayInstance!, (object)input);
    sw.Stop();

    Console.WriteLine("Finished in {0} ms", sw.ElapsedMilliseconds);
}
else
{
    Console.WriteLine("Invoking...");

    var sw = Stopwatch.StartNew();
    result = partMethod!.Invoke(dayInstance, new[] { input });
    sw.Stop();

    Console.WriteLine("Finished in {0} ms", sw.ElapsedMilliseconds);
}

Console.WriteLine($"Day{day} result: {result}");