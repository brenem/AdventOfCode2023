using System.Text;
using AdventOfCode2023.Models;

namespace AdventOfCode2023.Extensions;

public static class ArrayExtensions
{
    public static IEnumerable<GridNode> AsGridNodes(this IEnumerable<string> input)
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

    private static GridNode ParseGridNode(char value, int rowIdx, int colIdx, int rowLength, int colLength)
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

        GridLocation<int> tileLeft = null, tileRight = null, tileTop = null, tileBottom = null;

        if (colIdx == 0)
        {
            tileRight = new GridLocation<int>(rowIdx, colIdx + 1);

            if (prevRowIdx != null)
                tileTop = new GridLocation<int>(prevRowIdx.Value, colIdx);

            if (nextRowIdx != null)
                tileBottom = new GridLocation<int>(nextRowIdx.Value, colIdx);
        }
        else if (colIdx == colLength - 1)
        {
            tileLeft = new GridLocation<int>(rowIdx, colIdx - 1);

            if (prevRowIdx != null)
                tileTop = new GridLocation<int>(prevRowIdx.Value, colIdx);

            if (nextRowIdx != null)
                tileBottom = new GridLocation<int>(nextRowIdx.Value, colIdx);
        }
        else
        {
            tileRight = new GridLocation<int>(rowIdx, colIdx + 1);
            tileLeft = new GridLocation<int>(rowIdx, colIdx - 1);

            if (prevRowIdx != null)
                tileTop = new GridLocation<int>(prevRowIdx.Value, colIdx);

            if (nextRowIdx != null)
                tileBottom = new GridLocation<int>(nextRowIdx.Value, colIdx);
        }

        return new GridNode
        {
            Value = value,
            Location = new GridLocation<int>(rowIdx, colIdx),
            North = tileTop,
            South = tileBottom,
            East = tileRight,
            West = tileLeft
        };
    }
}
