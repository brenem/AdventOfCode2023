using System.Text;

namespace AdventOfCode2023;
#nullable disable

public class Day11
{
    public long Part1(string[] input)
    {
        var multiplier = 2;
        return CalculateWithMultipler(input, multiplier);
    }

    public long Part2(string[] input)
    {
        var multiplier = 1000000;
        return CalculateWithMultipler(input, multiplier);
    }

    public long CalculateWithMultipler(string[] input, int multiplier)
    {
        var image = input.Select(x => x.ToList()).ToList();

        var emptyRows = FindEmptyRows(image);
        var emptyCols = FindEmptyCols(image);
        var galaxies = FindGalaxies(image);
        var galaxyPairs = GetGalaxyPairs(galaxies.ToArray());

        var sumOfDistances = FindSumOfDistances(emptyRows, emptyCols, galaxyPairs, multiplier);

        return sumOfDistances;
    }

    IEnumerable<int> FindEmptyRows(List<List<char>> image)
    {
        for (var i = 0; i < image.Count; i++)
        {
            if (image[i].TrueForAll(x => x == '.'))
                yield return i;
        }
    }

    IEnumerable<int> FindEmptyCols(List<List<char>> image)
    {
        for (var i = 0; i < image[0].Count; i++)
        {
            if (image.TrueForAll(x => x[i] == '.'))
                yield return i;
        }
    }

    IEnumerable<Point> FindGalaxies(List<List<char>> image)
    {
        for (var r = 0; r < image.Count; r++)
        {
            for (var c = 0; c < image[r].Count; c++)
            {
                if (image[r][c] == '#')
                    yield return new Point(c, r);
            }
        }
    }

    long FindSumOfDistances(IEnumerable<int> emptyRows, IEnumerable<int> emptyCols, IEnumerable<(Point A, Point B)> galaxyPairs, int multiple = 2)
    {
        long sumOfDistances = 0;

        foreach (var pair in galaxyPairs)
        {
            var distance = pair.A.DistanceTo(pair.B);

            var yMin = Math.Min(pair.A.Y, pair.B.Y);
            var yMax = Math.Max(pair.A.Y, pair.B.Y);

            var emptyRowCount = emptyRows.Count(x => x >= yMin + 1 && x < yMax);

            var xMin = Math.Min(pair.A.X, pair.B.X);
            var xMax = Math.Max(pair.A.X, pair.B.X);

            var emptyColCount = emptyCols.Count(x => x >= xMin + 1 && x < xMax);

            distance = distance + (emptyRowCount + emptyColCount) * (multiple - 1);
            sumOfDistances += distance;
        }

        return sumOfDistances;
    }

    IEnumerable<(Point A, Point B)> GetGalaxyPairs(Point[] items)
    {
        for (var i = 0; i < items.Length; i++)
        {
            var curr = items[i];
            var start = i + 1;

            while (start < items.Length)
            {
                var other = items[start];
                start++;

                yield return (curr, other);
            }
        }
    }

    record Point(int X, int Y)
    {
        public int DistanceTo(Point that)
        {
            var xDiff = Math.Abs(that.X - X);
            var yDiff = Math.Abs(that.Y - Y);
            return xDiff + yDiff;
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}
public static class Day11Extensions
{
    public static string ToImageString(this List<List<char>> image)
    {
        var sb = new StringBuilder();
        foreach (var row in image)
        {
            sb.AppendLine(string.Join(string.Empty, row));
        }

        return sb.ToString();
    }
}