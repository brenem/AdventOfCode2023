namespace AdventOfCode2023;
#nullable disable

public class Day9
{
    public int Part1(string[] input)
    {
        var numberSets = input.Select(x => x.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()).ToArray();
        var nextNums = new List<int>();

        foreach (var numberSet in numberSets)
        {
            var sets = new List<int[]> { numberSet };

            var diffs = FindDiffs([.. numberSet]).ToArray();

            while (!diffs.All(x => x == 0))
            {
                sets.Add(diffs);
                diffs = FindDiffs(diffs).ToArray();
            }

            sets.Add(diffs);

            var diffSets = sets.AsEnumerable().Reverse().ToArray();

            for (var i = 0; i < diffSets.Length; i++)
            {
                var arr = diffSets[i];

                if (i == 0)
                {
                    Array.Resize(ref arr, arr.Length + 1);
                    arr[^1] = 0;
                }
                else
                {
                    var prevOfLast = diffSets[i - 1][^1];
                    var last = arr[^1];

                    Array.Resize(ref arr, arr.Length + 1);
                    arr[^1] = last + prevOfLast;
                }

                diffSets[i] = arr;
            }

            nextNums.Add(diffSets[^1][^1]);
        }

        return nextNums.Sum();
    }

    public int Part2(string[] input)
    {
        throw new NotImplementedException();
    }

    private IEnumerable<int> FindDiffs(int[] input)
    {
        for (var i = 1; i < input.Length; i++)
        {
            yield return input[i] - input[i - 1];
        }
    }
}
