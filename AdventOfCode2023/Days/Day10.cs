namespace AdventOfCode2023;
#nullable disable

public class Day10
{
    public int Part1(string[] input)
    {
        var pipes = input.SelectMany((row, rowIdx) => row.Select((pipe, pipeIdx) => ParsePipe(pipe, pipeIdx, rowIdx, input)).Where(x => x.PipeChar != '.')).ToList();

        var currentPipe = pipes.Single(x => x.PipeChar == 'S');
        var stepCount = 0;

        PipeNode prevPipe = null;
        while (true)
        {
            var nextPipeLocation = FindNextPipeNode(prevPipe, currentPipe, pipes);
            prevPipe = currentPipe;
            currentPipe = nextPipeLocation;

            stepCount++;

            if (currentPipe.PipeChar == 'S')
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

        var pipesInLoop = new List<PipeNode>();
        var steps = 0;

        var currentPipe = pipes.Single(x => x.PipeChar == 'S');

        PipeNode prevPipe = null;
        while (true)
        {
            pipesInLoop.Add(currentPipe);
            steps++;

            var nextPipeLocation = FindNextPipeNode(prevPipe, currentPipe, pipes);
            prevPipe = currentPipe;
            currentPipe = nextPipeLocation;

            if (currentPipe.PipeChar == 'S')
                break;
        }

        var farthestPoint = steps / 2;
        var area = ShoelaceArea(pipesInLoop.Select(x => x.Location).ToList());

        return area - (steps / 2) + 1;
    }

    double ShoelaceArea(List<PipeLocation> locations)
    {
        var n = locations.Count;
        double a = 0.0;
        for (int i = 0; i < n - 1; i++)
        {
            a += locations[i].Row * locations[i + 1].Col - locations[i + 1].Row * locations[i].Col;
        }

        return Math.Abs(a + locations[n - 1].Row * locations[0].Col - locations[0].Row * locations[n - 1].Col) / 2.0;
    }

    PipeNode FindNextPipeNode(PipeNode prevPipe, PipeNode currPipe, IEnumerable<PipeNode> pipes)
    {
        PipeNode topNode = currPipe.Top.FindPipeNode(pipes);
        PipeNode bottomNode = currPipe.Bottom.FindPipeNode(pipes);
        PipeNode rightNode = currPipe.Right.FindPipeNode(pipes);
        PipeNode leftNode = currPipe.Left.FindPipeNode(pipes);

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

        PipeLocation pipeLeft = null, pipeRight = null, pipeTop = null, pipeBottom = null;

        if (colIdx == 0)
        {
            pipeRight = new PipeLocation(rowIdx, colIdx + 1);

            if (prevLine != null)
                pipeTop = new PipeLocation(rowIdx - 1, colIdx);

            if (nextLine != null)
                pipeBottom = new PipeLocation(rowIdx + 1, colIdx);
        }
        else if (colIdx == currentLine.Length - 1)
        {
            pipeLeft = new PipeLocation(rowIdx, colIdx - 1);

            if (prevLine != null)
                pipeTop = new PipeLocation(rowIdx - 1, colIdx);

            if (nextLine != null)
                pipeBottom = new PipeLocation(rowIdx + 1, colIdx);
        }
        else
        {
            pipeRight = new PipeLocation(rowIdx, colIdx + 1);

            pipeLeft = new PipeLocation(rowIdx, colIdx - 1);

            if (prevLine != null)
                pipeTop = new PipeLocation(rowIdx - 1, colIdx);

            if (nextLine != null)
                pipeBottom = new PipeLocation(rowIdx + 1, colIdx);
        }

        return new PipeNode(pipeChar, new PipeLocation(rowIdx, colIdx), pipeDirection, pipeTop, pipeBottom, pipeRight, pipeLeft);
    }

    //bool IsConnected(PipeDirection a, PipeDirection b)
    //{
    //    return new { a, b } switch
    //    {
    //        { a: PipeDirection.Vertical, b: PipeDirection.SouthToWest } => true,
    //        { a: PipeDirection.Vertical, b: PipeDirection.SouthToEast } => true,
    //        { a: PipeDirection.Vertical, b: PipeDirection.NorthToWest } => true,
    //        { a: PipeDirection.Vertical, b: PipeDirection.NorthToEast } => true,

    //        { a: PipeDirection.Horizontal, b: PipeDirection.SouthToWest } => true,
    //        { a: PipeDirection.Horizontal, b: PipeDirection.SouthToEast } => true,
    //        { a: PipeDirection.Horizontal, b: PipeDirection.NorthToWest } => true,
    //        { a: PipeDirection.Horizontal, b: PipeDirection.NorthToEast } => true,

    //        { a: PipeDirection.NorthToEast, b: }
    //    };
    //}

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

record PipeNode(char PipeChar, PipeLocation Location, PipeDirection Direction, PipeLocation Top, PipeLocation Bottom, PipeLocation Right, PipeLocation Left)
{
    public override string ToString()
    {
        return $"'{PipeChar}', ({Location.Row}, {Location.Col}), N: ({Top.Row}, {Top.Col}), S: ({Bottom.Row}, {Bottom.Col}), E: ({Right.Row}, {Right.Col}), W: ({Left.Row}, {Left.Col})";
    }
}

record PipeLocation(int Row, int Col);

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

static class Day10Extensions
{
    public static PipeNode FindPipeNode(this PipeLocation location, IEnumerable<PipeNode> pipeNodes)
    {
        return pipeNodes.SingleOrDefault(x => x.Location == location);
    }
}
