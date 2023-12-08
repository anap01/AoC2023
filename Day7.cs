using System.Text.RegularExpressions;

namespace AoC2023;

[TestClass]
public class Day7 : AoCTestClass
{
    [TestMethod]
    public void Part1()
    {
        var input = DayInput.EnumerateLines();
        // var input = TestInput.EnumerateLines();
        var sortedHands = input.Select(row => row.Split(" "))
            .OrderBy(k => k[0], new PokerHandComparer())
            .ToList();
        var totalWinnings = sortedHands.Select((hand, rank) => int.Parse(hand[1]) * (rank + 1))
            .Sum();
        TestContext.Write($"{totalWinnings}");
    }

    [TestMethod]
    public void Part2()
    {
        var input = DayInput.EnumerateLines();
        // var input = TestInput.EnumerateLines();
        TestContext.Write($"");
    }

    private const string TestInput = """
                                     32T3K 765
                                     T55J5 684
                                     KK677 28
                                     KTJJT 220
                                     QQQJA 483
                                     """;
}

public class PokerHandComparer : IComparer<string>
{
    
    public int Compare(string x, string y)
    {
        var strength = GetStrength(x);
        var yStrength = GetStrength(y);
        if (strength > yStrength)
            return 1;
        if (strength < yStrength)
            return -1;

        for (int i = 0; i < 5; i++)
        {
            if (PokerHandSorter.Map(y[i]) < PokerHandSorter.Map(x[i]))
                return 1;
            if (PokerHandSorter.Map(y[i]) > PokerHandSorter.Map(x[i]))
                return -1;
        }

        return 0;
    }

    private string Remap(string input)
    {
        return input.Replace('T', 'A')
            .Replace('J', 'B')
            .Replace('Q', 'C')
            .Replace('K', 'D')
            .Replace('A', 'E');
    }

    private int GetStrength(string hand)
    {
        var sorted = Sort(hand);
        if (Regex.IsMatch(sorted, @"(.)\1{4}"))
            return 8;
        if (Regex.IsMatch(sorted, @"^(.)\1{3}"))
            return 7;
        if (Regex.IsMatch(sorted, @"^(.)\1{1,2}(.)\2{1,2}$"))
            return 6;
        if (Regex.IsMatch(sorted, @"(.)\1{2}"))
            return 5;
        if (Regex.IsMatch(sorted, @"(.)\1.?(.)\2"))
            return 4;
        if (Regex.IsMatch(sorted, @"(.)\1"))
            return 3;
        return 1;
    }

    private string Sort(string hand)
    {
        return new string(hand.OrderBy(c => c, new PokerHandSorter()).ToArray());
    }
}

internal class PokerHandSorter : IComparer<char>
{
    public int Compare(char x, char y)
    {
        return Map(y).CompareTo(Map(x));
    }

    public static char Map(char c)
    {
        return c switch
        {
            'T' => 'A',
            'J' => 'B',
            'Q' => 'C',
            'K' => 'D',
            'A' => 'E',
            _ => c
        };
    }
}