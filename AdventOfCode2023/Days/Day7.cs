using System.Collections.ObjectModel;

namespace AdventOfCode2023;
#nullable disable

public class Day7
{
    public int Part1(string[] input)
    {
        var hands = input.Select(x => new Part1Hand(x))
            .OrderBy(x => x, new Part1HandComparer())
            .ToList();

        return hands.Select((x, i) => x.Bid * (i + 1)).Sum();
    }

    public int Part2(string[] input)
    {
        var hands = input.Select(x => new Part2Hand(x))
            .OrderBy(x => x, new Part2HandComparer())
            .ToList();

        return hands.Select((x, i) => x.Bid * (i + 1)).Sum();
    }
}

public class Part1HandComparer : IComparer<Hand>
{
    public int Compare(Hand x, Hand y)
    {
        if (x.HandType > y.HandType)
            return 1;
        if (x.HandType < y.HandType)
            return -1;

        if (x.HandType == y.HandType)
        {
            for (var i = 0; i < x.CardHand.Length; i++)
            {
                var xCardVal = Constants.Part1CardValues[x.CardHand[i]];
                var yCardVal = Constants.Part1CardValues[y.CardHand[i]];
                if (xCardVal > yCardVal)
                    return 1;
                if (xCardVal < yCardVal)
                    return -1;
            }
        }

        return 0;
    }
}

public class Part2HandComparer : IComparer<Hand>
{
    public int Compare(Hand x, Hand y)
    {
        if (x.HandType != y.HandType)
        {
            return x.HandType - y.HandType;
        }
        else
        {
            if (x.HandType == y.HandType)
            {
                for (var i = 0; i < x.CardHand.Length; i++)
                {
                    var xCardVal = Constants.Part2CardValues[x.CardHand[i]];
                    var yCardVal = Constants.Part2CardValues[y.CardHand[i]];
                    if (xCardVal > yCardVal)
                        return 1;
                    if (xCardVal < yCardVal)
                        return -1;
                }
            }

            return 0;
        }
    }
}

public abstract class Hand
{
    protected Hand(string line)
    {
        var cards = line.Split(' ', StringSplitOptions.TrimEntries)[0];

        Bid = int.Parse(line.Split(' ', StringSplitOptions.TrimEntries)[1]);
        CardHand = cards;

        Initialize();
    }

    private void Initialize()
    {
        DetermineHandType();
    }

    public int Bid { get; protected set; }
    public string CardHand { get; protected set; }
    public HandType HandType { get; protected set; } = HandType.None;

    protected abstract void DetermineHandType();

    public override string ToString()
    {
        return $"{CardHand} {Bid} - {HandType}";
    }
}

public class Part1Hand(string line) : Hand(line)
{
    protected override void DetermineHandType()
    {
        var grouped = CardHand.GroupBy(x => x).ToList();
        var max = grouped.Max(x => x.Count());
        var min = grouped.Min(x => x.Count());

        HandType = new { grpCount = grouped.Count, max, min } switch
        {
            { grpCount: 1 } => HandType.FiveOfAKind,
            { max: 4 } => HandType.FourOfAKind,
            { grpCount: 2, max: 3, min: 2 } => HandType.FullHouse,
            { grpCount: 3, max: 3 } => HandType.ThreeOfAKind,
            { grpCount: 3, max: 2 } => HandType.TwoPair,
            { grpCount: 4 } => HandType.OnePair,
            { grpCount: 5 } => HandType.HighCard,
            _ => HandType.None
        };
    }
}

public class Part2Hand(string line) : Hand(line)
{
    protected override void DetermineHandType()
    {
        var grouped = CardHand.GroupBy(x => x).ToList();
        var max = grouped.Max(x => x.Count());
        var min = grouped.Min(x => x.Count());

        if (CardHand.Contains('J'))
        {
            if (CardHand != "JJJJJ")
            {
                var groupedWithoutJ = CardHand.Where(x => x != 'J').GroupBy(x => x).ToList();
                max = groupedWithoutJ.Max(x => x.Count());
                min = groupedWithoutJ.Max(x => x.Count());
            }

            HandType = new { grpCount = grouped.Count, max, min, jCount = grouped.Where(x => x.Contains('J')).SelectMany(x => x).Count() } switch
            {
                { grpCount: 1 } or { grpCount: 2 } => HandType.FiveOfAKind,
                { grpCount: 3, max: 3 } or { grpCount: 3, max: 2, jCount: 2 } or { grpCount: 3, jCount: 3 } => HandType.FourOfAKind,
                { grpCount: 3, max: 2, jCount: 1 } => HandType.FullHouse,
                { grpCount: 4, jCount: 2 } or { grpCount: 4, max: 2 } => HandType.ThreeOfAKind,
                { grpCount: 5 } => HandType.OnePair,
                _ => HandType.None
            };

            Console.WriteLine($"Cards: {CardHand}, HandType: {HandType}");
        }
        else
        {
            HandType = new { grpCount = grouped.Count, max, min } switch
            {
                { grpCount: 1 } => HandType.FiveOfAKind,
                { max: 4 } => HandType.FourOfAKind,
                { grpCount: 2, max: 3, min: 2 } => HandType.FullHouse,
                { grpCount: 3, max: 3 } => HandType.ThreeOfAKind,
                { grpCount: 3, max: 2 } => HandType.TwoPair,
                { grpCount: 4 } => HandType.OnePair,
                { grpCount: 5 } => HandType.HighCard,
                _ => HandType.None
            };
        }
    }
}

public enum HandType
{
    None = 0,
    HighCard = 1,
    OnePair = 2,
    TwoPair = 3,
    ThreeOfAKind = 4,
    FullHouse = 5,
    FourOfAKind = 6,
    FiveOfAKind = 7
}

public static class Constants
{
    public static readonly ReadOnlyDictionary<char, int> Part1CardValues = new Dictionary<char, int>()
    {
        { 'A', 14 },
        { 'K', 13 },
        { 'Q', 12 },
        { 'J', 11 },
        { 'T', 10 },
        { '9', 9 },
        { '8', 8 },
        { '7', 7 },
        { '6', 6 },
        { '5', 5 },
        { '4', 4 },
        { '3', 3 },
        { '2', 2 },
    }.AsReadOnly();

    public static readonly ReadOnlyDictionary<char, int> Part2CardValues = new Dictionary<char, int>()
    {
        { 'A', 13 },
        { 'K', 12 },
        { 'Q', 11 },
        { 'T', 10 },
        { '9', 9 },
        { '8', 8 },
        { '7', 7 },
        { '6', 6 },
        { '5', 5 },
        { '4', 4 },
        { '3', 3 },
        { '2', 2 },
        { 'J', 1 },
    }.AsReadOnly();
}