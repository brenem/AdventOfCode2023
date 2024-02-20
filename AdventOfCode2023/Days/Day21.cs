using AdventOfCode2023.Extensions;
using AdventOfCode2023.Models;

namespace AdventOfCode2023;
#nullable disable

public class Day21
{
    private readonly List<Coordinate<int>> _nsew = new()
    {
        new(-1, 0), new(1,0), new(0, -1), new(0, 1)
    };

    public long Part1(string[] input)
    {
        return CountSteps(65, input);
    }

    public long Part2(string[] input)
    {
        return CountSteps(26501365, input);
    }

    public long CountSteps(long limit, string[] input)
    {
        var tiles = input.AsGridTiles();
        var visited = new Dictionary<Coordinate<int>, long>();
        var queue = new Queue<(Coordinate<int> Location, long Steps)>();

        var startLocation = tiles.Single(x => x.Value == 'S').Location;
        queue.Enqueue((startLocation, 0));

        while (queue.Count > 0)
        {
            var (loc, steps) = queue.Dequeue();

            if (visited.ContainsKey(loc))
                continue;

            if (steps == limit + 1)
                break;

            visited[loc] = steps;

            foreach (var coor in _nsew)
            {
                var newLoc = loc + coor;
                if (tiles.Any(x => x.Location == newLoc) && ".S".Contains(tiles.Single(x => x.Location == newLoc).Value))
                    queue.Enqueue((newLoc, steps + 1));
            }
        }

        long parity = 0;
        return visited.Values.Count(x => x % 2 == parity);
    }
}
