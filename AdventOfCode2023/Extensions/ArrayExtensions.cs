using System.Text;

namespace AdventOfCode2023.Extensions;

public static class ArrayExtensions
{
    public static string ToReadableMap(this char[][] map)
    {
        var sb = new StringBuilder();
        foreach (var row in map)
        {
            sb.AppendLine(new string(row));
        }

        return sb.ToString();
    }
}
