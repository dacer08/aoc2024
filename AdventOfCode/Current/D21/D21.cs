using System.Diagnostics;
using System.Text;

namespace AdventOfCode.Current.D21;

public class D21
{
    private readonly Stopwatch Watch;

    public D21()
    {
        Watch = new Stopwatch();
        Watch.Start();
    }

    public D21(Stopwatch watch)
    {
        Watch = watch;
    }

    private const string INPUT = "Current/D21/InputTest.txt";
    private static char[] NUMBERS = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9'];
    //private const string INPUT = "D21/Input.txt";

    /*
        149A
        582A
        540A
        246A
        805A
     * */

    /*
+---+---+---+
| 7 | 8 | 9 |
+---+---+---+
| 4 | 5 | 6 |
+---+---+---+
| 1 | 2 | 3 |
+---+---+---+
    | 0 | A |
    +---+---+
     */

    public Dictionary<string, string> NumericalMap = new()
        {{"02","^A" },{"29","^^>A" },{"98","<A" },{"80","vvvA" },{"17","^^A" },
        {"79",">>A" },{"45",">A" }, {"54","<A" },{"56",">A" },{"6A","vvA" },{"37","<<^^A" },
        {"9A","vvvA" },{"0A",">A" },{"5A","vv>A" },{"A0","<A" },{"A4","^^<<A" },{"A3","^A" },
        {"14","^A" },{"58","^A" },{"82","vvA" },{"40",">vvA" },{"A9","^^^A" },{"A5","<^^A" },
        {"49",">>^A" },{"A8","<^^^A" },{"A1","^<<A" },
        {"46",">>A" },{"05","^^A" },
        {"2A","v>A" },{"24","<^A" },{"A2","<^A" },};

    /*
    +---+---+
    | ^ | A |
+---+---+---+
| < | v | > |
+---+---+---+
     */
    public Dictionary<string, List<string>> DirectionalMap = new()
    {
        {"AA", new (){"AA" }},
        {"A^", new (){"A<", "<A"}},
        {"A<", new (){"Av", "v<", "<<", "<A"}},
        {"Av", new (){"A<", "<v", "vA" }},
        {"A>", new (){"Av", "vA"}},
        {"^A", new (){"A>", ">A"}},
        {"^^", new (){"AA" }},
        {"^<", new (){"Av", "v<", "<A" }},
        {"^>", new (){"Av", "v>", ">A" }},
        {"<A", new (){"A>", ">>", ">^", "^A"}},
        {"<^", new (){"A>", ">^", "^A" }},
        {"<<", new (){"AA" }},
        {"vA", new (){"A^", "^>", ">A" }},
        {">v", new (){"A<", "<A"}},
        {">A", new (){"A^", "^A"}},
        {"vv", new (){"AA" }},
        {">>", new (){"AA" }},
        {"<v", new (){"A>", ">A"}},
        {"v>", new (){"A>", ">A"}},
        {"v<", new (){"A<", "<A"}},
        {">^", new (){"A<", "<^", "^A" }},
        {"><", new (){"A<", "<<", "<A" }}
    };

    public Dictionary<string, long> Results = [];

    public Dictionary<string, long> InitResult()
    {
        return new()
        {
            {"AA", 0},
            {"A^", 0},
            {"A<", 0},
            {"Av", 0},
            {"A>", 0},
            {"^A", 0},
            {"^^", 0},
            {"^<", 0},
            {"^>", 0},
            {"<A", 0},
            {"<^", 0},
            {"<<", 0},
            {"vA", 0},
            {">v", 0},
            {">A", 0},
            {"vv", 0},
            {">>", 0},
            {"<v", 0},
            {"v>", 0},
            {"v<", 0},
            {">^", 0},
            {"><", 0}
        };
    }

    public string GetDirectionsForCode(string code)
    {
        var res = "";
        code = "A" + code;
        for (var i = 1; i < code.Length; ++i)
        {
            res += NumericalMap[new string([code[i - 1], code[i]])];
        }
        return res;
    }

    public Dictionary<string, long> GetDirectionsForDirections(string code)
    {
        var results = InitResult();
        for (var i = 1; i < code.Length; i++)
        {
            results[new string([code[i - 1], code[i]])]++;
        }
        return results;
    }

    public static long GetInt(string source)
    {
        var result = new StringBuilder();
        for (int i = 0; i < source.Length; ++i)
        {
            var c = source[i];
            if (NUMBERS.Contains(c))
            {
                result.Append(c);
            }
        }

        return long.Parse(result.ToString());
    }

    public void Run()
    {
        var codes = File.ReadLines(INPUT);
        var count = 0L;
        foreach (var code in codes)
        {
            var number = GetInt(code);
            var directions = GetDirectionsForCode(code);
            //var results = InitResult();
            var results = GetDirectionsForDirections("A" + directions);
            //Console.WriteLine($"{results.Sum(r => r.Value)}");
            for (var i = 0; i < 25; i++)
            {
                var newResults = InitResult();
                foreach (var result in results)
                {
                    var value = result.Value;
                    foreach (var direction in DirectionalMap[result.Key])
                    {
                        newResults[direction] += value;
                    }
                }
                results = newResults;
                //Console.WriteLine($"{results.Sum(r => r.Value)}");
            }
            Console.WriteLine($"{results.Sum(r => r.Value)} * {number}");

            count += number * results.Sum(r => r.Value);
        }

        Console.WriteLine($"{count}");
    }
}
