namespace AdventOfCode.Y2023.D05;

public static class DataD05
{
    //private const string INPUT = "aOLD/2023/D05/InputTest.txt";
    private const string INPUT = "aOLD/2023/D05/Input.txt";

    private static List<long> _seeds = [];

    public static Dictionary<int, Rule[]> Categories = [];

    public static List<long> Seeds
    {
        get
        {
            if (_seeds.Count == 0)
            {
                Read();
            }

            return _seeds;
        }
    }

    //public static Dictionary<int, Dictionary<int, Rule>> Categories
    //{
    //    get
    //    {
    //        if (_categories.Count == 0)
    //        {
    //            Read();
    //        }

    //        return _categories;
    //    }
    //}

    public static void Read()
    {
        var lines = File.ReadAllLines(INPUT);

        _seeds = lines[0]
                .Split(':')[1]
                .Trim()
                .Split(' ')
                .Select(c => long.Parse(c))
                .ToList();

        var i = 3;
        var j = 0;
        while (i < lines.Length)
        {
            var line = lines[i];
            var rules = new List<Rule>();
            while (i < lines.Length && lines[i] != "")
            {
                line = lines[i];
                var rule = line.Split(' ').Select(c => long.Parse(c)).ToList();
                rules.Add(new Rule(rule[0], rule[1], rule[2], rule[1]+ rule[2]));
                i++;
            }
            Categories.Add(j++, rules.OrderBy(r => r.Destination).ToArray());
            i++;
            i++;
        }
    }
}
