namespace AoC2023;

[TestClass]
public class Day9 : AoCTestClass
{
    [TestMethod]
    public void Part1()
    {
        var input = DayInput.EnumerateLines();
        // var input = TestInput.EnumerateLines();
        var total = input.Select(row => row.Split(" ")
                .Select(int.Parse).ToList())
            .Select(arr =>
            {
                var stack = new Stack<int>();
                stack.Push(arr[^1]);
                bool allZero;
                do
                {
                    allZero = true;
                    var newList = new List<int>();
                    for (var i = 1; i < arr.Count; i++)
                    {
                        var delta = arr[i] - arr[i - 1];
                        if (delta != 0)
                            allZero = false;
                        newList.Add(delta);
                    }
                    stack.Push(newList[^1]);
                    arr = newList;
                } while (!allZero);

                return stack.Sum();
            }).Sum();
        TestContext.Write($"{total}");
    }

    [TestMethod]
    public void Part2()
    {
        var input = DayInput.EnumerateLines();
        // var input = TestInput.EnumerateLines();
        var total = input.Select(row => row.Split(" ")
                .Select(int.Parse).ToList())
            .Select(arr =>
            {
                var stack = new Stack<int>();
                stack.Push(arr[0]);
                bool allZero;
                do
                {
                    allZero = true;
                    var newList = new List<int>();
                    for (var i = 1; i < arr.Count; i++)
                    {
                        var delta = arr[i] - arr[i - 1];
                        if (delta != 0)
                            allZero = false;
                        newList.Add(delta);
                    }
                    stack.Push(newList[0]);
                    arr = newList;
                } while (!allZero);

                return stack.Aggregate((a, b) => b - a);
            }).Sum();
        TestContext.Write($"{total}");
    }

    private const string TestInput = """
                                     0 3 6 9 12 15
                                     1 3 6 10 15 21
                                     10 13 16 21 30 45
                                     """;
}