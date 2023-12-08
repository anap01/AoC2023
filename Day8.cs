using System.Numerics;
using System.Text.RegularExpressions;

namespace AoC2023;

[TestClass]
public class Day8 : AoCTestClass
{
    [TestMethod]
    public void Part1()
    {
        using var reader = new StringReader(DayInput);
        // using var reader = new StringReader(TestInput1);
        // using var reader = new StringReader(TestInput2);
        var directions = new Queue<char>(reader.ReadLine()!);
        reader.ReadLine();
        var map = GetMap(reader);

        var count = 0;
        var start = "AAA";
        while (start != "ZZZ")
        {
            var dir = directions.Dequeue();
            start = dir switch
            {
                'L' => map[start].Item1,
                'R' => map[start].Item2,
                _ => throw new Exception($"Unexpected direction: {dir}")
            };

            directions.Enqueue(dir);
            count++;
        }
        TestContext.Write($"{count}");
    }

    [TestMethod]
    public void Part2()
    {
        using var reader = new StringReader(DayInput);
        // using var reader = new StringReader(TestInput3);
        var directions = new Queue<char>(reader.ReadLine()!);
        reader.ReadLine();
        var map = GetMap(reader);

        var count = 0;
        var start = map.Keys.Where(k => k[2] == 'A').ToList();
        var initial = start.ToList();
        var loop = new List<List<long>>();
        for (var i = 0; i < start.Count; i++)
        {
            loop.Add(new List<long>());
        }
        while (loop.Any(v => v.Count < 1))
        {
            var dir = directions.Dequeue();
            for (int i = 0; i < start.Count; i++)
            {
                start[i] = dir switch
                {
                    'L' => map[start[i]].Item1,
                    'R' => map[start[i]].Item2,
                    _ => throw new Exception($"Unexpected direction: {dir}")
                };
            }

            directions.Enqueue(dir);
            count++;
            for (int i = 0; i < start.Count; i++)
            {
                if (start[i][2] == 'Z')
                {
                    loop[i].Add(count);
                }
            }
        }

        for (int i = 0; i < loop.Count; i++)
        {
            TestContext.WriteLine($"{loop[i][0]/271}");
        }

        var product = loop.Select(l => l[0]/271).Aggregate(271L, (a, b) => a * b);
        TestContext.WriteLine($"{product}");
    }

    private static Dictionary<string, (string, string)> GetMap(StringReader reader)
    {
        var map = new Dictionary<string, (string, string)>();
        while (reader.ReadLine() is { } line)
        {
            var match = Regex.Match(line, @"(\w{3}) = \((\w{3}), (\w{3})\)");
            var node = match.Groups[1].Value;
            var left = match.Groups[2].Value;
            var right = match.Groups[3].Value;
            map.Add(node, (left, right));
        }

        return map;
    }

    private const string TestInput1 = """
                                      RL

                                      AAA = (BBB, CCC)
                                      BBB = (DDD, EEE)
                                      CCC = (ZZZ, GGG)
                                      DDD = (DDD, DDD)
                                      EEE = (EEE, EEE)
                                      GGG = (GGG, GGG)
                                      ZZZ = (ZZZ, ZZZ)
                                      """;
    
    private const string TestInput2 = """
                                      LLR
                                      
                                      AAA = (BBB, BBB)
                                      BBB = (AAA, ZZZ)
                                      ZZZ = (ZZZ, ZZZ)
                                      """;
    
    private const string TestInput3 = """
                                      LR
                                      
                                      11A = (11B, XXX)
                                      11B = (XXX, 11Z)
                                      11Z = (11B, XXX)
                                      22A = (22B, XXX)
                                      22B = (22C, 22C)
                                      22C = (22Z, 22Z)
                                      22Z = (22B, 22B)
                                      XXX = (XXX, XXX)
                                      """;
}