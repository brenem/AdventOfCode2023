using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace AdventOfCode2023;

public class Day5
{
    public long Part1(string[] input)
    {
        var seeds = input[0].Split("seeds: ")[1].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
        var seedToSoilMap = ParseMap(seeds, FindSection("seed-to-soil", input));
        var soilToFertilizerMap = ParseMap(seedToSoilMap.Select(x => x.Key), FindSection("soil-to-fertilizer", input));
        var fertilizerToWaterMap = ParseMap(soilToFertilizerMap.Select(x => x.Key), FindSection("fertilizer-to-water", input));
        var waterToLightMap = ParseMap(fertilizerToWaterMap.Select(x => x.Key), FindSection("water-to-light", input));
        var lightToTempMap = ParseMap(waterToLightMap.Select(x => x.Key), FindSection("light-to-temperature", input));
        var tempToHumidityMap = ParseMap(lightToTempMap.Select(x => x.Key), FindSection("temperature-to-humidity", input));
        var humidityToLocationMap = ParseMap(tempToHumidityMap.Select(x => x.Key), FindSection("humidity-to-location", input));

        var locations = seeds.Select(seed => FindLocation(seed, seedToSoilMap, soilToFertilizerMap, fertilizerToWaterMap, waterToLightMap, lightToTempMap, tempToHumidityMap, humidityToLocationMap));
        return locations.Min();
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

    private Dictionary<long, long> ParseMap(IEnumerable<long> leftIds, IEnumerable<string> sectionInput)
    {
        var map = new Dictionary<long, long>();

        foreach (var line in sectionInput)
        {
            var parameters = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
            var destStart = parameters[0];
            var srcStart = parameters[1];
            var length = parameters[2];

            var destRange = CreateRange(destStart, length).ToArray();
            var srcRange = CreateRange(srcStart, length).ToArray();

            for (var i = 0; i < srcRange.Length; i++)
            {
                var src = srcRange[i];

                long dest;
                if (i < destRange.Length)
                    dest = destRange[i];
                else
                    dest = src;

                map.Add(src, dest);
            }
        }

        var unmapped = leftIds.Except(map.Select(m => m.Key));
        foreach (var id in unmapped)
            map.Add(id, id);

        return map;
    }

    private IEnumerable<long> CreateRange(long start, long count)
    {
        var nums = new List<long>();

        var limit = start + count;
        while (start < limit)
        {
            nums.Add(start);
            start++;
        }

        return nums;
    }

    private long FindLocation(long seed,
        Dictionary<long, long> seedToSoilMap,
        Dictionary<long, long> soilToFertilizerMap,
        Dictionary<long, long> fertilizerToWaterMap,
        Dictionary<long, long> waterToLightMap,
        Dictionary<long, long> lightToTempMap,
        Dictionary<long, long> tempToHumidityMap,
        Dictionary<long, long> humidityToLocationMap)
    {
        var location = from ss in seedToSoilMap
                       join sf in soilToFertilizerMap on ss.Value equals sf.Key
                       join fw in fertilizerToWaterMap on sf.Value equals fw.Key
                       join wl in waterToLightMap on fw.Value equals wl.Key
                       join lt in lightToTempMap on wl.Value equals lt.Key
                       join th in tempToHumidityMap on lt.Value equals th.Key
                       join hl in humidityToLocationMap on th.Value equals hl.Key
                       where ss.Key == seed
                       select hl.Value;

        return location.SingleOrDefault();
    }
}
