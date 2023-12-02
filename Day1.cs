using System.Text.RegularExpressions;

namespace AoC2023;

[TestClass]
public partial class Day1 : AoCTestClass
{
    [TestMethod]
    public void Part1()
    {
        var input = DayInput.EnumerateLines();
        // var input = TestInput.EnumerateLines();
        long sum = 0;
        foreach (var line in input)
        {
            int ten = 0, one = 0;
            foreach (var ch in line)
            {
                if (char.IsNumber(ch))
                {
                    ten = int.Parse(ch.ToString());
                    break;
                }
            }

            foreach (var ch in line.Reverse())
            {
                if (char.IsNumber(ch))
                {
                    one = int.Parse(ch.ToString());
                    break;
                }
            }

            sum += ten * 10 + one;
        }

        TestContext.Write($"{sum}");
    }

    [GeneratedRegex(@"(?=([1-9]|one|two|three|four|five|six|seven|eight|nine))")]
    private static partial Regex MyRegex();

    private readonly Dictionary<string, int> _dict = new()
    {
        { "one", 1 },
        { "two", 2 },
        { "three", 3 },
        { "four", 4 },
        { "five", 5 },
        { "six", 6 },
        { "seven", 7 },
        { "eight", 8 },
        { "nine", 9 }
    };

    [TestMethod]
    public void Part2()
    {
        // var input = DayInput.EnumerateLines();
        var input = TestInput2.EnumerateLines();
        long sum = 0;
        var regex = MyRegex();
        foreach (var line in input)
        {
            var matchCollection = regex.Matches(line);
            var first = matchCollection[0].Groups[1].Value;
            var ten = first.Length == 1 ? int.Parse(first) : _dict[first];
            var last = matchCollection[^1].Groups[1].Value;
            var one = last.Length == 1 ? int.Parse(last) : _dict[last];
            sum += ten * 10 + one;
        }

        TestContext.Write($"{sum}");
    }

    private const string TestInput = """
                                     1abc2
                                     pqr3stu8vwx
                                     a1b2c3d4e5f
                                     treb7uchet
                                     """;

    private const string TestInput2 = """
                                      two1nine
                                      eightwothree
                                      abcone2threexyz
                                      xtwone3four
                                      4nineeightseven2
                                      zoneight234
                                      7pqrstsixteen
                                      """;
}
