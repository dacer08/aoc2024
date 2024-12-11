namespace AdventOfCode;

public static class DataD11
{
    //private const string INPUT = "D11/InputTest.txt";
    private const string INPUT = "D11/Input.txt";

    private static long[] _map = [];


    private static Lock x = new Lock();
    public static long[] Map
    {
        get
        {
            if (_map.Length == 0)
            {
                _map = File.ReadAllText(INPUT)
                    .Split(' ')
                    .Select(x => long.Parse(x))
                    .ToArray();
            }
            return _map;
        }
    }

    public static void Display(List<string> map)
    {
        //Console.SetCursorPosition(0, 0);
        for (var x = 0; x < map.Count; x++)
        {
            Console.Write(map[x] + " ");
        }
        Console.WriteLine();
    }

    public static void DisplayCount(int count, int pos)
    {
        lock (x)
        {

            Console.SetCursorPosition((pos * 4), 0);

            Console.Write(count);
        }
    }
}
