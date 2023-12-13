namespace AdventOfCode2023;

public class Day6
{
    public uint Part1(string[] input)
    {
        var times = input[0].Replace("Time:", "")
            .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Select(uint.Parse)
            .ToArray();

        var distances = input[1].Replace("Distance:", "")
            .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Select(uint.Parse)
            .ToArray();

        var stats = times.Select((time, i) => new RaceStatsUint(time, distances[i]));
        var wins = stats.Select(x => x.CountWaysToWin());
        var marginOfError = wins.Aggregate((a, b) => a * b);
        return marginOfError;
    }

    public uint Part2(string[] input)
    {
        var time = long.Parse(string.Join(string.Empty, input[0].Replace("Time:", "")
            .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse)));

        var distance = long.Parse(string.Join(string.Empty, input[1].Replace("Distance:", "")
            .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse)));

        var stats = new RaceStatsLong(time, distance);
        var wins = stats.CountWaysToWin();
        return wins;
    }
}

public record RaceStatsUint(uint Time, uint Distance)
{
    public uint CountWaysToWin()
    {
        var count = 0u;

        for (var holdTime = 1; holdTime < Time - 1; holdTime++)
        {
            var timeToTravel = Time - holdTime;
            var distanceLimit = timeToTravel * holdTime;
            if (distanceLimit > Distance)
                count++;
        }

        return count;
    }
}

public record RaceStatsLong(long Time, long Distance)
{
    public uint CountWaysToWin()
    {
        var count = 0u;

        for (var holdTime = 1; holdTime < Time - 1; holdTime++)
        {
            var timeToTravel = Time - holdTime;
            var distanceLimit = timeToTravel * holdTime;
            if (distanceLimit > Distance)
                count++;
        }

        return count;
    }
}