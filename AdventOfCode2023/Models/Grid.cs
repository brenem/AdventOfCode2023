namespace AdventOfCode2023.Models;

public record GridLocation<T>(T Row, T Col);

public class GridNode
{
    public char Value { get; set; }
    public GridLocation<int> Location { get; set; }
    public GridLocation<int> North { get; set; }
    public GridLocation<int> South { get; set; }
    public GridLocation<int> East { get; set; }
    public GridLocation<int> West { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is GridNode n && Value == n.Value && Location == n.Location;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode() ^ Location.GetHashCode();
    }

    public override string ToString()
    {
        return $"{Value}, ({Location.Row}, {Location.Col}), N: ({North?.Row}, {North?.Col}), S: ({South?.Row}, {South?.Col}), E: ({East?.Row}, {East?.Col}), W: ({West?.Row}, {West?.Col})";
    }
}

public enum GridDirection
{
    North,
    East,
    South,
    West
}