using System.Diagnostics;
using System.Net.Sockets;

namespace AdventOfCode;

public class D22
{
    private readonly Stopwatch Watch;

    public D22(Stopwatch watch)
    {
        Watch = watch;
    }

    private const string INPUT = "D22/InputTest.txt";
    //private const string INPUT = "D21/Input.txt";

    private Dictionary<int, Dictionary<int, ValueD22>> Monkeys = [];

    private Dictionary<KeyD22, Dictionary<int, long>> Trades = [];

    public static long GetSecret(long starter)
    {
        var res = starter;
        res = (res ^ (res * 64)) % 16777216;
        res = (res ^ (res / 32)) % 16777216;
        return (res ^ (res * 2048)) % 16777216;
    }

    public void Run()
    {
        var starters = File.ReadAllLines(INPUT);
        long res = 0;
        for (int monkey = 0; monkey < starters.Length; monkey++)
        {
            var dic = new Dictionary<int, ValueD22>();

            var starter = long.Parse(starters[monkey]);

            var value = new ValueD22();
            value.Number = starter % 10;
            value.Difference = 0;
            dic[0] = value;

            var key = new KeyD22(-10, -10, -10, value.Difference);
            
            for (var i = 1; i < 2001; i++)
            {
                starter = GetSecret(starter);
                var newValue = new ValueD22();
                newValue.Number = starter % 10;
                newValue.Difference = newValue.Number - value.Number;
                dic[i] = newValue;
                value = newValue;
                key = new(key.N2, key.N3, key.N4, newValue.Difference);
                if (i > 3)
                {
                    if (!Trades.TryGetValue(key, out Dictionary<int, long>? trade))
                    {
                        trade = [];
                        Trades[key] = trade;
                    }

                    if (!trade.ContainsKey(monkey))
                    {
                        trade[monkey] = newValue.Number;
                    }
                }
            }

            Monkeys[monkey] = dic;

        }



        Console.WriteLine(Trades.Select(t => t.Value.Sum(m => m.Value)).OrderByDescending(v => v).First());
    }
}
