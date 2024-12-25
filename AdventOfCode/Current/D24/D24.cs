using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AdventOfCode.Current.D24;

public class D24
{
    private readonly Stopwatch Watch;

    public D24(Stopwatch watch)
    {
        Watch = watch;
    }

    private const string INPUT = "Current/D24/InputTest.txt";

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

    public Dictionary<string, bool> Destinations = [];

    public List<OperationsD24> Operations = [];

    public void Run_P1()
    {
        var file = File.ReadAllLines(INPUT);

        var i = 0;
        while (i < file.Length && file[i] != "")
        {
            var split = file[i].Split(": ");
            Destinations[split[0]] = split[1] == "1";
            i++;
        }

        i++;

        var j = 0;
        while (i < file.Length)
        {
            var split = file[i].Split(' ');
            Operations.Add(new(split[0], split[2], split[1], split[4], j));
            i++;
            j++;
        }

        //var remaining = 0;
        while (Operations.Count > 0)
        {
            var destinations = Destinations.Select(d => d.Key).ToList();
            foreach (var destination in destinations)
            {
                var todo = Operations
                    .Where(o => destination == o.Left || destination == o.Right)
                    .ToList();

                Operations = Operations
                    .Except(todo)
                    .ToList();

                foreach (var operation in todo)
                {
                    if (Destinations.TryGetValue(operation.Left, out var left) && Destinations.TryGetValue(operation.Right, out var right))
                    {
                        var result = operation.Operand switch
                        {
                            "AND" => left & right,
                            "OR" => left | right,
                            "XOR" => left ^ right,
                            _ => throw new Exception("lol"),
                        };

                        Destinations[operation.Destination] = result;
                    }
                    else
                    {
                        Operations.Add(operation);
                    }
                }


            }
            //Console.WriteLine($"remaining: {Operations.Count}");
            //if (remaining == Operations.Count)
            //{
            //    break;
            //}
            //remaining = Operations.Count;
            //if (!Operations
            //        .Where(o => destination == o.Left || destination == o.Right)
            //        .Any())
            //{
            //    break;
            //}
        }
        var list = Destinations
            .Where(d => d.Key.StartsWith("z"))
            .OrderByDescending(d => d.Key)
            .Select(d => d.Value)
            .ToList();

        StringBuilder sb = new();

        list.ForEach(b => sb.Append(b ? '1' : '0'));
        Console.WriteLine(Convert.ToInt64(sb.ToString(), 2));
    }

    public long ConvertToNumber(char start)
    {
        var list = Destinations
            .Where(d => d.Key[0] == start)
            .OrderByDescending(d => d.Key)
            .Select(d => d.Value)
            .ToList();

        var stringBuilder = new StringBuilder();

        list.ForEach(b => stringBuilder.Append(b ? '1' : '0'));
        return Convert.ToInt64(stringBuilder.ToString(), 2);
    }

    public void GetData()
    {
        var file = File.ReadAllLines(INPUT);

        var i = 0;
        while (i < file.Length && file[i] != "")
        {
            var split = file[i].Split(": ");
            Destinations[split[0]] = split[1] == "1";
            i++;
        }

        i++;

        var j = 0;
        while (i < file.Length)
        {
            var split = file[i].Split(' ');
            Operations.Add(new(split[0], split[2], split[1], split[4], j++));
            i++;
            j++;
        }
    }

    public int MAX = 8;

    public HashSet<CombinationD24> Errors = [];

