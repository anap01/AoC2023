namespace AoC2023;

public class Mapper
{
    private readonly IList<(uint, uint, uint)> _ranges = new List<(uint, uint, uint)>();
    
    public void AddRange(uint srcstart, uint deststart, uint range)
    {
        _ranges.Add((srcstart, deststart, range));
    }

    public bool TryMapValue(uint input, out uint output)
    {
        output = 0;
        foreach (var (srcstart, deststart, range) in _ranges)
        {
            if (input < srcstart || input >= srcstart + range) 
                continue;
            output = deststart + (input - srcstart);
            return true;
        }

        return false;
    }
}

[TestClass]
public class Day5 : AoCTestClass
{
    [TestMethod]
    public void Part1()
    {
        using var reader = new StringReader(DayInput);
        // using var reader = new StringReader(TestInput);
        var seeds = reader.ReadLine()!.Split(" ")[1..].Select(uint.Parse).ToList();
        reader.ReadLine();
        var maps = ReadMappers(reader);

        var result = new List<uint>(seeds.Count);
        for (int i = 0; i < seeds.Count; i++)
        {
            var input = seeds[i];
            for (int j = 0; j < maps.Count; j++)
            {
                if (maps[j].TryMapValue(input, out var value))
                    input = value;
            }

            result.Add(input);
        }
        TestContext.Write($"{result.Min()}");
    }

    private static List<Mapper> ReadMappers(StringReader reader)
    {
        var maps = new List<Mapper>();
        while (reader.ReadLine() != null)
        {
            var line = reader.ReadLine();
            var map = new Mapper();
            while (!string.IsNullOrEmpty(line))
            {
                var numbers = line.Split(" ");
                var srcstart = uint.Parse(numbers[1]);
                var deststart = uint.Parse(numbers[0]);
                var range = uint.Parse(numbers[2]);

                map.AddRange(srcstart, deststart, range);

                line = reader.ReadLine();
            }

            maps.Add(map);
        }

        return maps;
    }

    [TestMethod]
    public void Part2()
    {
        using var reader = new StringReader(DayInput);
        // using var reader = new StringReader(TestInput);
        var seeds = reader.ReadLine()!.Split(" ")[1..].Select(uint.Parse).ToList();
        reader.ReadLine();
        var maps = ReadMappers(reader);

        var minresult = uint.MaxValue;
        for (int i = 0; i < seeds.Count; i += 2)
        {
            for (uint k = 0; k < seeds[i + 1]; k++)
            {
                var input = seeds[i] + k;
                for (int j = 0; j < maps.Count; j++)
                {
                    if (maps[j].TryMapValue(input, out var value))
                        input = value;
                }
                minresult = Math.Min(minresult, input);
            }
        }
        TestContext.Write($"{minresult}");
    }

    private const string TestInput = """
                                     seeds: 79 14 55 13

                                     seed-to-soil map:
                                     50 98 2
                                     52 50 48

                                     soil-to-fertilizer map:
                                     0 15 37
                                     37 52 2
                                     39 0 15

                                     fertilizer-to-water map:
                                     49 53 8
                                     0 11 42
                                     42 0 7
                                     57 7 4

                                     water-to-light map:
                                     88 18 7
                                     18 25 70

                                     light-to-temperature map:
                                     45 77 23
                                     81 45 19
                                     68 64 13

                                     temperature-to-humidity map:
                                     0 69 1
                                     1 0 69

                                     humidity-to-location map:
                                     60 56 37
                                     56 93 4
                                     """;
}