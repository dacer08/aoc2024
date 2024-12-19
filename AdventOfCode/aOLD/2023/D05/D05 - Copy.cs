//using System.Collections.Concurrent;
//using System.Data;

//namespace AdventOfCode.Y2023.D05;

//public class D05
//{
//    public Dictionary<int, Dictionary<long, long>> Map = [];

//    public long Explore_P1(int index, long number)
//    {
//        //if (Map.ContainsKey(index) && Map[index].ContainsKey(number))
//        //{
//        //    return Map[index][number];
//        //}

//        var rules = DataD05.Categories[index];

//        var result = number;
//        foreach (var rule in rules)
//        {
//            if (number >= rule.Source && number <= rule.Source + rule.Length)
//            {
//                result = number + rule.Destination - rule.Source;
//                break;
//            }
//        }

//        if (index < DataD05.Categories.Count - 1)
//        {
//            result = Explore_P1(index + 1, result);
//        }

//        //if (!Map.ContainsKey(index))
//        //{
//        //    Map[index] = new Dictionary<long, long>();
//        //}

//        //Map[index][number] = result;

//        return result;
//    }

//    public void Run_P1()
//    {
//        var location = long.MaxValue;
//        foreach (var seed in DataD05.Seeds)
//        {
//            var res = Explore_P1(0, seed);
//            if (location > res)
//            {
//                location = res;
//            }
//        }
//        Console.WriteLine(location);
//    }

//    public Rule? Explore(int index, long start, long range, List<Rule> rules)
//    {
//        var category = DataD05.Categories[index];

//        var end = start + range;
//        var current = new List<Rule>();
//        foreach (var rule in category)
//        {
//            if ((start >= rule.Source && start <= rule.End)
//                || (end >= rule.Source && end <= rule.End)
//                || (rule.Source >= start && rule.Source <= end)
//                || (rule.End >= start && rule.End <= end))
//            {
//                if (index < DataD05.Categories.Count - 1)
//                {
//                    var result = Explore(index + 1, rule.Destination, rule.Length, rules);
//                    if (result != null)
//                    {
//                        current.Add(rule);
//                    }
//                }
//                else
//                {
//                    current.Add(rule);
//                }
//            }
//        }

//        if (current.Count > 0)
//        {
//            var result = current.OrderBy(c => c.Destination).First();
//            rules.Add(result);
//            return result;
//        }

//        if (index < DataD05.Categories.Count - 1)
//        {
//            var result = Explore(index + 1, start, range, rules);
//            if (result != null)
//            {
//                rules.Add(result.Value);
//                return result;
//            }
//        }

//        return null;
//    }

//    public void Run()
//    {
//        var location = long.MaxValue;
//        var list = new ConcurrentBag<long>();

//        for (var i = 0; i < DataD05.Seeds.Count; i++)
//        //Parallel.For(0, DataD05.Seeds.Count, (i) =>
//        {
//            //if (i % 2 == 1)
//            //{
//            //    return;
//            //}
//            var seed = DataD05.Seeds[i];
//            var range = DataD05.Seeds[i + 1];

//            //for (var j = 0; j < range; j++)
//            //{
//            var rules = new List<Rule>();
//            var res = Explore(0, seed, range, rules);
//            i++;
//            //if (location > res)
//            //{
//            //    location = res;
//            //}
//            //seed++;
//            //}
//        }
//        Console.WriteLine(location);
//    }
//}
