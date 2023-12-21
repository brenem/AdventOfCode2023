namespace AdventOfCode2023;
#nullable disable

public class Day13
{
    public long Part1(string[] input)
    {
        long total = 0;

        var patterns = ParseInput(input).ToList();

        bool CheckHorizontal(List<string> pattern, int row)
        {
            for (int i = row - 1, j = row; i >= 0 && j < pattern.Count; i--, j++)
            {
                if (pattern[i] != pattern[j])
                    return false;
            }

            return true;
        }

        foreach (var pattern in patterns)
        {
            var horizontalFound = false;

            for (var i = 1; i < pattern.Count; i++)
            {
                if (CheckHorizontal(pattern, i))
                {
                    total += 100 * i;
                    horizontalFound = true;
                    break;
                }
            }

            if (horizontalFound)
                continue;

            var pivoted = Pivot(pattern);
            for (var i = 1; i <= pivoted.Count; i++)
            {
                if (CheckHorizontal(pivoted, i))
                {
                    total += i;
                    break;
                }
            }
        }

        return total;
    }

    public long Part2(string[] input)
    {
        long total = 0;

        var patterns = ParseInput(input).ToList();

        bool CheckHorizontal(List<string> pattern, int row)
        {
            int? smudgeRow = null, smudgeCol = null;

            for (int i = row - 1, j = row; i >= 0 && j < pattern.Count; i--, j++)
            {
                string pi = pattern[i], pj = pattern[j];
                for (int k = 0; k < pi.Length; k++)
                {
                    if (pi[k] != pj[k])
                    {
                        if (smudgeRow is not null)
                            return false;
                        smudgeRow = i;
                    }
                }
            }

            return smudgeRow != null;
        }

        foreach (var pattern in patterns)
        {
            var horizontalFound = false;

            for (var i = 1; i < pattern.Count; i++)
            {
                if (CheckHorizontal(pattern, i))
                {
                    total += 100 * i;
                    horizontalFound = true;
                    break;
                }
            }

            if (horizontalFound)
                continue;

            var pivoted = Pivot(pattern);
            for (var i = 1; i <= pivoted.Count; i++)
            {
                if (CheckHorizontal(pivoted, i))
                {
                    total += i;
                    break;
                }
            }
        }

        return total;
    }

    IEnumerable<List<string>> ParseInput(string[] input)
    {
        var lastEmptyIndex = 0;

        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] == string.Empty)
            {
                yield return [.. input[lastEmptyIndex..i]];
                lastEmptyIndex = i + 1;
            }

            if ((i + 1) == input.Length)
                yield return [.. input[lastEmptyIndex..(i + 1)]];
        }
    }

    public long FindLinesOfReflection(List<(int Idx, string Line)> input)
    {
        var horizontalLines = 0;
        var verticalLines = 0;

        var horizontalGroups = input.GroupBy(x => x.Line);
        var horizontalMatches = horizontalGroups.Where(x => x.Count() >= 2).ToList();
        var hasHorizontalReflection = true;
        int? horizontalTopIndex = null;

        for (var i = 1; i < input.Count; i += 2)
        {
            var matches = horizontalMatches.Where(x => Math.Abs(x.ElementAt(0).Idx - x.ElementAt(1).Idx) == i);
            if (matches.Any())
            {
                if (i == 1)
                {
                    horizontalTopIndex = matches.SelectMany(x => x.Select(g => g)).OrderBy(x => x.Idx).First().Idx;
                }
                else
                {
                    if (matches.Any(x => x.ElementAt(0).Idx == 0 || x.ElementAt(1).Idx == input.Count - 1))
                        break;
                }
            }
            else
            {
                hasHorizontalReflection = false;
                break;
            }
        }

        if (hasHorizontalReflection && horizontalTopIndex != null)
        {
            return (horizontalTopIndex.Value + 1) * 100;
        }

        var pivoted = PivotIndexed(input.Select(x => x.Line).ToList());
        var verticalGroups = pivoted.GroupBy(x => x.Line);
        var verticalMatches = verticalGroups.Where(x => x.Count() >= 2).ToList();
        var hasVerticalReflection = true;
        int? verticalTopIndex = null;

        for (var i = 1; i < input.Count; i += 2)
        {
            var matches = verticalMatches.Where(x => Math.Abs(x.ElementAt(0).Idx - x.ElementAt(1).Idx) == i);
            if (matches.Any())
            {
                if (i == 1)
                {
                    verticalTopIndex = matches.SelectMany(x => x.Select(g => g)).OrderBy(x => x.Idx).First().Idx;
                }
                else
                {
                    if (matches.Any(x => x.ElementAt(0).Idx == 0 || x.ElementAt(1).Idx == input.Count - 1))
                        break;
                }
            }
            else
            {
                hasVerticalReflection = false;
                break;
            }
        }

        if (hasVerticalReflection && verticalTopIndex != null)
        {
            return verticalTopIndex.Value + 1;
        }

        return 0;
    }

    List<(int Idx, string Line)> PivotIndexed(List<string> input)
    {
        var length = input[0].Length;
        var retVal = new List<(int Idx, string Line)>();

        for (int i = 0; i < length; i++)
        {
            retVal.Add((i, new string(input.Select(x => x[i]).ToArray())));
        }

        return retVal;
    }

    List<string> Pivot(List<string> input)
    {
        return PivotIndexed(input).Select(x => x.Line).ToList();
    }
}
