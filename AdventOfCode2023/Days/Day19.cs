namespace AdventOfCode2023;
#nullable disable

public class Day19
{
    public int Part1(string[] input)
    {
        var system = ParseInput(input);
        var acceptedRatings = new List<PartRating>();

        foreach (var rating in system.Ratings)
        {
            var accepted = EvaluateRating(rating, system.Workflows);
            if (accepted)
                acceptedRatings.Add(rating);
        }

        return acceptedRatings.Sum(x => x.X + x.M + x.A + x.S);
    }

    public long Part2(string[] input)
    {
        var system = ParseInput(input, includeRatings: false);
        var startRanges = new DictMultiRange<char>
        {
            Ranges = new()
            {
                { 'x',new(1, 4000) },
                { 'm',new(1, 4000) },
                { 'a',new(1, 4000) },
                { 's',new(1, 4000) }
            }
        };

        return GetRangeLengths(startRanges, system.Workflows.Single(x => x.Name == "in"), system.Workflows);
    }

    public long Part2_FirstTry(string[] input)
    {
        var system = ParseInput(input, includeRatings: false);

        List<int> xRanges = [1, 4001], mRanges = [1, 4001], aRanges = [1, 4001], sRanges = [1, 4001];
        long total = 0;

        foreach (var workflow in system.Workflows)
        {
            foreach (var rule in workflow.Rules)
            {
                if (rule.Category == 'x')
                    xRanges.Add(rule.Op == Operation.LessThan ? rule.Limit : rule.Limit + 1);
                if (rule.Category == 'm')
                    mRanges.Add(rule.Op == Operation.LessThan ? rule.Limit : rule.Limit + 1);
                if (rule.Category == 'a')
                    aRanges.Add(rule.Op == Operation.LessThan ? rule.Limit : rule.Limit + 1);
                if (rule.Category == 's')
                    sRanges.Add(rule.Op == Operation.LessThan ? rule.Limit : rule.Limit + 1);
            }
        }

        xRanges.Sort();
        mRanges.Sort();
        aRanges.Sort();
        sRanges.Sort();

        for (int x = 0; x < xRanges.Count - 1; x++)
        {
            long xLength = xRanges[x + 1] - xRanges[x];
            for (int m = 0; m < mRanges.Count - 1; m++)
            {
                long mLength = mRanges[m + 1] - mRanges[m];
                for (int a = 0; a < aRanges.Count - 1; a++)
                {
                    long aLength = aRanges[a + 1] - aRanges[a];
                    for (int s = 0; s < sRanges.Count - 1; s++)
                    {
                        long sLength = sRanges[s + 1] - sRanges[s];
                        var rating = new PartRating(xRanges[x], mRanges[m], aRanges[a], sRanges[s]);
                        var accepted = EvaluateRating(rating, system.Workflows);

                        if (accepted)
                        {
                            total += xLength * mLength * aLength * sLength;
                        }
                    }
                }
            }
        }

        return total;
    }

    HeapSystem ParseInput(string[] input, bool includeRatings = true)
    {
        var workflows = new List<Workflow>();
        var ratings = new List<PartRating>();
        var system = new HeapSystem();

        foreach (var item in input)
        {
            if (string.IsNullOrEmpty(item))
                break;

            system.Workflows.Add(ParseWorkflow(item));
        }

        if (includeRatings)
        {
            var startIndex = Array.IndexOf(input, string.Empty);
            for (var i = startIndex + 1; i < input.Length; i++)
            {
                var item = input[i];

                system.Ratings.Add(ParsePartRating(item));
            }
        }

        system.Workflows.AddRange([new AcceptWorkflow(), new RejectWorkflow()]);

        return system;
    }

    bool EvaluateRating(PartRating rating, IEnumerable<Workflow> workflows)
    {
        const string acceptReject = "AR";
        var workflow = workflows.SingleOrDefault(x => x.Name == "in");
        var accepted = false;

        while (workflow != null)
        {
            var defaultValue = workflow.Default;
            Workflow nextWorkflow = null;

            if (workflow.Rules.TrueForAll(x => x.Conseqent == "A") && defaultValue == "A")
                return true;
            if (workflow.Rules.TrueForAll(x => x.Conseqent == "R") && defaultValue == "R")
                return false;

            foreach (var rule in workflow.Rules)
            {
                nextWorkflow = FindNextWorkflow(rating, rule, workflows);
                if (nextWorkflow is AcceptWorkflow)
                    return true;
                if (nextWorkflow is RejectWorkflow)
                    return false;
                if (nextWorkflow != null)
                    break;
            }

            if (nextWorkflow == null)
            {
                if (!acceptReject.Contains(defaultValue))
                    nextWorkflow = workflows.SingleOrDefault(x => x.Name == defaultValue);
                else
                    return defaultValue == "A";
            }

            workflow = nextWorkflow;
        }

        return accepted;
    }

    long GetRangeLengths(DictMultiRange<char> ranges, Workflow startWorkflow, IEnumerable<Workflow> workflows)
    {
        long accepted = 0;
        foreach (var rule in startWorkflow.Rules)
        {
            DictMultiRange<char> nR = new(ranges);

            if (rule.Op == Operation.GreaterThan)
            {
                if (ranges.Ranges[rule.Category].End > rule.Limit)
                {
                    nR.Ranges[rule.Category].Start = Math.Max(nR.Ranges[rule.Category].Start, rule.Limit + 1);

                    if (rule.Conseqent == "A")
                        accepted += nR.Length;
                    else if (rule.Conseqent != "R")
                        accepted += GetRangeLengths(nR, workflows.Single(x => x.Name == rule.Conseqent), workflows);

                    ranges.Ranges[rule.Category].End = rule.Limit;
                }
            }

            if (rule.Op == Operation.LessThan)
            {
                if (ranges.Ranges[rule.Category].Start < rule.Limit)
                {
                    nR.Ranges[rule.Category].End = Math.Min(nR.Ranges[rule.Category].End, rule.Limit - 1);

                    if (rule.Conseqent == "A")
                        accepted += nR.Length;
                    else if (rule.Conseqent != "R")
                        accepted += GetRangeLengths(nR, workflows.Single(x => x.Name == rule.Conseqent), workflows);

                    ranges.Ranges[rule.Category].Start = rule.Limit;
                }
            }
        }

        if (startWorkflow.Default == "A")
            accepted += ranges.Length;
        else if (startWorkflow.Default != "R")
            accepted += GetRangeLengths(ranges, workflows.Single(x => x.Name == startWorkflow.Default), workflows);

        return accepted;
    }

