using System.Text;
using AdventOfCode2023.Models;

namespace AdventOfCode2023.Extensions;

public static class ArrayExtensions
{
    public static IEnumerable<GridTile> AsGridTiles(this IEnumerable<string> input)
    {
        return input.SelectMany((row, rowIdx) =>
            row.Select((col, colIdx) =>
                ParseGridNode(col, rowIdx, colIdx, input.Count(), input.ElementAt(0).Length)));
    }

    public static string ToReadableMap(this char[][] map)
    {
        var sb = new StringBuilder();
        foreach (var row in map)
        {
            sb.AppendLine(new string(row));
        }

        return sb.ToString();
    }

    private static GridTile ParseGridNode(char value, int rowIdx, int colIdx, int rowLength, int colLength)
    {
        int? prevRowIdx = null;
        int? nextRowIdx = null;

        if (rowIdx == 0)
        {
            nextRowIdx = rowIdx + 1;
        }
        else if (rowIdx == rowLength - 1)
        {
            prevRowIdx = rowIdx - 1;
        }
        else
        {
            prevRowIdx = rowIdx - 1;
            nextRowIdx = rowIdx + 1;
        }

        Coordinate<int> tileLeft = null, tileRight = null, tileTop = null, tileBottom = null;

        if (colIdx == 0)
        {
            tileRight = new Coordinate<int>(rowIdx, colIdx + 1);

            if (prevRowIdx != null)
                tileTop = new Coordinate<int>(prevRowIdx.Value, colIdx);

            if (nextRowIdx != null)
                tileBottom = new Coordinate<int>(nextRowIdx.Value, colIdx);
        }
        else if (colIdx == colLength - 1)
        {
            tileLeft = new Coordinate<int>(rowIdx, colIdx - 1);

            if (prevRowIdx != null)
                tileTop = new Coordinate<int>(prevRowIdx.Value, colIdx);

            if (nextRowIdx != null)
                tileBottom = new Coordinate<int>(nextRowIdx.Value, colIdx);
        }
        else
        {
            tileRight = new Coordinate<int>(rowIdx, colIdx + 1);
            tileLeft = new Coordinate<int>(rowIdx, colIdx - 1);

            if (prevRowIdx != null)
                tileTop = new Coordinate<int>(prevRowIdx.Value, colIdx);

            if (nextRowIdx != null)
                tileBottom = new Coordinate<int>(nextRowIdx.Value, colIdx);
        }

        return new GridTile
        {
            Value = value,
            Location = new Coordinate<int>(rowIdx, colIdx),
            North = tileTop,
            South = tileBottom,
            East = tileRight,
            West = tileLeft
        };
    }
}
