using System.Diagnostics;

namespace AdventOfCode.aOLD.D23;

public class D23
{
    private readonly Stopwatch Watch;

    public D23(Stopwatch watch)
    {
        Watch = watch;
    }

    private const string INPUT = "D23/InputTest.txt";

    private Dictionary<string, List<string>> Map = [];

    private HashSet<ResultD23> Results_P1 = [];

    private HashSet<string> Results = [];

    public void Explore_P1(string pc, HashSet<string> results)
    {
        results.Add(pc);
        if (results.Count == 3)
        {
            var pc1 = results.ElementAt(0);
            var pc3 = results.ElementAt(2);
            if (Map.Any(m => m.Key == pc1 && m.Value.Contains(pc3) || m.Key == pc3 && m.Value.Contains(pc1)))
            {
                var r = results.OrderBy(r => r).ToList();

                Results_P1.Add(new(r[0], r[1], r[2]));
            }
            return;
        }

        if (Map.TryGetValue(pc, out var links))
        {
            foreach (var link in links)
            {
                Explore_P1(link, [.. results]);
            }
        }
    }

    public void Run_P1()
    {
        var links = File.ReadAllLines(INPUT);

        foreach (var link in links)
        {
            var split = link.Split('-');
            var pc1 = split[0];
            var pc2 = split[1];

            if (Map.ContainsKey(pc1))
            {
                Map[pc1].Add(pc2);
            }
            else if (Map.ContainsKey(pc2))
            {
                Map[pc2].Add(pc1);
            }
            else
            {
                Map[pc1] = [pc2];
            }
        }

        Map = Map.OrderBy(m => m.Key).ToDictionary();

        foreach (var link in Map.Select(m => m.Key))
        {
            Explore_P1(link, []);
        }

        var query = Results_P1; ;

        foreach (var result in Results_P1)
        {
            Console.WriteLine($"{result.Pc1}, {result.Pc2}, {result.Pc3}");
        }

        Console.WriteLine(Results_P1.Where(r => r.Pc1.StartsWith('t') || r.Pc2.StartsWith('t') || r.Pc3.StartsWith('t')).Count());
    }

    public void Explore(string pc, HashSet<string> results)
    {
        if (results.All(r => Map.ContainsKey(r) && Map[r].Contains(pc)))
        {
            results.Add(pc);
        }
        else
        {
            return;
        }

        if (Map.TryGetValue(pc, out var links))
        {
            foreach (var link in links)
            {
                Explore(link, [.. results]);
            }
        }
        else
        {
            if (results.Count > Results.Count)
            {
                Results = results;
            }
        }
    }

    public void Run()
    {
        var links = File.ReadAllLines(INPUT);

        foreach (var link in links)
        {
            var split = link.Split('-').OrderBy(s => s).ToList();
            var pc1 = split[0];
            var pc2 = split[1];

            if (!Map.TryGetValue(pc1, out var value))
            {
                value = [];
                Map[pc1] = value;
            }

            value.Add(pc2);
        }

        Map = Map
            .OrderBy(m => m.Key)
            .Select(m => new KeyValuePair<string, List<string>>(m.Key, m.Value.OrderBy(s => s).ToList()))
            .ToDictionary();


        foreach (var link in Map.Select(m => m.Key))
        {
            Explore(link, []);
        }

        Console.WriteLine(string.Join(",", Results));
    }
}
