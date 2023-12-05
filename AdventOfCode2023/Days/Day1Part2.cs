namespace AdventOfCode2023.Days
{
    internal class Day1Part2 : IDay<int>
    {
        public int Result { get; private set; }

        public IDay<int> Run()
        {
            var input = File.ReadAllLines(Path.Combine("InputData", "Day1.txt"));
            var sanitized = input.Select(ReplaceNumberWords);
            var numStrings = sanitized.Select(x => string.Concat(x.Where(c => char.IsDigit(c))));
            var concatNumStrings = numStrings.Select(x => x.First().ToString() + x.Last().ToString());
            var nums = concatNumStrings.Select(int.Parse);
            Result = nums.Sum();

            return this;
        }

        private string ReplaceNumberWords(string input)
        {
            var lower = input.ToLower();

            string newstring = "";

            for (var i = 0; i < lower.Length; i++)
            {
                newstring += ReplaceWordWithNumber(lower.Substring(0, i));
            }

            return newstring;
        }

        private string ReplaceWordWithNumber(string word)
        {
            return word.Replace("one", "1")
                .Replace("two", "2")
                .Replace("three", "3")
                .Replace("four", "4")
                .Replace("five", "5")
                .Replace("six", "6")
                .Replace("seven", "7")
                .Replace("eight", "8")
                .Replace("nine", "9");
        }

        private bool IsNumberWord(string word)
        {
            return word == "one"
                || word == "two"
                || word == "three"
                || word == "four"
                || word == "five"
                || word == "six"
                || word == "seven"
                || word == "eight"
                || word == "nine";
        }
    }
}
