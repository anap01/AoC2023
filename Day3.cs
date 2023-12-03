using System.Collections;
using System.Text.RegularExpressions;

namespace AoC2023;

[TestClass]
public class Day3 : AoCTestClass
{
    [TestMethod]
    public void Part1()
    {
        var input = DayInput.EnumerateLines();
        // var input = TestInput.EnumerateLines();
        var numbers = new Dictionary<(int, int), string>();
        var parts = new Dictionary<(int, int), string>();
        foreach (var (name, row, col, value) in input.SelectMany((row, rowIndex) => Regex.Matches(row, @"(?<number>\d+)|(?<part>[^0-9.])")
                     .SelectMany(match => match.Groups.Skip<Group>(1).Where(g => g.Success)).Select(group => (group.Name, rowIndex, group.Index, group.Value))))
        {
            switch (name)
            {
                case "number":
                    numbers.Add((row, col), value);
                    break;
                case "part":
                    parts.Add((row, col), value);
                    break;
                default:
                    throw new Exception("Unexpected input");
            }
        }

        var sum = 0;
        foreach (var kvp in numbers)
        {
            if (IsPart(kvp, parts))
                sum += int.Parse(kvp.Value);
        }
        TestContext.Write($"{sum}");
    }

    [TestMethod]
    public void Part2()
    {
        var input = DayInput.EnumerateLines();
        // var input = TestInput.EnumerateLines();
        var numbers = new Dictionary<(int, int), string>();
        var parts = new Dictionary<(int, int), string>();
        foreach (var (name, row, col, value) in input.SelectMany((row, rowIndex) => Regex.Matches(row, @"(?<number>\d+)|(?<part>[^0-9.])")
                     .SelectMany(match => match.Groups.Skip<Group>(1).Where(g => g.Success)).Select(group => (group.Name, rowIndex, group.Index, group.Value))))
        {
            switch (name)
            {
                case "number":
                    numbers.Add((row, col), value);
                    break;
                case "part":
                    parts.Add((row, col), value);
                    break;
                default:
                    throw new Exception("Unexpected input");
            }
        }

        var gears = new Dictionary<(int, int), List<int>>();
        foreach (var number in numbers)
        {
            foreach (var neighbor in Neighbors(number))
            {
                if (parts.TryGetValue(neighbor, out var part))
                {
                    if (part == "*")
                    {
                        if (gears.TryGetValue(neighbor, out var gearnumbers) == false)
                        {
                            gears[neighbor] = gearnumbers = new List<int>();
                        }
                        gearnumbers.Add(int.Parse(number.Value));
                    }
                }
            }
        }

        var sum = gears.Values.Where(l => l.Count == 2).Sum(l => l[0]*l[1]);
        TestContext.Write($"{sum}");
    }

    private bool IsPart(KeyValuePair<(int, int), string> number, Dictionary<(int, int),string> parts)
    {
        return Neighbors(number).Any(parts.ContainsKey);
    }

    private IEnumerable<(int, int)> Neighbors(KeyValuePair<(int, int), string> number)
    {
        var row = number.Key.Item1;
        var col = number.Key.Item2;
        for (var i = row - 1; i < row + 2; i++)
        {
            for (var j = col - 1; j < col + number.Value.Length + 1; j++)
            {
                yield return (i, j);
            }
        }
    }

    private const string TestInput = """
                                     467..114..
                                     ...*......
                                     ..35..633.
                                     ......#...
                                     617*......
                                     .....+.58.
                                     ..592.....
                                     ......755.
                                     ...$.*....
                                     .664.598..
                                     """;
}