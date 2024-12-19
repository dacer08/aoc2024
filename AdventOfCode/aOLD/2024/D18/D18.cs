using Dijkstra.NET.Graph;
using Dijkstra.NET.ShortestPath;
using System.Collections.Concurrent;

//using DijkstraAlgorithm.Graphing;
using System.Drawing;

namespace AdventOfCode.aOLD.D18;

public class D18
{
    private const string INPUT = "D18/InputTest.txt";
    //private const string INPUT = "D18/Input.txt";

    public void Run_P1()
    {
        var bytes = File.ReadAllLines(INPUT)
            .Select(l => l.Split(','))
            .Select(s => new Coordinate(int.Parse(s[0]), int.Parse(s[1])))
            .Take(1024)
            .ToHashSet();

        //bytes = [];

        var SIZE = 71;
        char[,] map = new char[SIZE, SIZE];
        var graph = new Graph<int, string>();
        //var newGraph = new GraphBuilder();

        for (var y = 0; y < SIZE; y++)
        {
            for (var x = 0; x < SIZE; x++)
            {
                if (bytes.Contains(new Coordinate(x, y)))
                {
                    map[y, x] = '#';
                }
                else
                {
                    map[y, x] = '.';
                }
                Console.Write(map[y, x]);
            }
            Console.WriteLine();
        }
        Console.WriteLine();

        for (var y = 0; y < SIZE; y++)
        {
            for (var x = 0; x < SIZE; x++)
            {
                var corrupted = new Coordinate(x, y);

                var current = y * SIZE + x + 1;
                //newGraph.AddNode()
                graph.AddNode(current);

            }
        }

        for (var y = 0; y < SIZE; y++)
        {
            for (var x = 0; x < SIZE; x++)
            {
                var corrupted = new Coordinate(x, y);

                var current = (uint)(y * SIZE + x + 1);

                if (bytes.Contains(corrupted))
                {
                    continue;
                }

                if (y > 0 && !bytes.Contains(new Coordinate(x, y - 1)))
                {
                    map[y, x] = 'O';
                    map[y - 1, x] = 'O';
                    var next = (uint)((y - 1) * SIZE + x + 1);
                    graph.Connect(next, current, 1, "");
                    graph.Connect(current, next, 1, "");
                }

                if (x > 0 && !bytes.Contains(new Coordinate(x - 1, y)))
                {
                    map[y, x] = 'O';
                    map[y, x - 1] = 'O';
                    var next = (uint)(y * SIZE + (x - 1) + 1);
                    graph.Connect(next, current, 1, "");
                    graph.Connect(current, next, 1, "");
                }
            }
        }

        for (var y = 0; y < SIZE; y++)
        {
            for (var x = 0; x < SIZE; x++)
            {

                Console.Write(map[y, x]);
            }
            Console.WriteLine();
        }
        Console.WriteLine();

        ShortestPathResult result = graph.Dijkstra(1, (uint)(SIZE * SIZE)); //result contains the shortest path
        Console.WriteLine(result);
        var path = result.GetPath();
    }

    public void Run()
    {
        var bytesDefault = File.ReadAllLines(INPUT)
            .Select(l => l.Split(','))
            .Select(s => new Coordinate(int.Parse(s[0]), int.Parse(s[1])))
            .ToHashSet();

        var SIZE = 71;
        //for (var i = 1024; i < bytesDefault.Count; i++)
        var bag = new ConcurrentBag<int>();
        Parallel.For(1024, bytesDefault.Count, (i) =>
        {
            Console.WriteLine($"{i}");
            var bytes = bytesDefault.Take(i).ToList();
            var graph = new Graph<int, string>();

            for (var y = 0; y < SIZE; y++)
            {
                for (var x = 0; x < SIZE; x++)
                {
                    var corrupted = new Coordinate(x, y);

                    var current = (uint)(y * SIZE + x + 1);
                    //newGraph.AddNode()
                    graph.AddNode((int)current);

                    if (bytes.Contains(corrupted))
                    {
                        continue;
                    }

                    if (y > 0 && !bytes.Contains(new Coordinate(x, y - 1)))
                    {
                        var next = (uint)((y - 1) * SIZE + x + 1);
                        graph.Connect(next, current, 1, "");
                        graph.Connect(current, next, 1, "");
                    }

                    if (x > 0 && !bytes.Contains(new Coordinate(x - 1, y)))
                    {
                        var next = (uint)(y * SIZE + (x - 1) + 1);
                        graph.Connect(next, current, 1, "");
                        graph.Connect(current, next, 1, "");
                    }
                }
            }

            ShortestPathResult result = graph.Dijkstra(1, (uint)(SIZE * SIZE)); //result contains the shortest path
            if (!result.IsFounded)
            {
                //Console.WriteLine($"Is found!! {i}");
                bag.Add(i);
            }
        });

        Console.WriteLine(bag.OrderBy(i => i).First());
    }
}