    public List<int> GetNextCombination(List<OperationsD24> operations, List<int> combination)
    {
        if (operations.Count > 0)
        {
            for (var i = 0; i < combination.Count; i++)
            {
                var n1 = combination[i++];
                var n2 = combination[i];

                CombinationD24 current;
                if (n1 > n2) current = new CombinationD24(n2, n1);
                else current = new CombinationD24(n1, n2);

                if (operations.All(o => o.Index != n1 && o.Index != n2))
                {
                    Errors.Add(current);
                }
            }

        }

        var found = false;
        while (!found)
        {
            var carry = false;
            var l = combination.Count - 1;

            combination[l]++;
            if (combination[l] >= Operations.Count)
            {
                combination[l] = 0;
                carry = true;
            }
            l--;
            for (; l >= 0 && carry; l--)
            {
                carry = false;
                var current = combination[l] + 1;
                if (current >= Operations.Count)
                {
                    current = 0;
                    carry = true;
                }
                combination[l] = current;
            }

            if (combination.Distinct().Count() == MAX)
            {
                var i = 0;
                for (; i < combination.Count; i++)
                {
                    var n1 = combination[i++];
                    var n2 = combination[i];

                    CombinationD24 current;
                    if (n1 > n2) current = new CombinationD24(n2, n1);
                    else current = new CombinationD24(n1, n2);

                    if (Errors.Contains(current))
                    {
                        break;
                    }
                }

                if (i == combination.Count && combination.Distinct().Count() == MAX)
                {
                    found = true;
                }
                else
                {
                    combination[i++]++;
                    for (; i < combination.Count; i++)
                    {
                        var start = 0;
                        var firsts = combination.Take(i).ToList();
                        while (firsts.Contains(start))
                        {
                            start++;
                        }
                        combination[i] = start;
                    }
                }
            }
        }

        return combination;
    }

    public void Run()
    {
        GetData();

        //Operations = Operations
        //    .Where(o => o.Left.StartsWith('z') && o.Right.StartsWith('z'))
        //    .Concat(Operations
        //            .Where(o => !o.Left.StartsWith('z') || !o.Right.StartsWith('z')))
        //    .ToList();

        //var remaining = 0;

        var x = ConvertToNumber('x');
        var y = ConvertToNumber('y');
        var expected = x & y;
        Console.WriteLine($"Expected: {expected}");
        var index = Enumerable.Range(0, MAX).ToList();
        var combination = index;
        var res = -1;
        var p = 0;
        while (res != expected && combination[0] < Operations.Count)
        {
            if (p % 1000 == 0)
            {
                Console.WriteLine($"Current: {p} => {string.Join(",", combination)}");
            }
            p++;
            var operations = new List<OperationsD24>(Operations);
            for (var i = 0; i < combination.Count; i++)
            {
                var a = operations[combination[i++]];
                var b = operations[combination[i]];
                operations.Add(new(a.Left, a.Right, a.Operand, b.Destination, a.Index));
                operations.Add(new(b.Left, b.Right, b.Operand, a.Destination, b.Index));
            }

            operations = operations.Where((a, b) => !combination.Contains(b)).ToList();
            var repeat = -1;
            while (operations.Count > 0)
            {
                var destinations = Destinations.Select(d => d.Key).ToList();
                foreach (var destination in destinations)
                {
                    var todo = operations
                        .Where(o => destination == o.Left || destination == o.Right)
                        .ToList();

                    foreach (var operation in todo)
                    {
                        if (Destinations.TryGetValue(operation.Left, out var left) && Destinations.TryGetValue(operation.Right, out var right))
                        {
                            var result = operation.Operand switch
                            {
                                "AND" => left & right,
                                "OR" => left | right,
                                "XOR" => left ^ right,
                                _ => throw new Exception("lol"),
                            };

                            Destinations[operation.Destination] = result;
                            operations.Remove(operation);
                        }
                    }

                }
                if (repeat == operations.Count)
                {
                    break;
                }
                repeat = operations.Count;
            }
            if (expected == ConvertToNumber('z'))
            {
                Console.WriteLine($"Result: {string.Join(",", combination)}");
                break;
            }
            combination = GetNextCombination(operations, combination);
        }
    }

    //var z = ConvertToNumber('z');
    //Console.WriteLine($"Result: {z}");
}
