//using AdventOfCode.Pending.D20;
//using System.Diagnostics;
//using System.Runtime.InteropServices;
//using System.Text;

//namespace AdventOfCode;

//public class D21
//{
//    private readonly Stopwatch Watch;

//    public D21()
//    {
//        Watch = new Stopwatch();
//        Watch.Start();
//    }

//    public D21(Stopwatch watch)
//    {
//        Watch = watch;
//    }

//    private const string INPUT = "D21/InputTest.txt";
//    private static char[] NUMBERS = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9'];
//    //private const string INPUT = "D21/Input.txt";

//    public Dictionary<Vector2D, string> NumericalKeyboard = new()
//    {
//        { new Vector2D(0, 0), "7" },
//        { new Vector2D(1, 0), "8" },
//        { new Vector2D(2, 0), "9" },
//        { new Vector2D(0, 1), "4" },
//        { new Vector2D(1, 1), "5" },
//        { new Vector2D(2, 1), "6" },
//        { new Vector2D(0, 2), "1" },
//        { new Vector2D(1, 2), "2" },
//        { new Vector2D(2, 2), "3" },
//        { new Vector2D(1, 3), "0" },
//        { new Vector2D(2, 3), "A" }
//    };

//    public Dictionary<string, HashSet<string>> NumericalMap = [];

//    public Dictionary<Vector2D, string> DirectionalKeyboard = new()
//    {
//        { new Vector2D(1, 0), "^" },
//        { new Vector2D(2, 0), "A" },
//        { new Vector2D(0, 1), "<" },
//        { new Vector2D(1, 1), "v" },
//        { new Vector2D(2, 1), ">" }
//    };

//    public Dictionary<string, HashSet<string>> DirectionalMap = [];



//    private static bool CheckScore(Dictionary<Vector2D, HashSet<string>> scores, Vector2D position, string score)
//    {
//        if (scores.TryGetValue(position, out var currentScore) && currentScore.First().Length <= score.Length)
//        {
//            if (currentScore.First().Length < score.Length)
//            {
//                return false;
//            }

//            currentScore.Add(score);
//            return true;
//        }
//        scores[position] = [score];
//        return true;
//    }

//    public static void ComputeMap(Dictionary<Vector2D, string> keyboard, Dictionary<string, HashSet<string>> map)
//    {
//        //var a = Combinations.GetKCombs(keyboard.Select(k => k.Key), 2).ToList();
//        foreach (var couple in Permutations.GetPermutations(keyboard.Select(k => k.Key), 2))
//        {
//            var scores = new Dictionary<Vector2D, HashSet<string>>();
//            var queue = new Queue<QueueItemD21>();
//            //Vector2D start = new(2, 3);// couple.First();
//            //Vector2D end = new(0, 2); //couple.Last();
//            Vector2D start = couple.First();
//            Vector2D end = couple.Last();
//            //queue.Enqueue(new(start, Vector2D.TOP, ""));
//            //queue.Enqueue(new(start, Vector2D.LEFT, ""));
//            queue.Enqueue(new(start, Vector2D.RIGHT, ""));
//            queue.Enqueue(new(start, Vector2D.BOTTOM, ""));
//            queue.Enqueue(new(start, Vector2D.TOP, ""));
//            queue.Enqueue(new(start, Vector2D.LEFT, ""));

//            var bestScore = int.MaxValue;
//            while (queue.TryDequeue(out var current))
//            {
//                if (current.Keys.Length > bestScore)
//                {
//                    continue;
//                }
//                var keys = current.Keys;

//                var v = current.Vector;
//                var v90 = Vector2D.Rotate90d(v);
//                var vMinus90 = Vector2D.RotateMinus90d(v);
//                var position = current.Position;

//                var v90s = ">";
//                if (v90 == Vector2D.BOTTOM) v90s = "v";
//                else if (v90 == Vector2D.LEFT) v90s = "<";
//                else if (v90 == Vector2D.TOP) v90s = "^";

