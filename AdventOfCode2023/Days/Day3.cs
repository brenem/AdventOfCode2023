using System.Text.RegularExpressions;

namespace AdventOfCode2023.Days
{
    public partial class Day3
    {
        public int Part1(string[] input)
        {
            var numList = new List<int>();

            for (var i = 0; i < input.Length; i++)
            {
                MatchCollection? prevCharMatches = null;
                MatchCollection? nextCharMatches = null;

                if (i > 0)
                {
                    prevCharMatches = NonPeriodCharacterRegex().Matches(input[i - 1]);
                }

                if (i < input.Length - 1)
                {
                    nextCharMatches = NonPeriodCharacterRegex().Matches(input[i + 1]);
                }

                var currentNumberMatches = NumberGroupRegex().Matches(input[i]);
                var currentCharMatches = NonPeriodCharacterRegex().Matches(input[i]);

                if (currentNumberMatches.Count > 0)
                {
                    if (currentCharMatches.Count > 0)
                    {
                        var currentAdjacent = currentNumberMatches
                            .Where(x => currentCharMatches.FirstOrDefault(n => Enumerable.Range(x.Index - 1, x.Length + 2).Contains(n.Index)) != null);

                        if (currentAdjacent.Any())
                            numList.AddRange(currentAdjacent.Select(x => int.Parse(x.Value)));
                    }

                    if (prevCharMatches != null && prevCharMatches.Count > 0)
                    {
                        var prevAdjacent = currentNumberMatches
                            .Where(x => prevCharMatches.FirstOrDefault(n => Enumerable.Range(x.Index - 1, x.Length + 2).Contains(n.Index)) != null);

                        if (prevAdjacent.Any())
                            numList.AddRange(prevAdjacent.Select(x => int.Parse(x.Value)));
                    }

                    if (nextCharMatches != null && nextCharMatches.Count > 0)
                    {
                        var nextAdjacent = currentNumberMatches
                            .Where(x => nextCharMatches.FirstOrDefault(n => Enumerable.Range(x.Index - 1, x.Length + 2).Contains(n.Index)) != null);

                        if (nextAdjacent.Any())
                            numList.AddRange(nextAdjacent.Select(x => int.Parse(x.Value)));
                    }
                }
            }

            return numList.Sum();
        }

        public int Part2(string[] input)
        {
            var numList = new List<int>();
            var lineCount = 0;

            for (var i = 1; i < input.Length - 1; i++)
            {
                var prevNumMatches = NumberGroupRegex().Matches(input[i - 1]);
                var nextNumMatches = NumberGroupRegex().Matches(input[i + 1]);

                var currNumMatches = NumberGroupRegex().Matches(input[i]);
                var currCharMatches = AsteriskCharactersRegex().Matches(input[i]);

                if (currCharMatches.Any())
                {
                    Console.WriteLine("Evaluating gear line: {0}", i);

                    var lineGears = new List<int>();

                    foreach (Match charMatch in currCharMatches)
                    {
                        var prevInBox = prevNumMatches.Where(x => IndexInBox(charMatch.Index, x)).ToList();
                        var nextInBox = nextNumMatches.Where(x => IndexInBox(charMatch.Index, x)).ToList();
                        var currInBox = currNumMatches.Where(x => IndexInBox(charMatch.Index, x)).ToList();

                        if (prevInBox.Count == 1 && nextInBox.Count == 1)
                        {
                            var prev = prevInBox[0];
                            var next = nextInBox[0];
                            var prevValue = int.Parse(prev.Value);
                            var nextValue = int.Parse(next.Value);

                            Console.WriteLine("\tFound gear ratio: {0},{1}", prevValue, nextValue);

                            lineGears.Add(prevValue * nextValue);
                        }
                        else if (prevInBox.Count == 2 && nextInBox.Count == 0)
                        {
                            var prev1 = prevInBox[0];
                            var prev2 = prevInBox[1];
                            var prev1Value = int.Parse(prev1.Value);
                            var prev2Value = int.Parse(prev2.Value);

                            Console.WriteLine("\tFound gear ratio: {0},{1}", prev1Value, prev2Value);

                            lineGears.Add(prev1Value * prev2Value);
                        }
                        else if (nextInBox.Count == 2 && prevInBox.Count == 0)
                        {
                            var next1 = nextInBox[0];
                            var next2 = nextInBox[1];
                            var next1Value = int.Parse(next1.Value);
                            var next2Value = int.Parse(next2.Value);

                            Console.WriteLine("\tFound gear ratio: {0},{1}", next1Value, next2Value);

                            lineGears.Add(next1Value * next2Value);
                        }

                        if (currInBox.Count == 2)
                        {
                            var currLeft = currInBox[0];
                            var currRight = currInBox[1];
                            var currLeftValue = int.Parse(currLeft.Value);
                            var currRightValue = int.Parse(currRight.Value);

                            Console.WriteLine("\tFound gear ratio: {0},{1}", currLeftValue, currRightValue);

                            lineGears.Add(currLeftValue * currRightValue);
                        }
                        else if (currInBox.Count == 1)
                        {
                            if (prevInBox.Count == 1)
                            {
                                var curr = currInBox[0];
                                var prev = prevInBox[0];
                                var currValue = int.Parse(curr.Value);
                                var prevValue = int.Parse(prev.Value);

                                Console.WriteLine("\tFound gear ratio: {0},{1}", currValue, prevValue);

                                lineGears.Add(currValue * prevValue);
                            }

                            if (nextInBox.Count == 1)
                            {
                                var curr = currInBox[0];
                                var next = nextInBox[0];
                                var currValue = int.Parse(curr.Value);
                                var nextValue = int.Parse(next.Value);

                                Console.WriteLine("\tFound gear ratio: {0},{1}", currValue, nextValue);

                                lineGears.Add(currValue * nextValue);
                            }
                        }
                    }

                    numList.AddRange(lineGears.Distinct());
                }

                lineCount++;
            }

            Console.WriteLine("Line count: {0}", lineCount);

            return numList.Sum();
        }

        private bool IndexInBox(int index, Match match)
        {
            return Enumerable.Range(match.Index - 1, match.Length + 2).Contains(index);
        }

        [GeneratedRegex("\\d+")]
        private static partial Regex NumberGroupRegex();

        [GeneratedRegex("[^.,0-9]")]
        private static partial Regex NonPeriodCharacterRegex();

        [GeneratedRegex("(\\*)")]
        private static partial Regex AsteriskCharactersRegex();
    }
}
