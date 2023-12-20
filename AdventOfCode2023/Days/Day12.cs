using PostSharp.Patterns.Caching;

namespace AdventOfCode2023;
#nullable disable

public class Day12
{
    public long Part1(string[] input)
    {
        long output = 0;

        foreach (var item in input)
        {
            var split = item.Split();
            var record = split[0];
            var groups = split[1].Split(',').Select(int.Parse).ToArray();

            output += Calc(record, groups);
        }

        return output;
    }

    public long Part2(string[] input)
    {
        long output = 0;

        foreach (var item in input)
        {
            var split = item.Split();
            var record = split[0];
            var groups = split[1].Split(',').Select(int.Parse).ToArray();

            record = string.Join('?', Enumerable.Repeat(record, 5));
            groups = Enumerable.Repeat(groups, 5).SelectMany(x => x).ToArray();
            output += Calc(record, groups);

            Console.WriteLine(new string('-', 10));
        }

        return output;
    }

    [Cache]
    long Calc(string record, int[] groups)
    {
        if (groups == null || groups.Length == 0)
        {
            if (record.Contains('#'))
                return 0;
            else
                return 1;
        }

        if (string.IsNullOrWhiteSpace(record))
        {
            if (groups.Length == 0)
                return 1;
            else
                return 0;
        }

        long result = 0;

        if (".?".Contains(record[0]))
        {
            var newRecord = record[1..];
            result += Calc(newRecord, groups);
        }

        if ("#?".Contains(record[0]))
        {
            if (groups[0] <= record.Length && !record[..groups[0]].Contains('.') && (groups[0] == record.Length || record[groups[0]] != '#'))
            {
                var recordIdx = groups[0] + 1;
                var newRecord = record[(recordIdx > record.Length ? groups[0] : recordIdx)..];
                var newGroups = groups[1..];

                result += Calc(newRecord, newGroups);
            }
        }

        Console.WriteLine($"{record} [{string.Join(", ", groups)}] -> {result}");

        return result;
    }
}
