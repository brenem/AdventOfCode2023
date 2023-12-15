namespace AdventOfCode2023;
#nullable disable

public class Day8
{
    public long Part1(string[] input)
    {
        var instructions = input[0];
        var elements = input.Skip(2).Select(ParseNode).ToList();
        var startingNode = elements.Single(x => x.Header == "AAA");

        var stepsTaken = FindSteps(instructions, elements, startingNode);

        return stepsTaken;
    }

    public long Part2(string[] input)
    {
        var instructions = input[0];
        var elements = input.Skip(2).Select(ParseNode).ToList();
        var currentNodes = elements.Where(x => x.Header.EndsWith("A"));

        var steps = currentNodes.Select(x => FindSteps(instructions, elements, x, true)).ToArray();

        return steps.Aggregate(FindLCM);
    }

    private long FindLCM(long x, long y)
    {
        var max = x > y ? x : y;
        long lcm;

        for (var i = max; ; i += max)
        {
            if (i % x == 0 && i % y == 0)
            {
                lcm = i;
                break;
            }
        }

        return lcm;
    }

    private long FindSteps(string instructions, List<Node> elements, Node startingNode, bool checkingHeaderEnding = false)
    {
        var currentNode = startingNode;
        var instrIdx = 0;
        var endFound = false;
        long stepsTaken = 0;

        while (!endFound)
        {
            var instr = instructions[instrIdx++];

            if (instrIdx == instructions.Length)
                instrIdx = 0;

            if (instr == 'R')
                currentNode = elements.Single(x => x.Header == currentNode.Right);

            if (instr == 'L')
                currentNode = elements.Single(x => x.Header == currentNode.Left);

            stepsTaken++;

            if ((checkingHeaderEnding && currentNode.Header.EndsWith('Z')) || currentNode.Header == "ZZZ")
                endFound = true;
        }

        return stepsTaken;
    }

    private Node ParseNode(string nodeLine)
    {
        var nodeParts = nodeLine.Split(" = ", StringSplitOptions.TrimEntries);
        var header = nodeParts[0];

        var leftRightParts = nodeParts[1].Replace("(", string.Empty).Replace(")", string.Empty).Split(", ", StringSplitOptions.TrimEntries);

        var leftElement = leftRightParts[0];
        var rightElement = leftRightParts[1];

        return new Node { Header = header, Left = leftElement, Right = rightElement };
    }
}

public class Node
{
    public string Header { get; set; }
    public string Left { get; set; }
    public string Right { get; set; }
}