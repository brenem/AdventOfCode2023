using AdventOfCode2023.Extensions;
using AdventOfCode2023.Models;

namespace AdventOfCode2023;
#nullable disable

public class Day10
{
    public int Part1(string[] input)
    {
        var pipes = input.SelectMany((row, rowIdx) => row.Select((pipe, pipeIdx) => ParsePipe(pipe, pipeIdx, rowIdx, input)).Where(x => x.Value != '.')).ToList();

        var currentPipe = pipes.Single(x => x.Value == 'S');
        var stepCount = 0;

        PipeNode prevPipe = null;
        while (true)
        {
            var nextPipeLocation = FindNextPipeNode(prevPipe, currentPipe, pipes);
            prevPipe = currentPipe;
            currentPipe = nextPipeLocation;

            stepCount++;

            if (currentPipe.Value == 'S')
                break;
        }


        var farthestPoint = stepCount / 2;
        return farthestPoint;
    }

    public double Part2(string[] input)
    {
        var nodes = input.SelectMany((row, rowIdx) => row.Select((pipe, pipeIdx) => ParsePipe(pipe, pipeIdx, rowIdx, input))).ToList();
        var pipes = nodes.Where(x => x.Direction != PipeDirection.Ground).ToList();
        var grounds = nodes.Where(x => x.Direction == PipeDirection.Ground).ToList();

        var pipesInLoop = new List<GridTile>();
        var steps = 0;

        var currentPipe = pipes.Single(x => x.Value == 'S');

        PipeNode prevPipe = null;
        while (true)
        {
            pipesInLoop.Add(currentPipe);
            steps++;

            var nextPipeLocation = FindNextPipeNode(prevPipe, currentPipe, pipes);
            prevPipe = currentPipe;
            currentPipe = nextPipeLocation;

            if (currentPipe.Value == 'S')
                break;
        }

        var farthestPoint = steps / 2;
        var area = pipesInLoop.Select(x => x.Location).ToShoelaceArea();

        return area - (steps / 2) + 1;
    }

    PipeNode FindNextPipeNode(PipeNode prevPipe, PipeNode currPipe, IEnumerable<PipeNode> pipes)
    {
        var topNode = currPipe.North.FindGridNode(pipes);
        var bottomNode = currPipe.South.FindGridNode(pipes);
        var rightNode = currPipe.East.FindGridNode(pipes);
        var leftNode = currPipe.West.FindGridNode(pipes);

        var top = topNode != null && topNode.Location != prevPipe?.Location ? topNode : null;
        var bottom = bottomNode != null && bottomNode.Location != prevPipe?.Location ? bottomNode : null;
        var right = rightNode != null && rightNode.Location != prevPipe?.Location ? rightNode : null;
        var left = leftNode != null && leftNode.Location != prevPipe?.Location ? leftNode : null;

        return currPipe.Direction switch
        {
            PipeDirection.Start => top ?? bottom ?? right ?? left,
            PipeDirection.Horizontal => left ?? right,
            PipeDirection.Vertical => top ?? bottom,
            PipeDirection.SouthToWest => left ?? bottom,
            PipeDirection.SouthToEast => right ?? bottom,
            PipeDirection.NorthToWest => left ?? top,
            PipeDirection.NorthToEast => top ?? right,
            _ => throw new NotSupportedException()
        };
    }

    PipeNode ParsePipe(char pipeChar, int colIdx, int rowIdx, string[] pipeLines)
    {
        var pipeDirection = ParsePipeDirection(pipeChar);

        var currentLine = pipeLines[rowIdx];
        string prevLine = null;
        string nextLine = null;

        if (rowIdx == 0)
        {
            nextLine = pipeLines[rowIdx + 1];
        }
        else if (rowIdx == pipeLines.Length - 1)
        {
            prevLine = pipeLines[rowIdx - 1];
        }
        else
        {
            prevLine = pipeLines[rowIdx - 1];
            nextLine = pipeLines[rowIdx + 1];
        }

        Coordinate<int> pipeLeft = null, pipeRight = null, pipeTop = null, pipeBottom = null;

        if (colIdx == 0)
        {
            pipeRight = new Coordinate<int>(rowIdx, colIdx + 1);

            if (prevLine != null)
                pipeTop = new Coordinate<int>(rowIdx - 1, colIdx);

            if (nextLine != null)
                pipeBottom = new Coordinate<int>(rowIdx + 1, colIdx);
        }
        else if (colIdx == currentLine.Length - 1)
        {
            pipeLeft = new Coordinate<int>(rowIdx, colIdx - 1);

            if (prevLine != null)
                pipeTop = new Coordinate<int>(rowIdx - 1, colIdx);

            if (nextLine != null)
                pipeBottom = new Coordinate<int>(rowIdx + 1, colIdx);
        }
        else
        {
            pipeRight = new Coordinate<int>(rowIdx, colIdx + 1);

            pipeLeft = new Coordinate<int>(rowIdx, colIdx - 1);

            if (prevLine != null)
                pipeTop = new Coordinate<int>(rowIdx - 1, colIdx);

            if (nextLine != null)
                pipeBottom = new Coordinate<int>(rowIdx + 1, colIdx);
        }

        return new PipeNode
        {
            Value = pipeChar,
            Location = new Coordinate<int>(rowIdx, colIdx),
            Direction = pipeDirection,
            North = pipeTop,
            South = pipeBottom,
            East = pipeRight,
            West = pipeLeft
        };
    }

    class PipeNode : GridTile
    {
        public PipeDirection Direction { get; set; }
    }

    PipeDirection ParsePipeDirection(char pipeChar) => pipeChar switch
    {
        '|' => PipeDirection.Vertical,
        '-' => PipeDirection.Horizontal,
        'L' => PipeDirection.NorthToEast,
        'J' => PipeDirection.NorthToWest,
        '7' => PipeDirection.SouthToWest,
        'F' => PipeDirection.SouthToEast,
        '.' => PipeDirection.Ground,
        'S' => PipeDirection.Start,
        _ => throw new NotSupportedException($"Pipe '{pipeChar}' is not handled")
    };
}

enum PipeDirection
{
    Ground,
    Start,
    Vertical,
    Horizontal,
    NorthToEast,
    NorthToWest,
    SouthToWest,
    SouthToEast
}
