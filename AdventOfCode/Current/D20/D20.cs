using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Runtime.Intrinsics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AdventOfCode.Current.D20;

public class D20
{
    private const string INPUT = "Current/D20/InputTest.txt";
    //private const string INPUT = "D20/Input.txt";

    public List<List<char>> Map = [];
    public Vector2D DefaultStart = new();
    public Vector2D DefaultEnd = new();
    public HashSet<Vector2D> DefaultWalls = [];
    public ConcurrentDictionary<Vector2D, int> ScoresFromStart = [];
    public ConcurrentDictionary<Vector2D, int> ScoresFromEnd = [];
    public int DefaultScore = 0;

    private bool CheckScore(Vector2D position, int score, Dictionary<Vector2D, int> scores)
    {
        if (scores.TryGetValue(position, out var currentScore) && currentScore < score)
            return false;
        scores[position] = score;
        return true;
    }

    public void Run()
    {
        Map = File.ReadAllLines(INPUT)
             .Select(l => l.ToCharArray().ToList())
             .ToList();

        //DefaultWalls = new List<Vector2D>();
        for (var y = 0; y < Map.Count; y++)
        {
            var line = Map[y];
            for (var x = 0; x < line.Count; x++)
            {
                Dones[new(x, y)] = [];
                if (Map[y][x] == 'S')
                {
                    DefaultStart = new Vector2D(x, y);
                    Map[y][x] = '.';
                }
                if (Map[y][x] == 'E')
                {
                    DefaultEnd = new Vector2D(x, y);
                    Map[y][x] = '.';
                }
                if (Map[y][x] == '#')
                {
                    DefaultWalls.Add(new Vector2D(x, y));
                }
            }
        }
        DefaultScore = ComputePath(DefaultStart, DefaultEnd);
        var countX = Map[0].Count;
        //for (var y = 0; y < Map.Count; y++)
        //{
        //    Parallel.For(0, countX, (x) =>
        //    //for (var x = 1; x < countX - 1; x++)
        //    {
        //        if (Map[y][x] == '.')
        //        {
        //            var vector = new Vector2D(x, y);
        //            ScoresFromStart[vector] = ComputePath(DefaultStart, vector);
        //            ScoresFromEnd[vector] = ComputePath(vector, DefaultEnd);
        //        }
        //    });
        //    Console.SetCursorPosition(0, 0);
        //    Console.WriteLine($"Init: Line {y + 1} / {Map.Count - 1}");
        //}

        //var sbScoresFromStart = new StringBuilder();
        //var sbScoresFromEnd = new StringBuilder();

        //foreach (var score in ScoresFromStart)
        //{
        //    sbScoresFromStart.AppendLine($"{score.Key.X} {score.Key.Y} {score.Value}");
        //}

        //foreach (var score in ScoresFromEnd)
        //{
        //    sbScoresFromEnd.AppendLine($"{score.Key.X} {score.Key.Y} {score.Value}");
        //}

        //File.WriteAllText("Current/D20/ScoresFromStart.txt", sbScoresFromStart.ToString());
        //File.WriteAllText("Current/D20/ScoresFromEnd.txt", sbScoresFromEnd.ToString());

        //Console.WriteLine("******************");
        //Console.WriteLine($"Default score: {defaultScore}");
        //var vector = new Vector2D(60, 75);
        //Console.WriteLine($"Test: {ScoresFromStart[vector]} + {ScoresFromEnd[vector]} = {ScoresFromStart[vector] + ScoresFromEnd[vector]}");
        //Console.WriteLine("******************");

        ScoresFromStart = new ConcurrentDictionary<Vector2D, int>(File.ReadAllLines("Current/D20/ScoresFromStart.txt")
            .Select(l => l.Split(" ").Select(int.Parse).ToList())
            .Select(s => new KeyValuePair<Vector2D, int>(new Vector2D(s[0], s[1]), s[2]))
            .ToDictionary());

        ScoresFromEnd = new ConcurrentDictionary<Vector2D, int>(File.ReadAllLines("Current/D20/ScoresFromStart.txt")
            .Select(l => l.Split(" ").Select(int.Parse).ToList())
            .Select(s => new KeyValuePair<Vector2D, int>(new Vector2D(s[0], s[1]), s[2]))
            .ToDictionary());

        Console.WriteLine($"Loaded {ScoresFromStart.Count} & {ScoresFromEnd.Count}");

        for (var y = 0; y < Map.Count; y++)
        {
            //Parallel.For(0, countX, (x) =>
            for (var x = 0; x < countX; x++)
            {
                if (Map[y][x] == '.')
                {
                    var vector = new Vector2D(x, y);
                    var range = new Dictionary<int, HashSet<Vector2D>>();
                    //for (var z = 0; z <= 20; z++)
                    //{
                    //    range[z] = [];
                    //}
                    Dones[vector] = [];
                    ComputeWalls(vector);
                }
            };
            Console.SetCursorPosition(0, 2);
            Console.WriteLine($"Walls: Line {y + 1} / {Map.Count}");
        }

        //Console.SetCursorPosition(0, 3);
        Console.WriteLine(ScoreWall);
        //Console.WriteLine("1005068");
    }

