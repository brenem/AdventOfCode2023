using System.Numerics;
#nullable disable

namespace AdventOfCode2023.Models;

public record Coordinate<T>(T Row, T Col) where T : ISignedNumber<T>
{
    public static Coordinate<T> operator +(Coordinate<T> a, Coordinate<T> b) => new(a.Row + b.Row, a.Col + b.Col);
}

public class GridTile
{
    public char Value { get; set; }
    public Coordinate<int> Location { get; set; }
    public Coordinate<int> North { get; set; }
    public Coordinate<int> South { get; set; }
    public Coordinate<int> East { get; set; }
    public Coordinate<int> West { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is GridTile n && Value == n.Value && Location == n.Location;
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