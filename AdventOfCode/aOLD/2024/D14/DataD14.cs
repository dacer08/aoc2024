using System.Numerics;
using System.Text.RegularExpressions;

namespace AdventOfCode.aOLD.D14;

public static class DataD14
{
    //private const string INPUT = "D14/InputTest.txt";
    private const string INPUT = "D14/Input.txt";

    private static List<D14Robot> _robots = [];

    public static List<D14Robot> Robots
    {
        get
        {
            if (_robots.Count == 0)
            {
                var lines = File.ReadAllLines(INPUT);
                for (var i = 0; i < lines.Length; i++)
                {
                    var line = lines[i];

                    var robot = new D14Robot();

                    var pattern = @"p\=(.*)\,(.*) v\=(.*),(.*)";
                    var match = Regex.Match(line, pattern);
                    robot.X = int.Parse(match.Groups[1].Value);
                    robot.Y = int.Parse(match.Groups[2].Value);
                    robot.Vx = int.Parse(match.Groups[3].Value);
                    robot.Vy = int.Parse(match.Groups[4].Value);
                    _robots.Add(robot);
                }
            }
            return _robots;
        }
    }

    public static void Display(List<List<char>> map)
    {
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