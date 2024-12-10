using System.Collections;

namespace AdventOfCode.D06;

public class D06
{
    public int Count = 0;

    public bool Found = false;

    public int Vx = 0;

    public int Vy = -1;

    public int X = 0;

    public int Y = 0;

    private Dictionary<int, Dictionary<int, bool>> Positions = [];

    private Queue<Coordinate> Coordinates = [];

    public void Explore(List<List<char>> map)
    {
        Positions = [];
        Coordinates = [];
        //D06Data.Display(map);
        var i = 0;
        var len = map.Count * map[0].Count;
        while (i < len)
        {
            //D06Data.Display(map);
            int newX = X + Vx;
            int newY = Y + Vy;

            if (newX < 0 || newY < 0 || newY >= map.Count || newX >= map[0].Count)
            {
                Found = true;
                //D06Data.Display(map);
                return;
            }

            var c = map[newY][newX];

            if (c == '.' || c == 'O')
            {
                if (!Positions.ContainsKey(newY))
                {
                    Positions[newY] = new Dictionary<int, bool>();
                }
                Positions[newY][newX] = true;
                map[newY][newX] = '^';
                map[Y][X] = '.';
                X = newX;
                Y = newY;
            }
            else if (c == '#')
            {
                var tmpX = Vx;
                Vx = -Vy;
                Vy = tmpX;
                //Coordinates.Enqueue(new Coordinate(X, Y));
                //if (Coordinates.Count == 8)
                //{
                //    if (Coordinates.ElementAt(7) == Coordinates.ElementAt(3)
                //        && Coordinates.ElementAt(6) == Coordinates.ElementAt(2)
                //        && Coordinates.ElementAt(5) == Coordinates.ElementAt(1)
                //        && Coordinates.ElementAt(4) == Coordinates.ElementAt(0))
                //    {
                //        Count++;
                //        return;
                //    }
                //    Coordinates.Dequeue();
                //}
            }
            i++;
        }
        Count++;
    }

    public List<List<char>> Duplicate()
    {
        var map = DataD06.Map;
        var newMap = new List<List<char>>();
        for (var y = 0; y < map.Count; y++)
        {
            var line = map[y];
            var newLine = new List<char>();
            for (var x = 0; x < line.Count; x++)
            {
                newLine.Add(map[y][x]);
            }
            newMap.Add(newLine);
        }
        return newMap;
    }

    public void Run()
    {
        var guardFound = false;
        var y = 0;
        var x = 0;
        var map = DataD06.Map;
        while (y < map.Count && !guardFound)
        {
            var line = map[y];
            x = 0;
            while (x < line.Count && !guardFound)
            {
                if (map[y][x] == '^')
                {
                    guardFound = true;
                }
                x++;
            }
            y++;
        }
        var startX = x - 1;
        var startY = y - 1;

        y = 0;
        while (y < map.Count)
        {
            var line = map[y];
            x = 0;
            while (x < line.Count)
            {

                var newMap = Duplicate();
                if (newMap[y][x] != '#' && newMap[y][x] != '^')
                {
                    X = startX;
                    Y = startY;
                    Vx = 0;

                    Vy = -1;
                    newMap[y][x] = '#';
                    Explore(newMap);
                }
                x++;
            }
            y++;
        }

        //Console.WriteLine(Positions.Sum(py => py.Value.Count(px => px.Value)));
        Console.WriteLine(Count);
    }
}
