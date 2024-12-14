using System.Numerics;

namespace AdventOfCode.D07;

public static class DataD07
{
    //private const string INPUT = "D07/InputTest.txt";
    private const string INPUT = "D07/Input.txt";

    private static List<long> _results = [];

    private static List<List<long>> _numbers = [];

    private static void GetData()
    {
        var lines = File.ReadAllLines(INPUT);
        var splits = lines.Select(l => l.Split(':').ToList());

        foreach (var split in splits)
        {
            _results.Add(long.Parse(split[0]));

            var numbers = split[1]
                .Trim()
                .Split(' ')
                .Select(n => long.Parse(n))
                .ToList();

            _numbers.Add(numbers);
        }
    }

    public static List<long> Results
    {
        get
        {
            if (_results.Count == 0)
            {
                GetData();
            }
            return _results;
        }
    }

    public static List<List<long>> Numbers
    {
        get
        {
            if (_numbers.Count == 0)
            {
                GetData();
            }
            return _numbers;
        }
    }

    public static void Display(List<List<char>> map)
    {
        //for (var x = 0; x < line.Count; x++)
        //{
        //    Console.Write(map[y][x]);
        //}
    }
}
