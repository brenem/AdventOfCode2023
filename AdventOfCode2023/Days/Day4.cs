using System.Runtime.CompilerServices;
using System.Xml.Xsl;

namespace AdventOfCode2023.Days;

public class Day4
{
    public int Part1(string[] input)
    {
        return input.Select(x => NumWins(x).NumWins).Where(x => x > 0).Select(CalculateWinnings).Sum();
    }

    public int Part2(string[] input)
    {
        var wins = new Dictionary<int, int>();

        for (var i = 0; i < input.Length; i++)
        {
            var line = input[i];

            var results = NumWins(line);
            
            if (wins.TryGetValue(results.CardNum, out int value))
                wins[results.CardNum] = ++value;
            else
                wins.Add(results.CardNum, 1);

            for (var r = 0; r < wins[results.CardNum]; r++)
            {
                for (var c = results.CardNum + 1; c <= results.NumWins + results.CardNum; c++)
                {
                    if (wins.TryGetValue(c, out int cvalue))
                        wins[c] = ++cvalue;
                    else
                        wins.Add(c, 1);
                }
            }
        }

        return wins.Select(x => x.Value).Sum();
    }

    private (int CardNum, int NumWins) NumWins(string line)
    {
        var cardParts = line.ToLower().Split(": ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var cardNum = int.Parse(cardParts[0].Replace("card ", ""));
        var cardContents = cardParts[1];

        var contentParts = cardContents.Split(" | ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        var winningNumbers = contentParts[0]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(int.Parse)
            .ToList();

        var ownNumbers = contentParts[1]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(int.Parse)
            .ToList();

        var winningMatches = ownNumbers.Where(winningNumbers.Contains).ToList();
        return (cardNum, winningMatches.Count);
    }

    private int CalculateWinnings(int winCount)
    {
        var whileCount = 0;
        var sum = 0;
        while (whileCount++ < winCount)
        {
            if (sum == 0)
                sum++;
            else
                sum *= 2;
        }

        return sum;
    }
}
