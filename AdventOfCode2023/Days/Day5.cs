using System.Collections.Concurrent;

namespace AdventOfCode2023;

public class Day5
{
    public Task<uint> Part1(string[] input)
    {
        var seeds = input[0].Split("seeds: ")[1].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(uint.Parse).ToArray();
        return FindLowestLocation(seeds, input);
    }

    public async Task<uint> Part2(string[] input)
    {
        var seedValues = input[0].Split("seeds: ")[1].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(uint.Parse).ToArray();
        var seedRanges = GetSeedRanges(seedValues);

        var allSeeds = new List<uint>();
        foreach (var (Start, Length) in seedRanges)
        {
            uint[] seeds = new uint[Length];
            for (uint i = 0; i < Length; i++)
            {
                seeds[i] = Start + i;
            }

            allSeeds.AddRange(seeds);
        }

        Console.WriteLine("Got seeds. find lowest location id...");

        var locationId = await FindLowestLocation([.. allSeeds], input);

        return locationId;
    }

    private async Task<uint> FindLowestLocation(uint[] seeds, string[] input)
    {
        Console.WriteLine("seed to soil...");
        var seedToSoilMap = ParseMap(FindSection("seed-to-soil", input).ToArray());

        Console.WriteLine("soil to fertilizer...");
        var soilToFertilizerMap = ParseMap(FindSection("soil-to-fertilizer", input).ToArray());

        Console.WriteLine("fertilizer to water...");
        var fertilizerToWaterMap = ParseMap(FindSection("fertilizer-to-water", input).ToArray());

        Console.WriteLine("water to light...");
        var waterToLightMap = ParseMap(FindSection("water-to-light", input).ToArray());

        Console.WriteLine("light to temp...");
        var lightToTempMap = ParseMap(FindSection("light-to-temperature", input).ToArray());

        Console.WriteLine("temp to humidity...");
        var tempToHumidityMap = ParseMap(FindSection("temperature-to-humidity", input).ToArray());

        Console.WriteLine("humidity to location...");
        var humidityToLocationMap = ParseMap(FindSection("humidity-to-location", input).ToArray());

        var locations = new ConcurrentBag<uint>();

        await Parallel.ForEachAsync(seeds, (seed, ct) =>
        {
            var soilId = seedToSoilMap.FindDestination(seed);
            var fertilizerId = soilToFertilizerMap.FindDestination(soilId);
            var waterId = fertilizerToWaterMap.FindDestination(fertilizerId);
            var lightId = waterToLightMap.FindDestination(waterId);
            var tempId = lightToTempMap.FindDestination(lightId);
            var humidityId = tempToHumidityMap.FindDestination(tempId);
            var locationId = humidityToLocationMap.FindDestination(humidityId);

            locations.Add(locationId);

            return ValueTask.CompletedTask;
        });

        return locations.Min();
    }

    private IEnumerable<(uint Start, uint Length)> GetSeedRanges(uint[] seeds)
    {
        for (var i = 0; i < seeds.Length; i += 2)
        {
            var start = seeds[i];
            var length = seeds[i + 1];

            yield return (start, length);
        }
    }

    private IEnumerable<string> FindSection(string name, string[] input)
    {
        var index = input.ToList().FindIndex(x => x.StartsWith(name));
        string line = input[++index];

        while (!string.IsNullOrWhiteSpace(line.Trim()))
        {
            yield return line;

            ++index;
            if (index == input.Length)
                break;
            else
                line = input[index];
        }
    }

    private IEnumerable<MapRow> ParseMap(string[] sectionInput)
    {
        var maps = new List<MapRow>();

        for (var i = 0; i < sectionInput.Length; i++)
        {
            var line = sectionInput[i];
            var parameters = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(uint.Parse).ToArray();
            var destStart = parameters[0];
            var srcStart = parameters[1];
            var length = parameters[2];

            var mapRow = new MapRow(destStart, srcStart, length);
            maps.Add(mapRow);
        }

        return maps;
    }
}

public record MapRow(uint Dest, uint Src, uint Length)
{
    public uint? FindDest(uint srcValue)
    {
        if (srcValue < Src)
            return null;

        var srcEnd = Src + Length - 1;

        if (srcValue > srcEnd)
            return null;

        var diff = srcValue - Src;
        return Dest + diff;
    }
}

public static class Day5Extensions
{
    public static uint FindDestination(this IEnumerable<MapRow> maps, uint srcValue)
        => maps.Select(x => x.FindDest(srcValue)).FirstOrDefault(x => x != null) ?? srcValue;
}