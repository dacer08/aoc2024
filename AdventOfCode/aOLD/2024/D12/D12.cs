namespace AdventOfCode.aOLD.D12;

public class D12
{
    public Dictionary<int, int> Perimeters = new Dictionary<int, int>();

    public Dictionary<int, int> Fences = new Dictionary<int, int>();

    public Dictionary<int, List<Coordinate>> Areas = new Dictionary<int, List<Coordinate>>();

    public HashSet<Coordinate> Coordinates = new HashSet<Coordinate>();

    private List<List<char>> Map = DataD12.Map;

    public void Explore_P1(int x, int y, int vx, int vy, char current, int id)
    {
        x += vx;
        y += vy;

        if (x < 0 || y < 0 || y >= Map.Count || x >= Map[0].Count)
        {
            Fences[id]++;
            return;
        }

        var c = Map[y][x];

        if (c != current)
        {
            Fences[id]++;
            return;
        }

        var coordinate = new Coordinate(x, y);
        if (Coordinates.Contains(coordinate))
        {
            return;
        }

        Coordinates.Add(coordinate);
        Perimeters[id]++;

        Explore(x, y, -1, 0, c, id); //left
        Explore(x, y, 0, 01, c, id); //bottom
        Explore(x, y, 1, 00, c, id); //right
        Explore(x, y, 0, -1, c, id); //top
    }

    public void Run_P1()
    {
        var id = 0;
        for (var y = 0; y < Map.Count; y++)
        {
            var line = Map[y];
            for (var x = 0; x < line.Count; x++)
            {
                if (Coordinates.Contains(new Coordinate(x, y)))
                {
                    continue;
                }

                var c = Map[y][x];

                if (!Perimeters.ContainsKey(id))
                {
                    Perimeters[id] = 0;
                    Fences[id] = 0;
                }

                Explore(x, y, 0, 0, c, id); //left

                id++;
            }
        }


        Console.WriteLine(Perimeters.Select(p => p.Value * Fences[p.Key]).Sum());
    }

    public void Explore(int x, int y, int vx, int vy, char current, int id)
    {
        x += vx;
        y += vy;

        if (x < 0 || y < 0 || y >= Map.Count || x >= Map[0].Count)
        {
            Fences[id]++;
            return;
        }

        var c = Map[y][x];

        if (c != current)
        {
            Fences[id]++;
            return;
        }

        var coordinate = new Coordinate(x, y);
        if (Coordinates.Contains(coordinate))
        {
            return;
        }

        Coordinates.Add(coordinate);
        Perimeters[id]++;

        Areas[id].Add(coordinate);

        Explore(x, y, -1, 0, c, id); //left
        Explore(x, y, 0, 01, c, id); //bottom
        Explore(x, y, 1, 00, c, id); //right
        Explore(x, y, 0, -1, c, id); //top
    }

    public void Run()
    {
        var id = 0;
        for (var y = 0; y < Map.Count; y++)
        {
            var line = Map[y];
            for (var x = 0; x < line.Count; x++)
            {
                if (Coordinates.Contains(new Coordinate(x, y)))
                {
                    continue;
                }

                var c = Map[y][x];

                if (!Perimeters.ContainsKey(id))
                {
                    Perimeters[id] = 0;
                    Fences[id] = 0;
                    Areas[id] = new List<Coordinate>();
                }

                Explore(x, y, 0, 0, c, id); //left

                id++;
            }
        }

        var corners = 0;
        var res = 0;

        foreach (var area in Areas)
        {
            var plants = area.Value;
            corners = 0;

            var dones = new HashSet<Coordinate>();
            foreach (var plant in plants)
            {
                var left = new Coordinate(plant.X - 1, plant.Y);
                var bottom = new Coordinate(plant.X, plant.Y + 1);
                var right = new Coordinate(plant.X + 1, plant.Y);
                var top = new Coordinate(plant.X, plant.Y - 1);

                var diag = new Coordinate(left.X, left.Y - 1);
                if (!plants.Any(p => p == left) && !plants.Any(p => p == top)) corners++;

                diag = new Coordinate(right.X, right.Y - 1);
                if (!plants.Any(p => p == top) && !plants.Any(p => p == right)) corners++;

                diag = new Coordinate(right.X, right.Y + 1);
                if (!plants.Any(p => p == bottom) && !plants.Any(p => p == right)) corners++;

                diag = new Coordinate(left.X, left.Y + 1);
                if (!plants.Any(p => p == left) && !plants.Any(p => p == bottom)) corners++;

                diag = new Coordinate(left.X, left.Y - 1);
                if (plants.Any(p => p == left) && plants.Any(p => p == top) && !plants.Any(p => p == diag)) corners++;

                diag = new Coordinate(right.X, right.Y - 1);
                if (plants.Any(p => p == top) && plants.Any(p => p == right) && !plants.Any(p => p == diag)) corners++;

                diag = new Coordinate(right.X, right.Y + 1);
                if (plants.Any(p => p == bottom) && plants.Any(p => p == right) && !plants.Any(p => p == diag)) corners++;

                diag = new Coordinate(left.X, left.Y + 1);
                if (plants.Any(p => p == left) && plants.Any(p => p == bottom) && !plants.Any(p => p == diag)) corners++;
            }

            res += Perimeters[area.Key] * corners;
            //res += corners;
        }

        Console.WriteLine(res);
    }
}
