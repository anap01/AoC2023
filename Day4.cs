namespace AoC2023;

[TestClass]
public class Day4 : AoCTestClass
{
    [TestMethod]
    public void Part1()
    {
        var input = DayInput.EnumerateLines();
        // var input = TestInput.EnumerateLines();
        var sum = input.Select(row => row.Split(":")[1].Split("|"))
            .Select(GetWinningNumbers)
            .Sum(n => n.Count > 0 ? Math.Pow(2, n.Count - 1) : 0);
        TestContext.Write($"{sum}");
    }

    private HashSet<int> GetWinningNumbers(string[] numbers)
    {
        var winning = numbers[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToHashSet();
        var having = numbers[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToHashSet();
        winning.IntersectWith(having);
        return winning;
    }

    [TestMethod]
    public void Part2()
    {
        var input = DayInput.EnumerateLines();
        // var input = TestInput.EnumerateLines();
        var count = new Dictionary<int, int>();
        foreach (var (card, winningNumbersCount) in input.Select(row => row.Split(":")[1].Split("|"))
                     .Select((strings, i) => (i + 1, GetWinningNumbers(strings).Count)))
        {
            if (count.TryGetValue(card, out var existing))
                count[card] = existing + 1;
            else
                count[card] = 1;

            for (var j = card + 1; j < card + winningNumbersCount + 1; j++)
            {
                if (count.TryGetValue(j, out var current))
                    count[j] = current + count[card];
                else
                    count[j] = count[card];
            }
        }
        
        TestContext.Write($"{count.Values.Sum()}");
    }

    private const string TestInput = """
                                     Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
                                     Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
                                     Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
                                     Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
                                     Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
                                     Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11
                                     """;
}