//                var vMinus90s = ">";
//                if (vMinus90 == Vector2D.BOTTOM) vMinus90s = "v";
//                else if (vMinus90 == Vector2D.LEFT) vMinus90s = "<";
//                else if (vMinus90 == Vector2D.TOP) vMinus90s = "^";

//                var currentS = ">";
//                if (v == Vector2D.BOTTOM) currentS = "v";
//                else if (v == Vector2D.LEFT) currentS = "<";
//                else if (v == Vector2D.TOP) currentS = "^";

//                while (position != end && keyboard.TryGetValue(position, out var value))
//                {
//                    if (value.Length > bestScore)
//                    {
//                        break;
//                    }
//                    var coordinate = Vector2D.Add(position, v90);
//                    if (keyboard.TryGetValue(coordinate, out var next) && CheckScore(scores, coordinate, keys + v90s))
//                    {
//                        queue.Enqueue(new(coordinate, v90, keys + v90s));
//                    }

//                    coordinate = Vector2D.Add(position, vMinus90);
//                    if (keyboard.TryGetValue(coordinate, out next) && CheckScore(scores, coordinate, keys + vMinus90s))
//                    {
//                        queue.Enqueue(new(coordinate, vMinus90, keys + vMinus90s));
//                    }

//                    position = Vector2D.Add(position, v);
//                    keys += currentS;
//                }

//                CheckScore(scores, position, keys);
//                if (position == end && scores[end].First().Length < bestScore)
//                {
//                    bestScore = scores[end].First().Length;
//                }
//            }

//            if (scores.TryGetValue(end, out var endScore))
//                map[keyboard[start] + keyboard[end]] = endScore;
//        }
//    }

//    public List<string> GetDirectionsForCode(Dictionary<string, HashSet<string>> map, List<string> codes)
//    {
//        var results = new List<string>();
//        var newResults = new List<string>(results);
//        var j = 0;
//        //Console.WriteLine("****************************");
//        foreach (var code in codes)
//        {
//            //Console.WriteLine($"{j} / {codes.Count}");
//            j++;
//            newResults = new List<string>();
//            if (newResults.Count == 0)
//            {
//                newResults.Add("");
//            }

//            var start = 'A';
//            for (int i = 0; i < code.Length; ++i)
//            {
//                var buffers = new List<string>();
//                var end = code[i];

//                if (end != start)
//                {
//                    var possibles = map[new string([start, end])];
//                    foreach (var x in newResults)
//                    {
//                        foreach (var y in possibles)
//                        {
//                            buffers.Add(x + y);
//                        }
//                    }
//                }

//                if (buffers.Count == 0)
//                {
//                    buffers = newResults;
//                }

//                for (int i1 = 0; i1 < buffers.Count; i1++)
//                {
//                    buffers[i1] += "A";
//                }

//                start = end;
//                newResults = buffers;
//            }
//            results.AddRange(newResults);
//        }
//        results = results.OrderBy(x => x.Length).ToList();
//        var min = results.First().Length;
//        return results.TakeWhile(r => r.Length == min).ToList();
//    }

//    public static int GetInt(string source)
//    {
//        var result = new StringBuilder();
//        for (int i = 0; i < source.Length; ++i)
//        {
//            var c = source[i];
//            if (NUMBERS.Contains(c))
//            {
//                result.Append(c);
//            }
//        }

//        return int.Parse(result.ToString());
//    }

//    public void Run()
//    {
//        ComputeMap(NumericalKeyboard, NumericalMap);
//        ComputeMap(DirectionalKeyboard, DirectionalMap);

//        var codes = File.ReadLines(INPUT);

//        var res = 0;
//        foreach (var code in codes)
//        {
//            var number = GetInt(code);
//            var result = GetDirectionsForCode(NumericalMap, [code]);
//            Console.WriteLine($"**********************");
//            for (var i = 0; i < 24; i++)
//            {
//                result = GetDirectionsForCode(DirectionalMap, result);
//                Console.WriteLine($"{i}");
//            }
//            //Console.WriteLine($"{code}: {result.First().Length} * {number} ({result}) ");
//            res += number * result.First().Length;
//        }

//        Console.WriteLine(res);
//    }
//}
