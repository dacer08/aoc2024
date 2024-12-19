namespace AdventOfCode.aOLD.D19;

public class D19
{
    private const string INPUT = "D19/InputTest.txt";
    //private const string INPUT = "D19/Input.txt";

    private List<string> Patterns = [];

    private Dictionary<string, long> Can = [];

    //private HashSet<string> Antipatterns = [];

    public long Explore(string towel, List<string> patterns)
    {
        if (towel.Length == 0)
        {
            return 1;
        }

        if (Can.TryGetValue(towel, out long value))
        {
            return value;
        }

        var count = 0L;
        foreach (var pattern in patterns.SkipWhile(p => p.Length > towel.Length))
        {
            if (towel.StartsWith(pattern))
            {
                count += Explore(towel[pattern.Length..], patterns);
            }
        }

        Can[towel] = count;
        return count;
    }

    public void Run()
    {
        var file = File.ReadAllLines(INPUT);

        Patterns = file[0]
            .Split(", ")
            .OrderByDescending(p => p.Length)
            .ToList();

        var towels = file.Skip(2).ToList();
        var i = 0;
        var count = 0;
        var total = 0L;
        foreach (var towel in towels)
        {
            //var todos = new Dictionary<int, string>();
            //todos[0] = towel;
            var current = Explore(towel, Patterns);
            if (current > 0)
            {
                count++;
            }
            total += current;
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(i++);
        }
        Console.WriteLine(total);
    }
}
