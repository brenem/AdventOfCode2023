namespace AdventOfCode2023.Days
{
    public class Day2
    {
        public int Part1(CubeData cubeData, string[] inputData)
        {
            var games = inputData.Select(ParseGame);
            var possibleGames = games.Where(x => x.Results.All(r => r.Red <= cubeData.Red && r.Green <= cubeData.Green && r.Blue <= cubeData.Blue));
            return possibleGames.Select(x => x.Id).Sum();
        }

        public int Part2(string[] inputData)
        {
            var games = inputData.Select(ParseGame);
            return games.Select(x => x.CalculatePower()).Sum();
        }

        private Game ParseGame(string input)
        {
            var parts = input.Split(':');
            var gameString = parts[0].Trim();
            var resultsString = parts[1].Trim();

            var gameId = int.Parse(gameString.ToLower().Replace("game ", string.Empty).Trim());

            var resultsParts = resultsString.Split(';');
            var cubeData = resultsParts.Select(x => ParseCubeData(x.Split(',')));

            var game = new Game(gameId, cubeData);
            return game;
        }

        private CubeData ParseCubeData(string[] input)
        {
            var cubeData = new CubeData();
            foreach (var str in input)
            {
                var lower = str.ToLower();
                if (lower.Contains("green"))
                    cubeData.Green = int.Parse(lower.Replace(" green", "").Trim());
                if (lower.Contains("red"))
                    cubeData.Red = int.Parse(lower.Replace(" red", "").Trim());
                if (lower.Contains("blue"))
                    cubeData.Blue = int.Parse(lower.Replace(" blue", "").Trim());
            }
            return cubeData;
        }
    }

    public record Game(int Id, IEnumerable<CubeData> Results)
    {
        public int CalculatePower()
        {
            var maxRed = Results.Select(x => x.Red).Max();
            var maxGreen = Results.Select(x => x.Green).Max();
            var maxBlue = Results.Select(x => x.Blue).Max();

            return maxRed * maxGreen * maxBlue;
        }
    }

    public record CubeData
    {
        public int Red { get; set; }
        public int Blue { get; set; }
        public int Green { get; set; }
    }
}
