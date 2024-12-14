namespace AdventOfCode.D01;

public class D01
{
    public static void Run()
    {
        var lines = File.ReadAllLines("D01/Input.txt")
            .Select(l => l.Split("   "));

        var l1 = new List<int>();
        var l2 = new List<int>();
        var res = 0;

        foreach (var line in lines)
        {
            l1.Add(int.Parse(line[0]));
            l2.Add(int.Parse(line[1]));
        }

        l1.Sort();
        l2.Sort();

        for (var i = 0; i < l1.Count; i++)
        {
            res += Math.Abs(l1[i] - l2[i]);
        }

        Console.WriteLine(res);

        var g2 = l2.GroupBy(l => l)
            .ToDictionary(l => l.Key, l => l.Count());

        var res2 = 0;

        for (var i = 0; i < l1.Count; i++)
        {
            var l = l1[i];
            if (g2.ContainsKey(l))
            {
                res2 += l * g2[l];
            }
        }

        Console.WriteLine(res2);
    }
}
