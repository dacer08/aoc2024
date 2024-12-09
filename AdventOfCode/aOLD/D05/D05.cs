namespace AdventOfCode.aOLD.D05;

public class D05
{
    public List<int> Print = [];

    public void Run()
    {
        for (var i = 0; i < D05Data.Map.Count; i++)
        {
            D05Data.Map[i].Sort(new D05Comparer());


            //var line = Data.Map[i];
            //var safe = true;
            //for (var x = 0; x < line.Count && safe; x++)
            //{
            //    var p1 = line[x];
            //    for (var y = x; y < line.Count && safe; y++)
            //    {
            //        var p2 = line[y];
            //        //if (PageOrders.Any(p => p.P1 == p1 && p.P2 == p2))
            //        //{
            //        //    continue;
            //        //} else 
            //        if (Data.PageOrders.Any(p => p.P1 == p2 && p.P2 == p1))
            //        {
            //            safe = false;
            //        }
            //    }
            //}

            //if (safe)
            //{
            //    Print.Add(line[line.Count / 2]);
            //}
            Console.WriteLine(string.Join(',', D05Data.Map[i]));
        }

    }
}
