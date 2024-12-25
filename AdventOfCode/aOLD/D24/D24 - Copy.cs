//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Security.Cryptography;
//using System.Text;

//namespace AdventOfCode.Current.D24;

//public class D24
//{
//    private readonly Stopwatch Watch;

//    public D24(Stopwatch watch)
//    {
//        Watch = watch;
//    }

//    private const string INPUT = "Current/D24/InputTest.txt";

//    public Dictionary<string, bool> Destinations = [];

//    public List<OperationsD24> Operations = [];

//    public void Run_P1()
//    {
//        var file = File.ReadAllLines(INPUT);

//        var i = 0;
//        while (i < file.Length && file[i] != "")
//        {
//            var split = file[i].Split(": ");
//            Destinations[split[0]] = split[1] == "1";
//            i++;
//        }

//        i++;

//        while (i < file.Length)
//        {
//            var split = file[i].Split(' ');
//            Operations.Add(new(split[0], split[2], split[1], split[4]));
//            i++;
//        }

//        //var remaining = 0;
//        while (Operations.Count > 0)
//        {
//            var destinations = Destinations.Select(d => d.Key).ToList();
//            foreach (var destination in destinations)
//            {
//                var todo = Operations
//                    .Where(o => destination == o.Left || destination == o.Right)
//                    .ToList();

//                Operations = Operations
//                    .Except(todo)
//                    .ToList();

//                foreach (var operation in todo)
//                {
//                    if (Destinations.TryGetValue(operation.Left, out var left) && Destinations.TryGetValue(operation.Right, out var right))
//                    {
//                        var result = operation.Operand switch
//                        {
//                            "AND" => left & right,
//                            "OR" => left | right,
//                            "XOR" => left ^ right,
//                            _ => throw new Exception("lol"),
//                        };

//                        Destinations[operation.Destination] = result;
//                    }
//                    else
//                    {
//                        Operations.Add(operation);
//                    }
//                }
//            }
//        }
//        var list = Destinations
//            .Where(d => d.Key.StartsWith("z"))
//            .OrderByDescending(d => d.Key)
//            .Select(d => d.Value)
//            .ToList();

//        StringBuilder sb = new();

//        list.ForEach(b => sb.Append(b ? '1' : '0'));
//        Console.WriteLine(Convert.ToInt64(sb.ToString(), 2));
//    }

//    public long ConvertToNumber(char start)
//    {
//        var list = Destinations
//            .Where(d => d.Key[0] == start)
//            .OrderByDescending(d => d.Key)
//            .Select(d => d.Value)
//            .ToList();

//        var stringBuilder = new StringBuilder();

//        list.ForEach(b => stringBuilder.Append(b ? '1' : '0'));
//        return Convert.ToInt64(stringBuilder.ToString(), 2);
//    }

//    public void GetData()
//    {
//        var file = File.ReadAllLines(INPUT);

//        var i = 0;
//        while (i < file.Length && file[i] != "")
//        {
//            var split = file[i].Split(": ");
//            Destinations[split[0]] = split[1] == "1";
//            i++;
//        }

//        i++;

//        while (i < file.Length)
//        {
//            var split = file[i].Split(' ');
//            var left = split[0];
//            var right = split[1];
//            if (string.Compare(left, right) > 0)
//            {
//                left = split[1];
//                right = split[0];
//            }
//            Operations.Add(new(left, right, split[1], split[4]));
//            i++;
//        }
//    }
//    public void Run()
//    {
//        GetData();

//        var x = ConvertToNumber('x');
//        var y = ConvertToNumber('y');
//        var expected = x & y;
//        Console.WriteLine($"Expected: {expected}");
//        //bvw,cng,csw,fhk,gkc,gww,hbh,hbw,htn,hvc,jhg,jqf,jvf,kbb,mdd,nnn,prp,rck,rjs,rvm,rvn,sbt,skh,tvf,vqf,wkb,wmq,wpd,wpd,wpp,wts,wwp,z01,z11,z15,z19,z37
//        var result = new List<string>();
//        foreach (var operation in Operations)
//        {
//            if (!operation.Destination.StartsWith('z') && operation.Operand == "XOR" && !operation.Left.StartsWith('x'))
//            {
//                result.Add(operation.Destination);
//            }

//            if (operation.Operand == "AND" && operation.Left != "x00")
//            {
//                var destinations = Operations.Where(o => o.Left == operation.Destination || o.Right == operation.Destination).ToList();
//                foreach (var destination in destinations)
//                {
//                    if (destination.Operand != "OR")
//                    {
//                        result.Add(destination.Destination);
//                        break;
//                    }
//                }
//            }

//            if (operation.Operand == "OR")
//            {
//                var destination = Operations.FirstOrDefault(t => t.Destination == operation.Left);
//                if (destination.Operand != null && destination.Operand != "AND")
//                {
//                    result.Add(destination.Destination);
//                }

//                destination = Operations.FirstOrDefault(t => t.Destination == operation.Right);
//                if (destination.Operand != null && destination.Operand != "AND")
//                {
//                    result.Add(destination.Destination);
//                }
//            }

//            if (operation.Destination.StartsWith('z') && operation.Operand != "XOR" && operation.Destination != "z45")
//            {
//                result.Add(operation.Destination);
//            }
//        }

//        Console.WriteLine(string.Join(",", result.OrderBy(r => r)));
//    }
//}
