namespace AdventOfCode.aOLD.D11;

public class D11
{
    public static int NbDigits(long n)
    {
        if (n < 10L) return 1;
        if (n < 100L) return 2;
        if (n < 1000L) return 3;
        if (n < 10000L) return 4;
        if (n < 100000L) return 5;
        if (n < 1000000L) return 6;
        if (n < 10000000L) return 7;
        if (n < 100000000L) return 8;
        if (n < 1000000000L) return 9;
        if (n < 10000000000L) return 10;
        if (n < 100000000000L) return 11;
        if (n < 1000000000000L) return 12;
        if (n < 10000000000000L) return 13;
        if (n < 100000000000000L) return 14;
        if (n < 1000000000000000L) return 15;
        if (n < 10000000000000000L) return 16;
        if (n < 100000000000000000L) return 17;
        if (n < 1000000000000000000L) return 18;
        return 19;
    }

    public Dictionary<long, Dictionary<long, long>> Dic = new Dictionary<long, Dictionary<long, long>>();

    public int MAX = 75;

    public long Explore(long number, long count)
    {
        if (count == 0) return 1;

        if (Dic.ContainsKey(number) && Dic[number].ContainsKey(count))
        {
            return Dic[number][count];
        }

        long res = 0;
        long newNumber = 0;
        if (number == 0)
        {
            newNumber = 1;
        }
        else
        {
            var nbDigit = NbDigits(number);
            if (nbDigit % 2 == 0)
            {
                long x = (long)Math.Pow(10, nbDigit / 2);
                newNumber = number / x;
                long right = number % x;

                res += Explore(right, count - 1);
            }
            else
            {
                newNumber = number * 2024;
            }
        }

        res = Explore(newNumber, count - 1) + res;

        if (!Dic.ContainsKey(number))
        {
            Dic[number] = new Dictionary<long, long>();
        }

        if (!Dic[number].ContainsKey(count))
        {
            Dic[number][count] = res;
        }

        return res;
    }

    public void Run()
    {
        var list = DataD11.Map.ToList();
        long res = 0;

        for (var i = 0; i < list.Count; i++)
        {
            res += Explore(list[i], MAX);
        }

        Console.WriteLine(res);

        /*var max = 75;
        var list = DataD11.Map.ToList();
        var result = new ConcurrentBag<long>();
        int pos = 0;

        var dic = new Dictionary<long, long>();
        long count = 0;


        var len = list.Count;
        while (len > 0)
        {
            var newList = new List<long>();

            for (var i = 0; i < list.Count; i++)
            {
                if (dic.ContainsKey(list[i]))
                {
                    count += dic[list[i]];
                }
            }

            len = newList.Count;
            list = newList;
        }

        //Parallel.ForEach(r, item =>
        foreach (var item in map)
        {
            if ( )
            {

            }
            var p = pos++;
            var list = new LinkedListLong();
            list.AddFirst(item);
            var count = max;
            while (count > 0)
            {
                var node = list.First;
                while (node != null)
                {
                    var number = node.Value;

                    if (number == 0)
                    {
                        node.Value = 1;
                    }
                    else
                    {
                        var nbDigit = NbDigits(number);
                        if (nbDigit % 2 == 0)
                        {
                            long x = (long)Math.Pow(10, (nbDigit / 2));
                            long left = number / x;
                            long right = number % x;
                            node.Value = left;
                            node.AddNext(right);
                            node = node.Next;

                        }
                        else
                        {
                            node.Value = number * 2024;
                        }
                    }

                    if (node != null)
                    {
                        node = node.Next;
                    }
                }

                DataD11.DisplayCount(count, p);
                count--;
            }

            for (var node = list.First; node != null; node = node.Next)
            {
                //Console.Write(node.Value + " ");
                result.Add(node.Value);
            }

            //DataD11.DisplayCount(count, p);
            //Console.WriteLine("added" + result.Count);
        });
        //}
        /*
        var list = new LinkedList<long>();

        foreach (var item in map)
        {
            list.AddLast(item);
        }

        var count = max;
       
        while (count > 0)
        {
            var node = list.First;
            while (node != null)
            {
                var number = node.Value;

                if (number == 0)
                {
                    node.Value = 1;
                }
                else
                {
                    var nbDigit = NbDigits(number);
                    if// (nbDigit % 2 == 0)
                    {
                        long x = (long)Math.Pow(10, (nbDigit / 2));
                        long left = number / x;
                        long right = number % x;
                        node.Value = left;
                        list.AddAfter(node, right);
                        node = node.Next;

                    }
                    else
                    {
                        node.Value = number * 2024;
                    }
                }

                if (node != null)
                {
                    node = node.Next;
                }
            }

            //Console.Write(count + ": ");
            //for (node = list.First; node != null; node = node.Next)
            //{
            //    Console.Write(node.Value + " ");
            //}
            //Console.WriteLine();
            //DataD11.DisplayCount(count, p);
            count--;
            DataD11.DisplayCount(count, 0);
        }

       

        //DataD11.DisplayCount(count, p);
        //Console.WriteLine("added" + result.Count);
        //});


        //while (count > 0) {
        //    for (var i = 0; i < map.Count; i++)
        //    {
        //        var number = map[i];

        //        if (number == "0")
        //        {
        //            map[i] = "1";
        //        }
        //        else if (number.Length % 2 == 0)
        //        {
        //            var left = number.Substring(0, number.Length / 2);
        //            var right = long.Parse(number.Substring(number.Length / 2)).ToString();

        //            map[i] = left;
        //            i++;
        //            map.Insert(i, right);
        //        }
        //        else
        //        {
        //            map[i] = (long.Parse(number) * 2024).ToString();
        //        }


        //    }

        //    //DataD11.Display(map);
        //    Console.WriteLine(count);
        //    count--;
        //}

        //var count = 0;
        //for (var node = list.First; node != null; node = node.Next)
        //{
        //    count++;
        //}

        Console.WriteLine();
        Console.WriteLine("-----------");
        //Console.WriteLine(count);
        Console.WriteLine(result.Count);
        Console.WriteLine("-----------");
        */
    }
}