namespace AdventOfCode;

public class D02
{
    public int Count { get; set; } = 0;

    public void Compute(List<List<int>> lines)
    {
        foreach (var line in lines)
        {
            var increasing = line[0] < line[1];
            var i = 1;
            var safe = true;
            for (; i < line.Count; i++)
            {
                var differ = Math.Abs(line[i - 1] - line[i]);
                if (differ > 3 || differ < 1)
                {
                    safe = false;
                    break;
                }

                if (increasing && line[i - 1] > line[i] || !increasing && line[i - 1] < line[i])
                {
                    safe = false;
                    break;
                }
            }

            if (safe)
            {
                Count++;
            }
            else
            {
                for (var j = 0; j < line.Count; j++)
                {
                    if (Comput02(line.Where((a, b) => b != j).ToList()))
                    {
                        break;
                    }
                }
            }
        }

    }

    public bool Comput02(List<int> line)
    {
        var increasing = line[0] < line[1];
        var i = 1;
        var safe = true;
        for (; i < line.Count; i++)
        {
            var differ = Math.Abs(line[i - 1] - line[i]);
            if (differ > 3 || differ < 1)
            {
                safe = false;
                break;
            }

            if (increasing && line[i - 1] > line[i] || !increasing && line[i - 1] < line[i])
            {
                safe = false;
                break;
            }
        }

        if (safe)
        {
            Count++;
        }

        return safe;
    }


    public void Run()
    {
        var lines = File.ReadAllLines("D02/Input.txt")
            .Select(l => l.Split(" ")
                            .Select(s => int.Parse(s))
                            .ToList())
            .ToList();

        Compute(lines);
        Console.WriteLine(Count);
    }
}
