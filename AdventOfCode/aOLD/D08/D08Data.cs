namespace AdventOfCode.aOLD.D08;

public static class D08Data
{
    //private const string INPUT = "D08/InputTest.txt";
    private const string INPUT = "D08/Input.txt";

    private static List<List<char>> _map = [];

    public static List<List<char>> Map
    {
        get
        {
            if (_map.Count == 0)
            {
                _map = File.ReadAllLines(INPUT)
               .Select(l => l.ToCharArray().ToList())
               .ToList();
            }
            return _map;
        }
    }

    public static void Display(List<List<char>> map)
    {
        //Thread.Sleep(50);
        //Console.Clear();
        Console.SetCursorPosition(0, 0);
        for (var y = 0; y < map.Count; y++)
        {
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
