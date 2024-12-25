using System.Diagnostics;
using System.Text;

namespace AdventOfCode.Pending.D25;

public class D25
{
    private readonly Stopwatch Watch;

    public D25(Stopwatch watch)
    {
        Watch = watch;
    }

    private const string INPUT = "D25/InputTest.txt";

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

    public List<List<int>> Locks = [];
    public List<List<int>> Keys = [];

    public void DisplayList(List<List<int>> list)
    {
        foreach (var l in list)
        {
            Console.WriteLine(string.Join(", ", l));
        }
    }

    public void GetData()
    {
        var file = File.ReadAllLines(INPUT);

        for (var i = 0; i < file.Length; i++)
        {
            var line = file[i];

            var isKey = false;
            if (line == ".....")
            {
                isKey = true;
                i++;
            }

            var list = new List<int>() { -1, -1, -1, -1, -1 };

            while (i < file.Length && file[i] != "")
            {
                line = file[i];
                for (var j = 0; j < line.Length; j++)
                {
                    if (line[j] == '#')
                    {
                        list[j]++;
                    }
                }
                i++;
            }

            if (isKey)
            {
                Keys.Add(list);
            }
            else
            {
                Locks.Add(list);
            }
        }
    }

    public void Run()
    {
        GetData();

        var count = 0;
        foreach (var k in Keys)
        {
            foreach (var l in Locks)
            {
                var i = 0;
                for (; i < l.Count; i++)
                {
                    if (l[i] + k[i] > 5)
                    {
                        break;
                    }
                }
                if (i == l.Count)
                {
                    count++;
                }
            }
        }
        Console.WriteLine($"Result: {count}");
    }
}
