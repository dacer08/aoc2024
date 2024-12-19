namespace AdventOfCode.aOLD.D16;

public static class DataD16
{
    //private const string INPUT = "D16/InputTest.txt";
    private const string INPUT = "D16/Input.txt";

    public static Coordinate Reindeer;

    public static Coordinate End;

    public static List<List<char>> Map = [];

    public static void Read()
    {
        var lines = File.ReadAllLines(INPUT);

        var i = 0;
        var foundS = false;
        var foundE = false;
        while (i < lines.Length)
        {
            Map.Add(lines[i].ToCharArray().ToList());
            
            if (!foundS)
            {
                var x = lines[i].IndexOf('S');
                if (x > -1)
                {
                    foundS = true;
                    Reindeer = new Coordinate(x, i);
                    Map[i][x] = '.';
                }
            }

            if (!foundE)
            {
                var x = lines[i].IndexOf('E');
                if (x > -1)
                {
                    foundE = true;
                    End = new Coordinate(x, i);
                    Map[i][x] = '.';
                }
            }
            i++;
        }
    }

    public static void Display(List<List<char>> map, int ms = 0)
    {
        if (ms > 0)
        {
            Thread.Sleep(ms);
        }
        Console.SetCursorPosition(0, 0);
        Console.Write(" ");
        for (var x = 0; x < map[0].Count; x++)
        {
            Console.Write(x % 10);
        }
        Console.WriteLine();
        for (var y = 0; y < map.Count; y++)
        {
            Console.Write(y%10);
            var line = map[y];
            for (var x = 0; x < line.Count; x++)
            {
                Console.Write(map[y][x]);
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}