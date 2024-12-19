using System;
using System.Collections.Concurrent;

namespace AdventOfCode.Y2023.D05;

public class D05
{
    public Dictionary<int, Dictionary<long, long>> Map = [];

    public long Explore(int index, long number, Rule[] rules)
    {
        //if (Map.ContainsKey(index) && Map[index].ContainsKey(number))
        //{
        //    return Map[index][number];
        //}

        var result = number;
        //var found = true;
        //Parallel.For(0, rules.Count, (i) =>
        for (var i = 0; i < rules.Length; i++)
        {
            var rule = rules[i];
            var end = rule.Source + rule.Length;
            if (number >= rule.Source && number <= end)
            {
                result = number + rule.Destination - rule.Source;
                //found = true;
                break;
            }
        };

        //if (index == 6 && found && rules.Count > 0)
        //{
        //    DataD05.Categories[index] = DataD05.Categories[index]
        //        .Where(c => c.Destination <= rule.Destination)
        //        .ToList();
        //}
        //else {
        //    var i = index;
        //    var canDelete = true;
        //    while (i < 7 && canDelete)
        //    {
        //        canDelete = DataD05.Categories[i].Count == 1;
        //        i++;
        //    }
        //    if (canDelete)
        //    {
        //        DataD05.Categories[index] = DataD05.Categories[index]
        //        .Where(c => c.Destination < rule.Destination)
        //        .ToList();
        //    }
        //}

        if (index < DataD05.Categories.Count - 1)
        {
            result = Explore(index + 1, result, DataD05.Categories[index + 1]);
        }

        //if (!Map.ContainsKey(index))
        //{
        //    Map[index] = new Dictionary<long, long>();
        //}

        //Map[index][number] = result;

        return result;
    }

    //public void Run_P1()
    //{
    //    var location = long.MaxValue;
    //    foreach (var seed in DataD05.Seeds)
    //    {
    //        var res = Explore(0, seed);
    //        if (location > res)
    //        {
    //            location = res;
    //        }
    //    }
    //    Console.WriteLine(location);
    //}

    public void Run()
    {
        var location = long.MaxValue;

        for (var i = 0; i < DataD05.Seeds.Count; i++)
        //Parallel.For(0, DataD05.Seeds.Count, (i) =>
        {
            if (i % 2 == 1)
            {
                continue;
                //return;
            }
            Console.WriteLine(i);
            var seed = DataD05.Seeds[i];
            var range = DataD05.Seeds[i + 1];
            //var range = 1;

            for (var j = 0; j < range; j++)
            {
                var res = Explore(0, seed, DataD05.Categories[0]);
                if (location > res)
                {
                    location = res;
                }
                seed++;
            }
            Console.WriteLine("done: " + i);
        };
        Console.WriteLine(location);
    }
}
