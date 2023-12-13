using System.Collections.ObjectModel;

namespace AdventOfCode2023;

public class Day7
{
    public int Part1(string[] input)
    {
        var hands = input.Select(x => new Hand(x));
        var ordered = hands.OrderBy(x => x.HandValue);
        var multiplied = ordered.Select((hand, i) => hand.Bid * (i + 1));
        var sum = multiplied.Sum();
        return sum;
    }

    public uint Part2(string[] input)
    {
        throw new NotImplementedException();
    }
}

public class Hand
{
    public Hand(string line)
    {
        var cards = line.Split(' ', StringSplitOptions.TrimEntries)[0];

        Bid = int.Parse(line.Split(' ', StringSplitOptions.TrimEntries)[1]);
        Cards = cards;
        HandValue = cards.Select(x => Constants.CardValues[x]).Sum();
    }

    public int Bid { get; }
    public string Cards { get; }
    public int HandValue { get; }

    public override string ToString()
    {
        return $"{Cards} {Bid} - {HandValue}";
    }
}

public static class Constants
{
    public static readonly ReadOnlyDictionary<char, int> CardValues = new Dictionary<char, int>()
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
}