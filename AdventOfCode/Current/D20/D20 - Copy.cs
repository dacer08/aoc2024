//using System.Diagnostics;
//using System.IO;

//namespace AdventOfCode.Current.D20;

//public class D20
//{
//    private const string INPUT = "Current/D20/InputTest.txt";
//    //private const string INPUT = "D20/Input.txt";

//    public List<List<char>> Map = [];
//    public Vector2D Start = new();
//    public Vector2D End = new();

//    public Dictionary<Vector2D, int> Scores = [];

//    //public Dictionary<Vector2D, Dictionary<Vector2D, int>> ScoresNoWalls = [];
//    public Dictionary<Vector2D, int> ScoresNoWalls = [];

//    public List<int> Steps = [];

//    private bool CheckScore(Vector2D position, int score)
//    {
//        if (Scores.TryGetValue(position, out var currentScore) && currentScore < score)
//            return false;
//        Scores[position] = score;
//        return true;
//    }

//    public void Run_P1()
//    {
//        var watch = new Stopwatch();
//        watch.Start();
//        Map = File.ReadAllLines(INPUT)
//            .Select(l => l.ToCharArray().ToList())
//            .ToList();

//        var defaultWalls = new List<Vector2D>();
//        for (var y = 0; y < Map.Count; y++)
//        {
//            var line = Map[y];
//            for (var x = 0; x < line.Count; x++)
//            {
//                if (Map[y][x] == 'S')
//                {
//                    Start = new Vector2D(x, y);
//                }
//                if (Map[y][x] == 'E')
//                {
//                    End = new Vector2D(x, y);
//                }
//                if (Map[y][x] == '#')
//                {
//                    defaultWalls.Add(new Vector2D(x, y));
//                }
//            }
//        }

//        //var list = new HashSet<CoordinateComparableComparable>();
//        //list.Add(new CoordinateComparableComparable(0, 0));
//        //list.Add(new CoordinateComparableComparable(0, 1));
//        //list.Add(new CoordinateComparableComparable(1, 0));
//        //list.Add(new CoordinateComparableComparable(1, 0));

//        //foreach (var x in Combinations.GetKCombs(list, 2))
//        //{
//        //    Console.Write(x.First());
//        //    Console.Write(x.Last());
//        //    Console.WriteLine();
//        //}

//        var walls = defaultWalls.ToHashSet();

//        var queue1 = new Queue<QueueItemD20>();
//        queue1.Enqueue(new QueueItemD20(Start, Vector2D.TOP, 0));

//        while (queue1.TryDequeue(out var current))
//        {
//            var score = current.Score;
//            var v = current.Vector;
//            var v90 = Vector2D.Rotate90d(v);
//            var vMinus90 = Vector2D.RotateMinus90d(v);
//            var position = current.Position;

//            while (position != End && !walls.Contains(position))
//            {
//                score++;

//                var coordinate = Vector2D.Add(position, v90);
//                if (CheckScore(coordinate, score))
//                {
//                    queue1.Enqueue(new QueueItemD20(coordinate, v90, score));
//                }

//                coordinate = Vector2D.Add(position, vMinus90);
//                if (CheckScore(coordinate, score))
//                {
//                    queue1.Enqueue(new QueueItemD20(coordinate, vMinus90, score));
//                }


//                position = Vector2D.Add(position, v);
//            }

//            CheckScore(position, score);
//        }

//        Scores.TryGetValue(End, out var endScoreDefault);
//        Console.WriteLine(endScoreDefault);

//        var maxX = Map[0].Count - 1;
//        var maxY = Map.Count - 1;
//        var max = endScoreDefault - 100;
//        //foreach (var noWalls in defaultWalls)
//        for (var i = 0; i < defaultWalls.Count; i++)
//        {
//            var noWalls = defaultWalls[i];
//            //Vector2D w1 = new Vector2D(1, 5);// noWalls.First();
//            //Vector2D w2 = new Vector2D(2, 5);

//            //if (w1.X == 0 ||  w1.Y == 0 || w1.X == maxX || w1.Y == maxY
//            //    || w2.X == 0 || w2.Y == 0 || w2.X == maxX || w2.Y == maxY)
//            //{
//            //    continue;
//            //}

//            //if (Math.Abs(w1.X - w2.X) != 1 && Math.Abs(w1.Y - w2.Y) != 1)
//            //{
//            //    continue;
//            //}

