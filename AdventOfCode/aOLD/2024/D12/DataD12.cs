﻿namespace AdventOfCode.aOLD.D12;

public static class DataD12
{
    //private const string INPUT = "D12/InputTest.txt";
    private const string INPUT = "D12/Input.txt";

    private static List<List<char>> _map = [];

    public static List<List<char>> Map
    {
        get
        {
            if (_map.Count == 0)
            {
                _map = File.ReadAllLines(INPUT)
               .Select(l => l.Select(c => c).ToList())
               .ToList();
            }
            return _map;
        }
    }

    public static void Display(List<List<char>> map)
    {
        Console.SetCursorPosition(0, 0);
        for (var y = 0; y < map.Count; y++)
        {
            var line = map[y];
            for (var x = 0; x < line.Count; x++)
            {
                if (map[y][x] == -2)
                {
                    Console.Write('.');
                }
                else
                {
                    Console.Write(map[y][x]);
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}