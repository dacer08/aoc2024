using System.Numerics;

namespace AdventOfCode.aOLD.D14;

public class D14
{
    public int Count(List<D14Robot> robots, Coordinate start, Coordinate end)
    {
        var count = 0;

        foreach (var robot in robots)
        {
            if (robot.X >= start.X && robot.X < end.X
                && robot.Y >= start.Y && robot.Y < end.Y)
            {
                count++;
            }
        }

        return count;
    }

    public void Run_P1()
    {
        var MAX = new Coordinate(101, 103);
        var count = 7572;
        //foreach (var robot in DataD14.Robots)
        for (var i = 0; i < DataD14.Robots.Count; i++)
        {
            var robot = DataD14.Robots[i];
            robot.X = (robot.X + robot.Vx * count) % MAX.X;
            if (robot.X < 0)
            {
                robot.X = MAX.X + robot.X;
            }
            robot.Y = (robot.Y + robot.Vy * count) % MAX.Y;
            if (robot.Y < 0)
            {
                robot.Y = MAX.Y + robot.Y;
            }
            DataD14.Robots[i] = robot;
        }

        var robots = DataD14.Robots
                .OrderBy(r => r.X)
                .ThenBy(r => r.Y)
                .ToList();

        var res = 0;

        var start = new Coordinate(0, 0);
        var end = new Coordinate(MAX.X / 2, MAX.Y / 2);
        res = Count(robots, start, end);

        start = new Coordinate(MAX.X / 2 + 1, 0);
        end = new Coordinate(MAX.X, MAX.Y / 2);
        res *= Count(robots, start, end);

        start = new Coordinate(0, MAX.Y / 2 + 1);
        end = new Coordinate(MAX.X / 2, MAX.Y);
        res *= Count(robots, start, end);

        start = new Coordinate(MAX.X / 2 + 1, MAX.Y / 2 + 1);
        end = new Coordinate(MAX.X, MAX.Y);
        res *= Count(robots, start, end);

        //Console.WriteLine(res);

        Console.ReadKey();

        Console.SetCursorPosition(0, 0);
        for (var y = 0; y < MAX.Y; y++)
        {
            for (var x = 0; x < MAX.X; x++)
            {
                if (DataD14.Robots.Any(r => r.X == x && r.Y == y))
                {
                    Console.Write('.');
                }
                else
                {
                    Console.Write(' ');
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    public void Run()
    {
        var MAX = new Coordinate(101, 103);

        for (var count = 1; count < 10000; count++)
        {
            for (var i = 0; i < DataD14.Robots.Count; i++)
            {
                var robot = DataD14.Robots[i];
                robot.X = (robot.X + robot.Vx) % MAX.X;
                if (robot.X < 0)
                {
                    robot.X = MAX.X + robot.X;
                }
                robot.Y = (robot.Y + robot.Vy) % MAX.Y;
                if (robot.Y < 0)
                {
                    robot.Y = MAX.Y + robot.Y;
                }
                DataD14.Robots[i] = robot;
            }

            foreach (var robot in DataD14.Robots)
            {
                var start = new Coordinate(robot.X - 2, robot.Y - 2);
                var end = new Coordinate(robot.X + 3, robot.Y + 3);
                if (start.X >= 0 && start.Y >= 0 && end.X < MAX.X && end.Y < MAX.Y)
                {

                    var res = Count(DataD14.Robots, start, end);
                    if (res >= 25)
                    {
                        Console.WriteLine(count);
                    }
                }
            }
        }

        //Console.SetCursorPosition(0, 0);
        //for (var y = 0; y < MAX.Y; y++)
        //{
        //    for (var x = 0; x < MAX.X; x++)
        //    {
        //        if (DataD14.Robots.Any(r => r.X == x && r.Y == y))
        //        {
        //            Console.Write('.');
        //        }
        //        else
        //        {
        //            Console.Write(' ');
        //        }
        //    }
        //    Console.WriteLine();
        //}
        //Console.WriteLine();

        //Console.SetCursorPosition(0, 0);
        //for (var y = 0; y < map.Count; y++)
        //{
        //    var line = map[y];
        //    for (var x = 0; x < line.Count; x++)
        //    {
        //        if (map[y][x] == -2)
        //        {
        //            Console.Write('.');
        //        }
        //        else
        //        {
        //            Console.Write(map[y][x]);
        //        }
        //    }
        //    Console.WriteLine();
        //}
        //Console.WriteLine();
    }
}
