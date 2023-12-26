using AdventOfCode2023.Extensions;
using AdventOfCode2023.Models;

namespace AdventOfCode2023;
#nullable disable

public class Day17
{
    public int Part1(string[] input)
    {
        var queue = new PriorityQueue<Block, int>();
        var visited = new List<string>();

        queue.Enqueue(new Block(new GridLocation<int>(0, 0), GridDirection.East, 0), 0);

        var heatLoss = 0;

        while (queue.Count > 0)
        {
            var block = queue.Dequeue();

            if (block.Position.Row == input.Length - 1 && block.Position.Col == input[0].Length - 1)
            {
                heatLoss = block.HeatLoss;
                break;
            }

            if (block.PathLength < 3)
            {
                TryMove(block, block.Direction, queue, visited, input);
            }

            TryMove(block, block.Direction.TurnLeft(), queue, visited, input);
            TryMove(block, block.Direction.TurnRight(), queue, visited, input);
        }

        return heatLoss;
    }

    public int Part2(string[] input)
    {
        var queue = new PriorityQueue<Block, int>();
        var visited = new List<string>();

        queue.Enqueue(new Block(new GridLocation<int>(0, 0), GridDirection.East, 0), 0);
        queue.Enqueue(new Block(new GridLocation<int>(0, 0), GridDirection.South, 0), 0);

        var heatLoss = 0;

        while (queue.Count > 0)
        {
            var block = queue.Dequeue();

            if (block.Position.Row == input.Length - 1 && block.Position.Col == input[0].Length - 1 && block.PathLength >= 4)
            {
                heatLoss = block.HeatLoss;
                break;
            }

            if (block.PathLength < 10)
            {
                TryMove(block, block.Direction, queue, visited, input);
            }

            if (block.PathLength >= 4)
            {
                TryMove(block, block.Direction.TurnLeft(), queue, visited, input);
                TryMove(block, block.Direction.TurnRight(), queue, visited, input);
            }
        }

        return heatLoss;
    }

    void TryMove(Block block, GridDirection direction, PriorityQueue<Block, int> queue, List<string> visited, string[] input)
    {
        var nextBlock = new Block(block.Position.Move(direction), direction, direction == block.Direction ? block.PathLength + 1 : 1);

        if (nextBlock.Position.Row < 0 || nextBlock.Position.Row >= input.Length ||
            nextBlock.Position.Col < 0 || nextBlock.Position.Col >= input[0].Length)
        {
            return;
        }

        var key = $"{nextBlock.Position.Row},{nextBlock.Position.Col},{nextBlock.Direction},{nextBlock.PathLength}";
        if (visited.Contains(key))
            return;

        visited.Add(key);

        nextBlock.HeatLoss = block.HeatLoss + int.Parse(input[nextBlock.Position.Row][nextBlock.Position.Col].ToString());
        queue.Enqueue(nextBlock, nextBlock.HeatLoss);
    }

    record Block(GridLocation<int> Position, GridDirection Direction, int PathLength)
    {
        public int HeatLoss { get; set; }
    }
}
