using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode.D10;

public class D10
{
    public void Explore_P1(int x, int y, int vx, int vy, int scale, HashSet<Coordinate> coordinates)
    {
        x = x + vx;
        y = y + vy;
        scale = scale + 1;

        var map = DataD10.Map;
        if (x < 0 || y < 0 || y >= map.Count || x >= map[0].Count)
        {
            return;
        }

        var currentScale = map[y][x];

        if (currentScale == scale && currentScale == 9)
        {
            coordinates.Add(new Coordinate(x, y));
            return;
        }


        if (currentScale == scale)
        {
            Explore_P1(x, y, -1, 0, scale, coordinates); //left
            Explore_P1(x, y, 0, 1, scale, coordinates); //bottom
            Explore_P1(x, y, 1, 0, scale, coordinates); //right
            Explore_P1(x, y, 0, -1, scale, coordinates); //top
        }
    }

    public void Run_P1()
    {
        var map = DataD10.Map;
        var count = 0;
        for (var y = 0; y < map.Count; y++)
        {
            var line = map[y];
            //Parallel.For(0, 0, (x) =>
            //{
            //    if (map[y][x] == 0)
            //    {
            //        var coordinates = new HashSet<Coordinate>();
            //        Explore(x, y, -1, 0, 0, coordinates);
            //        Explore(x, y, 0, 1, 0, coordinates);
            //        Explore(x, y, 1, 0, 0, coordinates);
            //        Explore(x, y, 0, -1, 0, coordinates);
            //        count += coordinates.Count;
            //    }
            //});
            for (var x = 0; x < line.Count; x++)
            {
                if (map[y][x] == 0)
                {
                    var coordinates = new HashSet<Coordinate>();
                    Explore_P1(x, y, -1, 0, 0, coordinates);
                    Explore_P1(x, y, 0, 1, 0, coordinates);
                    Explore_P1(x, y, 1, 0, 0, coordinates);
                    Explore_P1(x, y, 0, -1, 0, coordinates);
                    count += coordinates.Count;
                }
            }
        }
        Console.WriteLine(count);
    }

    public int Count = 0;

    public void Explore(int x, int y, int vx, int vy, int scale)
    {
        x = x + vx;
        y = y + vy;
        scale = scale + 1;

        var map = DataD10.Map;
        if (x < 0 || y < 0 || y >= map.Count || x >= map[0].Count)
        {
            return;
        }

        var currentScale = map[y][x];

        if (currentScale == scale && currentScale == 9)
        {
            Count++;
            return;
        }


        if (currentScale == scale)
        {
            Explore(x, y, -1, 0, scale); //left
            Explore(x, y, 0, 1, scale); //bottom
            Explore(x, y, 1, 0, scale); //right
            Explore(x, y, 0, -1, scale); //top
        }
    }

    public void Run()
    {
        var map = DataD10.Map;
        for (var y = 0; y < map.Count; y++)
        {
            var line = map[y];
            //Parallel.For(0, 0, (x) =>
            //{
            //    if (map[y][x] == 0)
            //    {
            //        var coordinates = new HashSet<Coordinate>();
            //        Explore(x, y, -1, 0, 0, coordinates);
            //        Explore(x, y, 0, 1, 0, coordinates);
            //        Explore(x, y, 1, 0, 0, coordinates);
            //        Explore(x, y, 0, -1, 0, coordinates);
            //        count += coordinates.Count;
            //    }
            //});
            for (var x = 0; x < line.Count; x++)
            {
                if (map[y][x] == 0)
                {
                    Explore(x, y, -1, 0, 0);
                    Explore(x, y, 0, 1, 0);
                    Explore(x, y, 1, 0, 0);
                    Explore(x, y, 0, -1, 0);
                }
            }
        }
        Console.WriteLine(Count);
    }
}
