using System.Diagnostics;
using System.Text;

namespace AdventOfCode;

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

    private const string INPUT = "D21/InputTest.txt";
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
    public Dictionary<string, string> DirectionalMap = new()
    {
        {"A^","<A" },{"^A",">A" },{"A>","vA" },{">A","^A" },{"vA","^>A" },{"Av","<vA" },{"<A",">>^A" },
{"A<","v<<A" },{"^^","A" },{"vv","A" },{">>","A" },{"<<","A" },{"AA","A" },{"<v",">A" },
{"v>",">A" },{"v<","<A" },{">v","<A" },{"<^",">^A" },{"^<","v<A" },{"^>","v>A" },{">^","<^A" },
{"><","<<A" },{"^v","vA"}
    };

    private static bool CheckScore(Dictionary<Vector2D, HashSet<string>> scores, Vector2D position, string score)
    {
        if (scores.TryGetValue(position, out var currentScore) && currentScore.First().Length <= score.Length)
        {
            if (currentScore.First().Length < score.Length)
            {
                return false;
            }

            currentScore.Add(score);
            return true;
        }
        scores[position] = [score];
        return true;
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

    public Dictionary<string, string> Cache = [];

    public int MaxDepth = 25;
    public int Chunk = 20;

    public string GetDirectionsForDirections(string code)
    {
        var res = "";
        for (var i = 0; i < code.Length;)
        {
            var chunk = (i + Chunk);
            if (chunk > code.Length)
            {
                chunk = chunk - code.Length;
            }

            var current = code[i..chunk];

            if (Cache.TryGetValue(current, out var directions))
            {
                res += directions;
                i += Chunk;
                continue;
            }
            var currentRes = "";
            for (var x = 1; x < current.Length; ++x)
            {
                currentRes += DirectionalMap[new string([current[x - 1], current[x]])];
            }
            Cache[current] = currentRes;
            //directions = GetDirectionsForDirections(currentRes);
            res += currentRes;
            i += Chunk;
        }
        
        return res;
    }

    public string Result;

    //public void GetDirectionsForDirections(string code)
    //{
    //    var stack = new Stack<ItemsD21>();
    //    stack.Push(new ItemsD21(0, code));

    //    while (stack.TryPop(out var item))
    //    {
    //        if (!Cache.TryGetValue(item.Code, out var directions))
    //        {
    //            directions = "";
    //            for (var x = 1; x < item.Code.Length; ++x)
    //            {
    //                directions += DirectionalMap[new string([item.Code[x - 1], item.Code[x]])];
    //            }
    //            Cache[item.Code] = directions;
    //        }
    //    }
    //}

    public static int GetInt(string source)
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

        return int.Parse(result.ToString());
    }

    public void Run()
    {
        var codes = File.ReadLines(INPUT);
        var count = 0L;
        foreach (var code in codes)
        {
            //for (int i = 1; i < code.Length; ++i)
            //{
            //    if (!NumericalMap.ContainsKey(new string([code[i - 1], code[i]])))
            //    {
            //        Console.WriteLine(new string([code[i - 1], code[i]]));
            //    }
            //}

            var number = GetInt(code);
            var res = GetDirectionsForCode(code);
            //res = GetDirectionsForDirections("A" + res, 0);
            for (var i = 0; i < MaxDepth + 1; i++)
            {
                res = GetDirectionsForDirections("A" + res);
                Console.WriteLine($"{i} / {MaxDepth}");
            }

            //res = GetDirectionsForDirections(res);
            //res = GetDirectionsForDirections(res);
            //Console.WriteLine(res);
            count += number * res.Length;

            Console.WriteLine($"{res.Length} * {number}");
        }

        Console.WriteLine($"{count} / 164960");
    }
}
