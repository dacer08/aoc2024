namespace AdventOfCode.aOLD.D17;

public static class DataD17
{
    private const string INPUT = "D17/InputTest.txt";
    //private const string INPUT = "D17/Input.txt";

    public static long A;
    public static long B;
    public static long C;

    public static List<long> Program = [];

    public static void Read()
    {
        var lines = File.ReadAllLines(INPUT);

        A = long.Parse(lines[0].Split(' ')[2]);
        B = long.Parse(lines[1].Split(' ')[2]);
        C = long.Parse(lines[2].Split(' ')[2]);

        Program = lines[4].Split(' ')[1].Split(',').Select(long.Parse).ToList();
    }

    public static void Display(List<List<char>> map, int ms = 0)
    {
        //if (ms > 0)
        //{
        //    Thread.Sleep(ms);
        //}
        //Console.SetCursorPosition(0, 0);
        //Console.Write(" ");
        //for (var x = 0; x < map[0].Count; x++)
        //{
        //    Console.Write(x % 10);
        //}
        //Console.WriteLine();
        //for (var y = 0; y < map.Count; y++)
        //{
        //    Console.Write(y%10);
        //    var line = map[y];
        //    for (var x = 0; x < line.Count; x++)
        //    {
        //        Console.Write(map[y][x]);
        //    }
        //    Console.WriteLine();
        //}
        //Console.WriteLine();
    }
}