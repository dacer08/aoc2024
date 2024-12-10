namespace AdventOfCode.D04;

public class D04
{
    public List<Coordinate> Index = [];

    public List<List<char>> Map = [];

    public string XMAS = "XMAS";

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

    public bool Explore(int x, int y, int vx, int vy, int iXMAS)
    {
        if (x < 0 || y < 0 || y >= Map.Count || x >= Map[0].Count)
        {
            return false;
        }

        var c = Map[y][x];

        if (Map[y][x] == XMAS[iXMAS])
        {
            if (iXMAS + 1 == XMAS.Length)
            {
                Add(x, y);
                Count++;
                return true;
            }

            if (Explore(x + vx, y + vy, vx, vy, iXMAS + 1))
            {
                Add(x, y);
                return true;
            }
        }

        return false;
    }

    public void Run()
    {
        Map = File.ReadAllLines("D04/Input.txt")
             .Select(l => l.ToCharArray().ToList())
             .ToList();

        for (var y = 0; y < Map.Count; y++)
        {
            var line = Map[y];
            for (var x = 0; x < line.Count; x++)
            {
                Explore(x, y, -1, -1, 0);
                Explore(x, y, 0, -1, 0);
                Explore(x, y, 1, -1, 0);
                Explore(x, y, 1, 0, 0);
                Explore(x, y, 1, 1, 0);
                Explore(x, y, 0, 1, 0);
                Explore(x, y, -1, 1, 0);
                Explore(x, y, -1, 0, 0);
            }
        }

        Display();

        Console.WriteLine(Count);
    }
}
