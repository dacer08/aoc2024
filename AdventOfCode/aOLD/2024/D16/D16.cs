using System.Drawing;

namespace AdventOfCode.aOLD.D16;

public class D16
{
    public List<List<char>> Map = DataD16.Map;

    public Dictionary<Coordinate, int> Scores_P1 = [];

    public Dictionary<Coordinate, ScoreD16> Scores = [];

    private bool CheckScore_P1(Coordinate position, int penalty)
    {
        if (Scores_P1.TryGetValue(position, out var currentScore) && currentScore <= penalty)
            return false;
        Scores_P1[position] = penalty;
        return true;
    }

    public void Run_P1()
    {
        DataD16.Read();

        //DataD16.Display(DataD16.Map);

        var walls = new HashSet<Coordinate>();
        for (var y = 0; y < Map.Count; y++)
        {
            for (var x = 0; x < Map[0].Count; x++)
            {
                if (Map[y][x] == '#')
                {
                    walls.Add(new Coordinate(x, y));
                }
            }
        }

        var queue = new Queue<QueueItemD16>();
        queue.Enqueue(new QueueItemD16(DataD16.Reindeer, Vectors.RIGHT, 0));

        while (queue.TryDequeue(out var current))
        {
            var score = current.Score;
            var penalty = score + 1001;
            score++;
            var v = current.Vector;
            var v90 = Vectors.Rotate90d(v);
            var vMinus90 = Vectors.RotateMinus90d(v);
            var position = current.Position;

            while (position != DataD16.End && !walls.Contains(position))
            {
                var coordinate = Vectors.Add(position, v90);
                if (CheckScore_P1(coordinate, penalty))
                {
                    queue.Enqueue(new QueueItemD16(coordinate, v90, penalty));
                }

                coordinate = Vectors.Add(position, vMinus90);
                if (CheckScore_P1(coordinate, penalty))
                {
                    queue.Enqueue(new QueueItemD16(coordinate, vMinus90, penalty));
                }

                penalty++;
                position = Vectors.Add(position, v);
            }

            CheckScore_P1(position, score);
        }



        Console.WriteLine(Scores_P1[DataD16.End]);
    }

    public void Run()
    {
        DataD16.Read();

        //DataD16.Display(DataD16.Map);

        var walls = new HashSet<Coordinate>();
        for (var y = 0; y < Map.Count; y++)
        {
            for (var x = 0; x < Map[0].Count; x++)
            {
                if (Map[y][x] == '#')
                {
                    walls.Add(new Coordinate(x, y));
                }
            }
        }

        var queue = new Queue<QueueItemD16>();
        queue.Enqueue(new QueueItemD16(DataD16.Reindeer, Vectors.RIGHT, 0));

        while (queue.TryDequeue(out var current))
        {
            var score = current.Score;
            var penalty = score + 1000;

            var v = current.Vector;
            var v90 = Vectors.Rotate90d(v);
            var vMinus90 = Vectors.RotateMinus90d(v);
            var position = current.Position;

            var path = new HashSet<Coordinate>();

            while (position != DataD16.End && !walls.Contains(position))
            {
                if (CheckScore(position, penalty, path))
                {
                    queue.Enqueue(new QueueItemD16(position, v90, penalty));
                }

                if (CheckScore(position, penalty, path))
                {
                    queue.Enqueue(new QueueItemD16(position, vMinus90, penalty));
                }

                penalty++;
                score++;

                path.Add(position);
                position = Vectors.Add(position, v);
            }

            CheckScore(position, score, path);
        }

        var countQueue = new Queue<Coordinate>();
        countQueue.Enqueue(DataD16.End);
        var res = new HashSet<Coordinate>();
        while (countQueue.TryDequeue(out var current))
        {
            if (Scores.TryGetValue(current, out var score))
            {
                foreach (var coordinate in score.Path)
                {
                    if (res.Add(coordinate))
                    {
                        countQueue.Enqueue(coordinate);
                    }
                }
            }
        }

        Console.WriteLine(res.Count + 1);
    }

    private bool CheckScore(Coordinate position, int penalty, HashSet<Coordinate> path)
    {

        if (Scores.TryGetValue(position, out var currentScore) && currentScore.Score <= penalty)
        {
            if (currentScore.Score < penalty)
            {
                return false;
            }
            for (var i = 0; i < path.Count; i++)
                currentScore.Path.Add(path.ElementAt(i));
            return true;
        }
        Scores[position] = new ScoreD16(penalty, new HashSet<Coordinate>(path));
        return true;
    }
}
