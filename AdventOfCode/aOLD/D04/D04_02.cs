namespace AdventOfCode;

public class D04_02
{
    public List<Coordinate> Index = [];

    public List<List<char>> Map = [];

    public string MAS = "MAS";

    public int Count = 0;

    public void Add(int x, int y)
    {
        Index.Add(new Coordinate(x, y));
    }

    public void Display()
    {
        Index = Index
            .Distinct()
            //.OrderBy(i => i.Item1)
            //.ThenBy(i => i.Item2)
            .ToList();

        for (var y = 0; y < Map.Count; y++)
        {
            var line = Map[y];
            for (var x = 0; x < line.Count; x++)
            {
                //Console.Write(Map[y][x]);
                if (Index.Any(i => i.X == x && i.Y == y))
                {
                    Console.Write(Map[y][x]);
                }
                else
                {
                    Console.Write(".");
                }
            }
            Console.WriteLine();
        }
    }

    public void Explore(int x, int y)
    {
        var c = Map[y][x];

        if (Map[y - 1][x - 1] == 'M' && Map[y + 1][x + 1] == 'S')
        {
            if (Map[y + 1][x - 1] == 'M' && Map[y - 1][x + 1] == 'S')
            {
                Add(x, y);

                Add(x - 1, y - 1);
                Add(x + 1, y + 1);

                Add(x - 1, y + 1);
                Add(x + 1, y - 1);

                Count++;
            }
            else if (Map[y + 1][x - 1] == 'S' && Map[y - 1][x + 1] == 'M')
            {
                Add(x, y);

                Add(x - 1, y - 1);
                Add(x + 1, y + 1);

                Add(x - 1, y + 1);
                Add(x + 1, y - 1);

                Count++;
            }

        }
        else if (Map[y - 1][x - 1] == 'S' && Map[y + 1][x + 1] == 'M')
        {
            if (Map[y + 1][x - 1] == 'M' && Map[y - 1][x + 1] == 'S')
            {
                Add(x, y);

                Add(x - 1, y - 1);
                Add(x + 1, y + 1);

                Add(x - 1, y + 1);
                Add(x + 1, y - 1);

                Count++;
            }
            else if (Map[y + 1][x - 1] == 'S' && Map[y - 1][x + 1] == 'M')
            {
                Add(x, y);

                Add(x - 1, y - 1);
                Add(x + 1, y + 1);

                Add(x - 1, y + 1);
                Add(x + 1, y - 1);

                Count++;
            }

        }

    }

    public void Run()
    {
        Map = File.ReadAllLines("D04/Input.txt")
             .Select(l => l.ToCharArray().ToList())
             .ToList();

        for (var y = 1; y < Map.Count - 1; y++)
        {
            var line = Map[y];
            for (var x = 1; x < line.Count - 1; x++)
            {
                if (Map[y][x] == 'A')
                {
                    Explore(x, y);
                }
            }
        }

        Display();

        Console.WriteLine(Count);
    }
}
