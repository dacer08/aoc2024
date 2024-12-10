namespace AdventOfCode.aOLD.D09;

public static class D09Data
{
    //private const string INPUT = "D09/InputTest.txt";
    private const string INPUT = "D09/Input.txt";

    private static List<int> _map = [];

    public static List<int> Map
    {
        get
        {
            if (_map.Count == 0)
            {
                _map = File.ReadAllText(INPUT)
               .Select(l => l - '0')
               .ToList();
            }
            return _map;
        }
    }

    public static void Display(List<int> map)
    {
        //Console.SetCursorPosition(0, 0);
        for (var x = 0; x < map.Count; x++)
        {
            Console.Write(map[x]);
        }
        Console.WriteLine();
    }
}
