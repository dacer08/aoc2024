namespace AdventOfCode.aOLD.D15;

public static class DataD15
{
    private const string INPUT = "D15/InputTest.txt";
    //private const string INPUT = "D15/Input.txt";

    public static Coordinate Robot;

    public static List<char> Movements = [];

    public static List<List<char>> Map = [];

    public static List<List<char>> Map2 = [];

    public static void Read()
    {
        var lines = File.ReadAllLines(INPUT);

        var i = 0;
        var found = false;
        while (i < lines.Length && lines[i] != "")
        {
            Map.Add(lines[i].ToCharArray().ToList());
            Map2.Add(lines[i]
                .Replace("O", "[]")
                .Replace(".", "..")
                .Replace("#", "##")
                .Replace("@", "@.")
                .ToCharArray().ToList());
            if (!found)
            {
                var x = lines[i].IndexOf('@');
                if (x > -1)
                {
                    found = true;
                    Robot = new Coordinate(x, i);
                }
            }
            i++;
        }

        i++;

        while (i < lines.Length && lines[i] != "")
        {
            Movements.AddRange(lines[i].ToCharArray());
            i++;
        }
    }

    public static void Display(List<List<char>> map)
    {
        Console.SetCursorPosition(0, 0);
        Console.Write(" ");
        for (var x = 0; x < map[0].Count; x++)
        {
            Console.Write(x%10);
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