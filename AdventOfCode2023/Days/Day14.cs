using System.Text;

namespace AdventOfCode2023;
#nullable disable

public class Day14
{
    public int Part1(string[] input)
    {
        var map = input.Select(x => x.ToCharArray()).ToArray();

        MoveNorth(map);

        var rowLoad = map.Length;
        var loadSum = 0;

        foreach (var row in map)
        {
            var rockCount = row.Count(x => x == 'O');
            loadSum += rockCount * rowLoad;
            rowLoad--;
        }

        return loadSum;
    }

    public long Part2(string[] input)
    {
        var map = input.Select(x => x.ToCharArray()).ToArray();
        var cycleMap = new Dictionary<string, (int Counter, int LoadSum)>();

        var totalCycles = 1000000000;
        var cycleCount = 1;
        var lookup = 0;

        while (lookup == 0)
        {
            MoveNorth(map);
            MoveWest(map);
            MoveSouth(map);
            MoveEast(map);

            var mapString = map.ToReadableMap();

            if (cycleMap.TryGetValue(mapString, out (int Counter, int LoadSum) result) && result != cycleMap.Last().Value)
            {
                var counter = result.Counter;
                var loopLength = cycleCount - counter;
                lookup = counter + (totalCycles - counter) % loopLength;
                continue;
            }

            var rowLoad = map.Length;
            var loadSum = 0;

            foreach (var row in map)
            {
                var rocks = row.Count(x => x == 'O');
                loadSum += rocks * rowLoad;
                rowLoad--;
            }

            cycleMap[mapString] = (cycleCount, loadSum);
            cycleCount++;
        }

        foreach (var (counter, loadSum) in cycleMap.Select(x => x.Value))
        {
            if (counter == lookup)
                return loadSum;
        }

        return 0;
    }

    void MoveNorth(char[][] map)
    {
        var movement = new int[map[0].Length];

        int y = 0;
        foreach (var row in map)
        {
            for (var i = 0; i < row.Length; i++)
            {
                if (row[i] == '.')
                {
                    movement[i]++;
                    continue;
                }

                if (row[i] == '#')
                {
                    movement[i] = 0;
                    continue;
                }

                var destRow = y - movement[i];
                if (destRow != y)
                {
                    map[destRow][i] = 'O';
                    row[i] = '.';
                }
            }

            y++;
        }
    }

    void MoveWest(char[][] map)
    {
        foreach (var row in map)
        {
            var movement = 0;
            for (var i = 0; i < row.Length; i++)
            {
                if (row[i] == '.')
                {
                    movement++;
                    continue;
                }

                if (row[i] == '#')
                {
                    movement = 0;
                    continue;
                }

                var dest = i - movement;
                if (dest != i)
                {
                    row[dest] = 'O';
                    row[i] = '.';
                }
            }
        }
    }

    void MoveSouth(char[][] map)
    {
        var movement = new int[map[0].Length];

        for (var r = map.Length - 1; r >= 0; r--)
        {
            int y = r;
            var row = map[r];

            for (var i = 0; i < row.Length; i++)
            {
                if (row[i] == '.')
                {
                    movement[i]++;
                    continue;
                }

                if (row[i] == '#')
                {
                    movement[i] = 0;
                    continue;
                }

                var destRow = y + movement[i];
                if (destRow != y)
                {
                    map[destRow][i] = 'O';
                    row[i] = '.';
                }
            }
        }
    }

    void MoveEast(char[][] map)
    {
        foreach (var row in map)
        {
            var movement = 0;
            for (var i = row.Length - 1; i >= 0; i--)
            {
                if (row[i] == '.')
                {
                    movement++;
                    continue;
                }

                if (row[i] == '#')
                {
                    movement = 0;
                    continue;
                }

                var dest = i + movement;
                if (dest != i)
                {
                    row[dest] = 'O';
                    row[i] = '.';
                }
            }
        }
    }
}

static class Day14Extensions
{
    public static string ToReadableMap(this char[][] map)
    {
        var sb = new StringBuilder();
        foreach (var row in map)
        {
            sb.AppendLine(new string(row));
        }

        return sb.ToString();
    }
}