namespace AdventOfCode.D08;

public class D08
{
    private HashSet<Coordinate> Nodes = [];

    public void Run()
    {
        var map = DataD08.Map;
        var newMap = new List<List<char>>();
        var chars = map
            .SelectMany(m => m.Where(c => c != '.').ToList())
            .Distinct()
            .ToList();

        Dictionary<char, List<Coordinate>> coordinates = [];

        for (var y = 0; y < map.Count; y++)
        {
            var line = map[y];
            var newLine = new List<char>();
            for (var x = 0; x < line.Count; x++)
            {
                newLine.Add('.');
                var c = line[x];
                if (c != '.')
                {
                    if (!coordinates.ContainsKey(c))
                    {
                        coordinates[c] = new List<Coordinate>();
                    }
                    coordinates[c].Add(new Coordinate(x, y));
                }
            }
            newMap.Add(newLine);
        }

        foreach (var coordinate in coordinates)
        {
            for (var i = 0; i < coordinate.Value.Count; i++)
            {
                for (var j = i + 1; j < coordinate.Value.Count; j++)
                {
                    var x = coordinate.Value[j].X - coordinate.Value[i].X;
                    var y = coordinate.Value[j].Y - coordinate.Value[i].Y;

                    var iy = coordinate.Value[i].Y + y;
                    var ix = coordinate.Value[i].X + x;

                    while (ix >= 0 && iy >= 0 && iy < newMap.Count && ix < newMap[0].Count)
                    {
                        newMap[iy][ix] = '#';
                        Nodes.Add(new Coordinate(ix, iy));

                        iy += y;
                        ix += x;
                    }

                    iy = coordinate.Value[j].Y - y;
                    ix = coordinate.Value[j].X - x;

                    while (ix >= 0 && iy >= 0 && iy < newMap.Count && ix < newMap[0].Count)
                    {
                        newMap[iy][ix] = '#';
                        Nodes.Add(new Coordinate(ix, iy));

                        iy -= y;
                        ix -= x;
                    }

                    DataD08.Display(newMap);
                }

            }
        }

        Console.WriteLine(Nodes.Count);
    }
}
