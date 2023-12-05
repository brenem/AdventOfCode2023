namespace AdventOfCode2023.Days
{
    internal class Day1Part1 : IDay<int>
    {
        public int Result { get; private set; }

        public IDay<int> Run()
        {
            var input = File.ReadAllLines(Path.Combine("InputData", "Day1.txt"));

            var numStrings = input.Select(x => string.Concat(x.Where(c => char.IsDigit(c))));
            var concatNumStrings = numStrings.Select(x => x.First().ToString() + x.Last().ToString());
            var nums = concatNumStrings.Select(int.Parse);
            Result = nums.Sum();

            return this;
        }
    }
}
