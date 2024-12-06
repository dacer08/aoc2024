namespace AdventOfCode;

public static class D05Data
{
    private const string INPUT = "D05/InputTest.txt";

    private static List<PageOrder> _pageOrders = [];

    public static List<PageOrder> PageOrders
    {
        get
        {
            if (_pageOrders.Count == 0)
            {
                var lines = File.ReadAllLines(INPUT);
                var i = 0;
                while (i < lines.Length && lines[i].Contains('|'))
                {
                    var line = lines[i].Split('|');

                    var p1 = int.Parse(line[0]);
                    var p2 = int.Parse(line[1]);

                    _pageOrders.Add(new PageOrder(p1, p2));

                    i++;
                }
            }
            return _pageOrders;
        }
    }

    private static List<List<int>> _map = [];
    public static List<List<int>> Map
    {
        get
        {
            if (_map.Count == 0)
            {
                var lines = File.ReadAllLines(INPUT);
                var i = 0;
                while (i < lines.Length && lines[i].Contains('|'))
                {
                    i++;
                }

                i++;

                while (i < lines.Length)
                {
                    var line = lines[i].Split(',').Select(s => int.Parse(s)).ToList();

                    _map.Add(line);

                    i++;
                }
            }
            return _map;
        }
    }

}