//            if (noWalls.X == 0 || noWalls.Y == 0 || noWalls.X == maxX || noWalls.Y == maxY)
//            {
//                continue;
//            }

//            var near = new List<Vector2D>();

//            near.Add(noWalls.Add(Vector2D.TOP));
//            near.Add(noWalls.Add(Vector2D.LEFT));
//            near.Add(noWalls.Add(Vector2D.RIGHT));
//            near.Add(noWalls.Add(Vector2D.BOTTOM));

//            if (near.Where(w => Map[w.Y][w.X] == '#').Count() > 2)
//            {
//                continue;
//            }

//            Scores = [];
//            walls = defaultWalls.Where((w, i2) => i != i2).ToHashSet();

//            var queue = new Queue<QueueItemD20>();
//            queue.Enqueue(new QueueItemD20(Start, Vector2D.TOP, 0));

//            while (queue.TryDequeue(out var current))
//            {
//                var score = current.Score;
//                if (score > max)
//                {
//                    continue;
//                }
//                var v = current.Vector;
//                var v90 = Vector2D.Rotate90d(v);
//                var vMinus90 = Vector2D.RotateMinus90d(v);
//                var position = current.Position;

//                while (position != End && !walls.Contains(position))
//                {
//                    score++;

//                    var coordinate = Vector2D.Add(position, v90);
//                    if (!walls.Contains(coordinate) && CheckScore(coordinate, score))
//                    {
//                        queue.Enqueue(new QueueItemD20(coordinate, v90, score));
//                    }

//                    coordinate = Vector2D.Add(position, vMinus90);
//                    if (!walls.Contains(coordinate) && CheckScore(coordinate, score))
//                    {
//                        queue.Enqueue(new QueueItemD20(coordinate, vMinus90, score));
//                    }


//                    position = Vector2D.Add(position, v);
//                }

//                CheckScore(position, score);
//            }

//            if (Scores.TryGetValue(End, out var endScore))
//            {
//                var atLeast = endScoreDefault - endScore;
//                if (atLeast >= 100)
//                    ScoresNoWalls[noWalls] = endScore;
//                //if (!ScoresNoWalls.TryGetValue(w1, out Dictionary<Vector2D, int>? value))
//                //{
//                //    value = [];
//                //    ScoresNoWalls[w1] = value;
//                //}

//                //value[w2] = endScore;
//            }
//        }

//        Console.WriteLine(ScoresNoWalls.Count);
//        Console.WriteLine(watch.ElapsedMilliseconds / 1000);

//    }


//    public void Run()
//    {
//        Map = File.ReadAllLines(INPUT)
//             .Select(l => l.ToCharArray().ToList())
//             .ToList();

//        var defaultWalls = new List<Vector2D>();
//        for (var y = 0; y < Map.Count; y++)
//        {
//            var line = Map[y];
//            for (var x = 0; x < line.Count; x++)
//            {
//                if (Map[y][x] == 'S')
//                {
//                    Start = new Vector2D(x, y);
//                }
//                if (Map[y][x] == 'E')
//                {
//                    End = new Vector2D(x, y);
//                }
//                if (Map[y][x] == '#')
//                {
//                    defaultWalls.Add(new Vector2D(x, y));
//                }
//            }
//        }

//        var walls = defaultWalls.ToHashSet();

//        var queue = new Queue<QueueItemD20>();
//        queue.Enqueue(new QueueItemD20(Start, Vector2D.TOP, 0));

//        while (queue.TryDequeue(out var current))
//        {
//            var score = current.Score;
//            var v = current.Vector;
//            var v90 = Vector2D.Rotate90d(v);
//            var vMinus90 = Vector2D.RotateMinus90d(v);
//            var position = current.Position;

//            while (position != End && !walls.Contains(position))
//            {
//                score++;

//                var coordinate = Vector2D.Add(position, v90);
//                if (CheckScore(coordinate, score))
//                {
//                    queue.Enqueue(new QueueItemD20(coordinate, v90, score));
//                }

//                coordinate = Vector2D.Add(position, vMinus90);
//                if (CheckScore(coordinate, score))
//                {
//                    queue.Enqueue(new QueueItemD20(coordinate, vMinus90, score));
//                }


//                position = Vector2D.Add(position, v);
//            }

//            CheckScore(position, score);
//        }

//        Scores.TryGetValue(End, out var endScoreDefault);
//        Console.WriteLine(endScoreDefault);
//    }
//}
