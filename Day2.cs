using System.Text.RegularExpressions;

namespace AoC2022;

[TestClass]
public partial class Day2 : AoCTestClass
{
    [GeneratedRegex(@"(\d+) (\w+)")]
    private static partial Regex MyRegex();

    [TestMethod]
    public void Part1()
    {
        var input = DayInput.EnumerateLines();
        // var input = TestInput.EnumerateLines();
        var result = input.Select(game => game.Split(":"))
            .Select(l => (int.Parse(l[0][5..]), l[1].Split(";").Select(
                set => MyRegex().Matches(set)
                    .ToDictionary(m => m.Groups[2].Value, v => int.Parse(v.Groups[1].Value)))))
            .Where(d => d.Item2.All(d => d.All(d2 => d2.Value <= Limit(d2.Key))))
            .Sum(d => d.Item1);
        TestContext.Write($"{result}");
    }

    private int Limit(string color)
    {
        return color switch
        {
            "red" => 12,
            "green" => 13,
            "blue" => 14,
            _ => throw new Exception($"Unexpected color {color}")
        };
    }

    [TestMethod]
    public void Part2()
    {
        var input = DayInput.EnumerateLines();
        // var input = TestInput.EnumerateLines();
        var result = input.Select(game => game.Split(":"))
            .Select(l => l[1].Split(";").Select(
                    set => MyRegex().Matches(set)
                        .ToDictionary(m => m.Groups[2].Value, v => int.Parse(v.Groups[1].Value)))
                .Aggregate((a, b) =>
                {
                    foreach (var kvp in b)
                    {
                        if (a.TryGetValue(kvp.Key, out var v))
                        {
                            if (v < kvp.Value)
                                a[kvp.Key] = kvp.Value;
                        }
                        else
                        {
                            a[kvp.Key] = kvp.Value;
                        }
                    }

                    return a;
                }))
            .Sum(d => d.Values.Aggregate((a, b) => a * b));
        TestContext.Write($"{result}");
    }

    private const string TestInput = """
                                     Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
                                     Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
                                     Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
                                     Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
                                     Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green
                                     """;
}