    Workflow FindNextWorkflow(PartRating rating, WorkflowRule rule, IEnumerable<Workflow> workflows)
    {
        Workflow workflow = null;

        switch (rule.Category)
        {
            case 'x':
                switch (rule.Op)
                {
                    case Operation.LessThan:
                        if (rating.X < rule.Limit)
                            workflow = workflows.SingleOrDefault(x => x.Name == rule.Conseqent);
                        break;
                    case Operation.GreaterThan:
                        if (rating.X > rule.Limit)
                            workflow = workflows.SingleOrDefault(x => x.Name == rule.Conseqent);
                        break;
                }
                break;
            case 'm':
                switch (rule.Op)
                {
                    case Operation.LessThan:
                        if (rating.M < rule.Limit)
                            workflow = workflows.SingleOrDefault(x => x.Name == rule.Conseqent);
                        break;
                    case Operation.GreaterThan:
                        if (rating.M > rule.Limit)
                            workflow = workflows.SingleOrDefault(x => x.Name == rule.Conseqent);
                        break;
                }
                break;
            case 'a':
                switch (rule.Op)
                {
                    case Operation.LessThan:
                        if (rating.A < rule.Limit)
                            workflow = workflows.SingleOrDefault(x => x.Name == rule.Conseqent);
                        break;
                    case Operation.GreaterThan:
                        if (rating.A > rule.Limit)
                            workflow = workflows.SingleOrDefault(x => x.Name == rule.Conseqent);
                        break;
                }
                break;
            case 's':
                switch (rule.Op)
                {
                    case Operation.LessThan:
                        if (rating.S < rule.Limit)
                            workflow = workflows.SingleOrDefault(x => x.Name == rule.Conseqent);
                        break;
                    case Operation.GreaterThan:
                        if (rating.S > rule.Limit)
                            workflow = workflows.SingleOrDefault(x => x.Name == rule.Conseqent);
                        break;
                }
                break;
        }

        return workflow;
    }

    Workflow ParseWorkflow(string line)
    {
        var parts = line.Split(new[] { '{', '}' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var name = parts[0];
        var ruleItems = parts[1].Split(',');

        var workflow = new Workflow(name);
        workflow.Default = ruleItems[^1];

        foreach (var item in ruleItems[..^1])
        {
            var rule = new WorkflowRule();
            rule.Category = item[0];
            rule.Op = item[1] == '<' ? Operation.LessThan : Operation.GreaterThan;
            rule.Limit = int.Parse(item[2..].Split(':')[0]);
            rule.Conseqent = item[2..].Split(':')[1];

            workflow.Rules.Add(rule);
        }

        return workflow;
    }

    PartRating ParsePartRating(string line)
    {
        var ratingParts = line.Split(new[] { '{', '}', ',' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var xRating = int.Parse(ratingParts.Single(x => x.StartsWith("x="))[2..]);
        var mRating = int.Parse(ratingParts.Single(x => x.StartsWith("m="))[2..]);
        var aRating = int.Parse(ratingParts.Single(x => x.StartsWith("a="))[2..]);
        var sRating = int.Parse(ratingParts.Single(x => x.StartsWith("s="))[2..]);

        return new PartRating(xRating, mRating, aRating, sRating);
    }

    class HeapSystem
    {
        public List<Workflow> Workflows { get; set; } = [];
        public List<PartRating> Ratings { get; set; } = [];
    }

    class Workflow(string name)
    {
        public string Name { get; } = name;
        public List<WorkflowRule> Rules { get; set; } = [];
        public string Default { get; set; }
    }

    class AcceptWorkflow : Workflow
    {
        public AcceptWorkflow() : base("A") { }
    }

    class RejectWorkflow : Workflow
    {
        public RejectWorkflow() : base("R") { }
    }

    class WorkflowRule
    {
        public char Category { get; set; }
        public Operation Op { get; set; }
        public int Limit { get; set; }
        public string Conseqent { get; set; }
    }

    record PartRating(int X, int M, int A, int S);
    class PartRange
    {
        public long Start { get; set; }
        public long End { get; set; }
        public long Len => End - Start + 1;

        public PartRange(long Start, long End)
        {
            this.Start = Start;
            this.End = End;
        }

        //Forced Deep Copy
        public PartRange(PartRange other)
        {
            Start = other.Start;
            End = other.End;
        }

        public override string ToString()
        {
            return $"[{Start}, {End}] ({Len})";
        }
    }

    class DictMultiRange<T>
    {
        public Dictionary<T, PartRange> Ranges { get; set; } = [];

        public DictMultiRange() { }

        public DictMultiRange(DictMultiRange<T> other)
        {
            foreach (var r in other.Ranges)
            {
                PartRange n = new(r.Value);
                Ranges[r.Key] = n;
            }
        }

        public long Length => Ranges.Aggregate(1L, (a, b) => a *= b.Value.Len);
    }

    enum Operation
    {
        GreaterThan,
        LessThan
    }
}