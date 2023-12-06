using System.Text.RegularExpressions;

namespace AoC2023;

[TestClass]
public partial class Day6 : AoCTestClass
{
    [TestMethod]
    public void Part1()
    {
        var input = DayInput.EnumerateLines().ToList();
        // var input = TestInput.EnumerateLines().ToList();
        var times = input[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse).ToArray();
        var distances = input[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse).ToArray();
        var product = 1;
        for (int i = 0; i < times.Length; i++)
        {
            var time = times[i];
            var count = 0;
            for (int j = 1; j < time; j++)
            {
                var distance = j * (time - j);
                if (distance > distances[i])
                    count++;
            }

            product *= count;
        }
        TestContext.Write($"{product}");
    }

    [TestMethod]
    public void Part2()
    {
        var input = DayInput.EnumerateLines().ToList();
        // var input = TestInput.EnumerateLines().ToList();
        var time = long.Parse(MyRegex().Replace(input[0], ""));
        var distance = long.Parse(MyRegex().Replace(input[1], ""));
        var count = 0;
        for (int j = 1; j < time; j++)
        {
            var currentDistance = j * (time - j);
            if (currentDistance > distance)
                count++;
        }
        TestContext.Write($"{count}");
    }

    private const string TestInput = """
                                     Time:      7  15   30
                                     Distance:  9  40  200
                                     """;

    [GeneratedRegex("[^0-9]")]
    private static partial Regex MyRegex();
}