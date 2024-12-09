using System.Numerics;

namespace AdventOfCode.aOLD.D07;

public class D07
{
    private long Count = 0;

    private static readonly Func<long, long, long> Add = (x, y) => x + y;

    private static readonly Func<long, long, long> Multiply = (x, y) => x * y;

    private static readonly Func<long, long, long> Concat = (x, y) => long.Parse(x.ToString() + y.ToString());

    static IEnumerable<string> CombinationsWithRepetition(IEnumerable<int> input, int length)
    {
        if (length <= 0)
            yield return "";
        else
        {
            foreach (var i in input)
                foreach (var c in CombinationsWithRepetition(input, length - 1))
                    yield return i.ToString() + c;
        }
    }

    private List<List<Func<long, long, long>>> GetOperations(int count)
    {
        var operations = new List<List<Func<long, long, long>>>();

        var o = CombinationsWithRepetition(new List<int>() { 0, 1, 2 }, count).ToList();

        for (var i = 0; i < o.Count; i++)
        {
            var lineOperations = new List<Func<long, long, long>>();
            for (var j = 0; j < o[i].Length; j++)
            {
                if (o[i][j] == '0')
                {
                    lineOperations.Add(Add);
                }
                else if (o[i][j] == '1')
                {
                    lineOperations.Add(Multiply);
                }
                else if (o[i][j] == '2')
                {
                    lineOperations.Add(Concat);
                }
            }
            operations.Add(lineOperations);
        }

        return operations;
    }


    private void Explore(long result, List<long> numbers)
    {
        var operations = GetOperations(numbers.Count);
        foreach (var lineOperations in operations)
        {
            var current = numbers[0];
            for (var x = 1; x < numbers.Count; x++)
            {
                current = lineOperations[x - 1](current, numbers[x]);
            }
            if (current == result)
            {
                Count += result;
                return;
            }
        }
    }

    public void Run()
    {

        for (var i = 0; i < D07Data.Results.Count; i++)
        {
            Explore(D07Data.Results[i], D07Data.Numbers[i]);
        }
        Console.WriteLine(Count);
    }
}
