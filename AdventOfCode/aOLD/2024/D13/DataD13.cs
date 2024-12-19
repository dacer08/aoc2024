using System.Numerics;
using System.Text.RegularExpressions;

namespace AdventOfCode.aOLD.D13;

public static class DataD13
{
    //private const string INPUT = "D13/InputTest.txt";
    private const string INPUT = "D13/Input.txt";

    private static List<Machine> _machines = [];

    public static List<Machine> Machines
    {
        get
        {
            if (_machines.Count == 0)
            {
                var lines = File.ReadAllLines(INPUT);
                for (var i = 0; i < lines.Length; i++)
                {
                    var line = lines[i];

                    var machine = new Machine();
                    //RegexOptions options = RegexOptions.Multiline;

                    var pattern = @"Button A\: X\+(.*)\, Y\+(.*)";
                    var match = Regex.Match(line, pattern);
                    machine.Ax = BigInteger.Parse(match.Groups[1].Value);
                    machine.Ay = BigInteger.Parse(match.Groups[2].Value);

                    i++;
                    line = lines[i];
                    pattern = @"Button B\: X\+(.*)\, Y\+(.*)";
                    match = Regex.Match(line, pattern);
                    machine.Bx = BigInteger.Parse(match.Groups[1].Value);
                    machine.By = BigInteger.Parse(match.Groups[2].Value);

                    i++;
                    line = lines[i];
                    pattern = @"Prize\: X\=(.*)\, Y\=(.*)";
                    match = Regex.Match(line, pattern);
                    machine.Px = BigInteger.Parse(match.Groups[1].Value);
                    machine.Py = BigInteger.Parse(match.Groups[2].Value);

                    i++;

                    _machines.Add(machine);
                }
            }
            return _machines;
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