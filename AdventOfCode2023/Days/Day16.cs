using AdventOfCode2023.Extensions;
using AdventOfCode2023.Extensions.Extensions;
using AdventOfCode2023.Models;

namespace AdventOfCode2023;
#nullable disable

public class Day16
{
    public int Part1(string[] input)
    {
        //var tiles = input.SelectMany((row, rowIdx) => row.Select((tile, colIdx) => ParseGridNode(tile, rowIdx, colIdx, input.Length, input[0].Length))).ToList();
        var tiles = input.AsGridNodes().ToList();
        var startTile = tiles.Find(x => x.Location.Row == 0 && x.Location.Col == 0);
        var startDirection = GridDirection.East;

        return GetEnergizedCount(startDirection, startTile, tiles);
    }

    public async Task<int> Part2(string[] input)
    {
        //var tiles = input.SelectMany((row, rowIdx) => row.Select((tile, colIdx) => ParseGridNode(tile, rowIdx, colIdx, input.Length, input[0].Length))).ToList();
        var tiles = input.AsGridNodes().ToList();
        var tests = new[]
        {
            (GridDirection.South, tiles.Where(x => x.Location.Row == 0)),
            (GridDirection.North, tiles.Where(x => x.Location.Row == input.Length - 1)),
            (GridDirection.East, tiles.Where(x => x.Location.Col == 0)),
            (GridDirection.West, tiles.Where(x => x.Location.Col == input[0].Length - 1))
        };

        var maxEnergizedCount = 0;
        foreach (var (direction, testTiles) in tests)
        {
            var tasks = testTiles.Select(tile => Task.Run(() =>
            {
                Console.WriteLine($"counting: {direction}, starting with - {tile.Location}");
                return GetEnergizedCount(direction, tile, tiles);
            }));

            var directionCounts = await Task.WhenAll(tasks);
            if (directionCounts.Any())
            {
                var maxCount = directionCounts.Max();
                if (maxCount > maxEnergizedCount)
                    maxEnergizedCount = maxCount;
            }

            //foreach (var tile in testTiles)
            //{
            //    Console.WriteLine($"counting: {direction}, starting with - {tile.Location}");
            //    var count = GetEnergizedCount(direction, tile, tiles);
            //    if (count > maxEnergizedCount)
            //        maxEnergizedCount = count;
            //}
        }

        return maxEnergizedCount;
    }

    int GetEnergizedCount(GridDirection startDirection, GridNode startTile, IEnumerable<GridNode> tiles)
    {
        var visited = new List<Beam>
        {
            new(startDirection, startTile.Location)
        };

        Queue<(GridDirection DestDirection, GridNode DestTile)> queue = new();
        queue.Enqueue((startDirection, startTile));

        while (queue.Count > 0)
        {
            var (nextDir, nextTile) = queue.Dequeue();
            var newTiles = TraverseNode(nextDir, nextTile, tiles, visited);

            if (newTiles != null)
            {
                foreach (var tile in newTiles)
                {
                    queue.Enqueue(tile);
                }
            }
        }

        var energized = visited.Select(x => x.Location).Distinct();
        var energizedCount = energized.Count();
        return energizedCount;
    }

    IEnumerable<(GridDirection DestDirection, GridNode DestTile)> TraverseNode(GridDirection srcDir, GridNode srcTile, IEnumerable<GridNode> nodes, List<Beam> visited)
    {
        if (srcTile is null)
            yield break;

        var northNode = srcTile.North.FindGridNode(nodes);
        var southNode = srcTile.South.FindGridNode(nodes);
        var eastNode = srcTile.East.FindGridNode(nodes);
        var westNode = srcTile.West.FindGridNode(nodes);

        var srcChar = srcTile.Value;

        IEnumerable<(GridDirection DestDir, GridNode DestNode)> dests = new { srcDir, srcChar } switch
        {
            { srcDir: GridDirection.South, srcChar: '.' } or { srcDir: GridDirection.South, srcChar: '|' } => [(srcDir, southNode)],
            { srcDir: GridDirection.South, srcChar: '\\' } => [(GridDirection.East, eastNode)],
            { srcDir: GridDirection.South, srcChar: '/' } => [(GridDirection.West, westNode)],
            { srcDir: GridDirection.South, srcChar: '-' } => [(GridDirection.East, eastNode), (GridDirection.West, westNode)],

            { srcDir: GridDirection.North, srcChar: '.' } or { srcDir: GridDirection.North, srcChar: '|' } => [(srcDir, northNode)],
            { srcDir: GridDirection.North, srcChar: '\\' } => [(GridDirection.West, westNode)],
            { srcDir: GridDirection.North, srcChar: '/' } => [(GridDirection.East, eastNode)],
            { srcDir: GridDirection.North, srcChar: '-' } => [(GridDirection.East, eastNode), (GridDirection.West, westNode)],

            { srcDir: GridDirection.East, srcChar: '.' } or { srcDir: GridDirection.East, srcChar: '-' } => [(srcDir, eastNode)],
            { srcDir: GridDirection.East, srcChar: '\\' } => [(GridDirection.South, southNode)],
            { srcDir: GridDirection.East, srcChar: '/' } => [(GridDirection.North, northNode)],
            { srcDir: GridDirection.East, srcChar: '|' } => [(GridDirection.North, northNode), (GridDirection.South, southNode)],

            { srcDir: GridDirection.West, srcChar: '.' } or { srcDir: GridDirection.West, srcChar: '-' } => [(srcDir, westNode)],
            { srcDir: GridDirection.West, srcChar: '\\' } => [(GridDirection.North, northNode)],
            { srcDir: GridDirection.West, srcChar: '/' } => [(GridDirection.South, southNode)],
            { srcDir: GridDirection.West, srcChar: '|' } => [(GridDirection.North, northNode), (GridDirection.South, southNode)],
        };

        foreach (var (DestDir, DestNode) in dests)
        {
            if (DestNode != null)
            {
                if (visited.Exists(x => x.Direction == DestDir && x.Location == DestNode.Location))
                    yield break;

                visited.Add(new Beam(DestDir, DestNode.Location));

                yield return (DestDir, DestNode);
            }
        }
    }
}

record Beam(GridDirection Direction, GridLocation Location);

record TileLocation(int Row, int Col);

record Tile(char TileChar, TileLocation Location, TileLocation Top, TileLocation Bottom, TileLocation Left, TileLocation Right);