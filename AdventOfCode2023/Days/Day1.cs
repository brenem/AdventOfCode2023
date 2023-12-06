namespace AdventOfCode2023.Days
{
    public class Day1
    {
        private readonly Dictionary<string, string> _numberWords = new()
        {
            { "one", "1ne" },
            { "two", "2wo" },
            { "three", "3hree" },
            { "four", "4our" },
            { "five", "5ive" },
            { "six", "6ix" },
            { "seven", "7even" },
            { "eight", "8ight" },
            { "nine", "9ine" }
        };

        public int Part1(string[] inputData)
        {
            var numStrings = inputData.Select(x => string.Concat(x.Where(c => char.IsDigit(c))));
            var concatNumStrings = numStrings.Select(x => x.First().ToString() + x.Last().ToString());
            var nums = concatNumStrings.Select(int.Parse);
            return nums.Sum();
        }

        public int Part2(string[] inputData)
        {
            var sanitized = inputData.Select(FindAndReplaceNumberWords);
            var numStrings = sanitized.Select(x => string.Concat(x.Where(c => char.IsDigit(c))));
            var concatNumStrings = numStrings.Select(x => x.First().ToString() + x.Last().ToString());
            var nums = concatNumStrings.Select(int.Parse);
            return nums.Sum();
        }

        private string FindAndReplaceNumberWords(string input)
        {
            var firstNumberWord = _numberWords.Keys
                .Select(x => new { idx = input.IndexOf(x), word = x })
                .Where(x => x.idx > -1)
                .OrderBy(x => x.idx)
                .FirstOrDefault();

            if (firstNumberWord != null)
            {
                input = input.Replace(firstNumberWord.word, ConvertWordToNumber(firstNumberWord.word));
                input = FindAndReplaceNumberWords(input);
            }

            return input;
        }

        private string ConvertWordToNumber(string word)
        {
            if (_numberWords.TryGetValue(word, out string? value))
                return value;
            else
                return string.Empty;
        }
    }
}
