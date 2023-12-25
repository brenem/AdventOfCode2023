using AdventOfCode2023.Models;

namespace AdventOfCode2023.Extensions.Extensions;

public static class GridExtensions
{
    public static T? FindGridNode<T>(this GridLocation location, IEnumerable<T> nodes) where T : GridNode
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

    public static GridLocation Move(this GridLocation location, GridDirection direction)
    {
        return direction switch
        {
            GridDirection.North => new GridLocation(location.Row - 1, location.Col),
            GridDirection.South => new GridLocation(location.Row + 1, location.Col),
            GridDirection.East => new GridLocation(location.Row, location.Col + 1),
            GridDirection.West => new GridLocation(location.Row, location.Col - 1),
        };
    }
}