    public int ScoreWall = 0;
    public Dictionary<Vector2D, HashSet<Vector2D>> Dones = [];

    private void ComputeWalls(Vector2D start)
    {
        var queue = new Queue<QueueWallD20>();

        var dones = Dones[start];

        queue.Enqueue(new QueueWallD20(start, start, Vector2D.TOP,    0));
        queue.Enqueue(new QueueWallD20(start, start, Vector2D.LEFT,   0));
        queue.Enqueue(new QueueWallD20(start, start, Vector2D.RIGHT,  0));
        queue.Enqueue(new QueueWallD20(start, start, Vector2D.BOTTOM, 0));

        while (queue.TryDequeue(out var current))
        {
            //if (current.Pico > 19)
            //{
            //    continue;
            //}
            var v = current.Vector;
            var position = current.Position;

            if (Map[position.Y][position.X] == '.')
            {
                var result = ScoresFromStart[current.Start] + ScoresFromEnd[position] + current.Pico;
                if (result <= (DefaultScore - 100))
                {
                    ScoreWall++;
                }
            }

            position = Vector2D.Add(current.Position, v);


            if (position.X < 0 || position.Y < 0 || position.X >= Map[0].Count || position.Y >= Map.Count)
            {
                continue;
            }

            if (dones.Contains(position) || Dones[position].Contains(current.Start))
            {
                continue;
            }

            dones.Add(position);
            Dones[position].Add(current.Start);

            var v90 = Vector2D.Rotate90d(v);
            //var vMinus90 = Vector2D.RotateMinus90d(v);

            var pico = current.Pico + 1;

            if (pico < 20 && position != current.Start)
            {
                queue.Enqueue(new QueueWallD20(current.Start, position, v90, pico));
                //queue.Enqueue(new QueueWallD20(current.Start, position, vMinus90, pico));
                queue.Enqueue(new QueueWallD20(current.Start, position, v, pico));
            }

            
        }
    }

    private int ComputePath(Vector2D start, Vector2D end)
    {

        var scores = new Dictionary<Vector2D, int>();
        var walls = DefaultWalls;

        var queue = new Queue<QueueItemD20>();
        queue.Enqueue(new QueueItemD20(start, Vector2D.TOP, 0));
        queue.Enqueue(new QueueItemD20(start, Vector2D.LEFT, 0));
        queue.Enqueue(new QueueItemD20(start, Vector2D.RIGHT, 0));
        queue.Enqueue(new QueueItemD20(start, Vector2D.BOTTOM, 0));

        while (queue.TryDequeue(out var current))
        {
            var score = current.Score;
            var v = current.Vector;
            var v90 = Vector2D.Rotate90d(v);
            var vMinus90 = Vector2D.RotateMinus90d(v);
            var position = current.Position;

            while (position != end && !walls.Contains(position))
            {
                score++;

                var coordinate = Vector2D.Add(position, v90);
                if (CheckScore(coordinate, score, scores))
                {
                    queue.Enqueue(new QueueItemD20(coordinate, v90, score));
                }

                coordinate = Vector2D.Add(position, vMinus90);
                if (CheckScore(coordinate, score, scores))
                {
                    queue.Enqueue(new QueueItemD20(coordinate, vMinus90, score));
                }


                position = Vector2D.Add(position, v);
            }

            CheckScore(position, score, scores);
        }

        scores.TryGetValue(end, out var endScoreDefault);
        return endScoreDefault;
    }
}
