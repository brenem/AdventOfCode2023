using AdventOfCode2023.Extensions;
using AdventOfCode2023.Models;

namespace AdventOfCode2023;
#nullable disable

public class Day18
{
    public long Part1(string[] input)
    {
        var turns = ParseInput(input);
        return FindTotalArea(turns);
    }

    public long Part2(string[] input)
    {
        var turns = ParseInput2(input);
        return FindTotalArea(turns);
    }

    long FindTotalArea(IEnumerable<(GridDirection Direction, long Moves)> turns)
    {
        var queue = new Queue<(GridDirection Direction, long Moves)>(turns);
        var location = new Coordinate<long>(0, 0);
        var visited = new List<Coordinate<long>>();
        var perimeter = 0L;

        while (queue.Count > 0)
        {
            var nextTurn = queue.Dequeue();
            perimeter += nextTurn.Moves;

            long row = location.Row;
            long col = location.Col;

            row = nextTurn.Direction switch
            {
                GridDirection.North => row - nextTurn.Moves,
                GridDirection.South => row + nextTurn.Moves,
                _ => row
            };

            col = nextTurn.Direction switch
            {
                GridDirection.East => col + nextTurn.Moves,
                GridDirection.West => col - nextTurn.Moves,
                _ => col
            };

            location = new Coordinate<long>(row, col);
            visited.Add(location);
        }

        var area = visited.ToShoelaceArea();
        return (long)(area + perimeter / 2 + 1);
    }

    IEnumerable<(GridDirection Direction, long Moves)> ParseInput(string[] input)
    {
        foreach (var line in input)
        {
            var parts = line.Split(' ');
            var direction = parts[0][0];
            var moves = int.Parse(parts[1]);

            var gridDirection = direction switch
            {
                'U' => GridDirection.North,
                'D' => GridDirection.South,
                'L' => GridDirection.West,
                'R' => GridDirection.East
            };

            yield return (gridDirection, moves);
        }
    }

    IEnumerable<(GridDirection Direction, long Moves)> ParseInput2(string[] input)
    {
        foreach (var line in input)
        {
            var parts = line.Split(' ');
            var enc = parts[2].Replace("#", string.Empty).Replace("(", string.Empty).Replace(")", string.Empty);
            var moves = long.Parse(enc[..5], System.Globalization.NumberStyles.HexNumber);
            var direction = enc[^1];

            var gridDirection = direction switch
            {
                '3' => GridDirection.North,
                '1' => GridDirection.South,
                '2' => GridDirection.West,
                '0' => GridDirection.East
            };

            yield return (gridDirection, moves);
        }
    }
}
