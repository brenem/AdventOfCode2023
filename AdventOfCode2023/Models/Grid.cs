namespace AdventOfCode2023.Models;

public record GridLocation(int Row, int Col);

public class GridNode
{
    public char GridChar { get; set; }
    public GridLocation Location { get; set; }
    public GridLocation Top { get; set; }
    public GridLocation Bottom { get; set; }
    public GridLocation Right { get; set; }
    public GridLocation Left { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is GridNode n && GridChar == n.GridChar && Location == n.Location;
    }

    public override int GetHashCode()
    {
        return GridChar.GetHashCode() ^ Location.GetHashCode();
    }

    public override string ToString()
    {
        return $"'{GridChar}', ({Location.Row}, {Location.Col}), N: ({Top?.Row}, {Top?.Col}), S: ({Bottom?.Row}, {Bottom?.Col}), E: ({Right?.Row}, {Right?.Col}), W: ({Left?.Row}, {Left?.Col})";
    }
}

[Flags]
public enum GridDirection
{
    North,
    South,
    East,
    West
}