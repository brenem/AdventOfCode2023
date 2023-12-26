using AdventOfCode2023.Models;

namespace AdventOfCode2023.Extensions;

public static class GridExtensions
{
    public static TNode? FindGridNode<TNode>(this GridLocation<int> location, IEnumerable<TNode> nodes) where TNode : GridNode
    {
        return nodes.SingleOrDefault(x => x.Location == location);
    }

    public static GridDirection ToOpposite(this GridDirection direction)
        => direction switch
        {
            GridDirection.North => GridDirection.South,
            GridDirection.East => GridDirection.West,
            GridDirection.South => GridDirection.North,
            GridDirection.West => GridDirection.East
        };

    public static GridDirection TurnLeft(this GridDirection direction)
    {
        return direction switch
        {
            GridDirection.North => GridDirection.West,
            GridDirection.South => GridDirection.East,
            GridDirection.East => GridDirection.North,
            GridDirection.West => GridDirection.South
        };
    }

    public static GridDirection TurnRight(this GridDirection direction)
    {
        return direction switch
        {
            GridDirection.North => GridDirection.East,
            GridDirection.South => GridDirection.West,
            GridDirection.East => GridDirection.South,
            GridDirection.West => GridDirection.North
        };
    }

    public static GridLocation<int> Move(this GridLocation<int> location, GridDirection direction)
    {
        return direction switch
        {
            GridDirection.North => new GridLocation<int>(location.Row - 1, location.Col),
            GridDirection.South => new GridLocation<int>(location.Row + 1, location.Col),
            GridDirection.East => new GridLocation<int>(location.Row, location.Col + 1),
            GridDirection.West => new GridLocation<int>(location.Row, location.Col - 1),
        };
    }

    public static double ToShoelaceArea(this IEnumerable<GridLocation<int>> locations)
    {
        var arrLocations = locations.ToArray();
        var n = arrLocations.Length;
        double a = 0.0;
        for (int i = 0; i < n - 1; i++)
        {
            a += arrLocations[i].Row * arrLocations[i + 1].Col - arrLocations[i + 1].Row * arrLocations[i].Col;
        }

        return Math.Abs(a + arrLocations[n - 1].Row * arrLocations[0].Col - arrLocations[0].Row * arrLocations[n - 1].Col) / 2.0;
    }

    public static double ToShoelaceArea(this IEnumerable<GridLocation<long>> locations)
    {
        var arrLocations = locations.ToArray();
        var n = arrLocations.Length;
        double a = 0.0;
        for (int i = 0; i < n - 1; i++)
        {
            a += arrLocations[i].Row * arrLocations[i + 1].Col - arrLocations[i + 1].Row * arrLocations[i].Col;
        }

        return Math.Abs(a + arrLocations[n - 1].Row * arrLocations[0].Col - arrLocations[0].Row * arrLocations[n - 1].Col) / 2.0;
    }
}
