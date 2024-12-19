namespace AdventOfCode.aOLD.D15;

public class D15
{
    public void Run_P1()
    {
        DataD15.Read();

        var robot = DataD15.Robot;
        var map = DataD15.Map;

        foreach (var movement in DataD15.Movements)
        {
            Coordinate vector;

            if (movement == '>')
                vector = Vectors.RIGHT;
            else if (movement == '<')
                vector = Vectors.LEFT;
            else if (movement == '^')
                vector = Vectors.TOP;
            else
                vector = Vectors.BOTTOM;

            var next = Vectors.Add(robot, vector);
            var c = map[next.Y][next.X];

            if (c == '.')
            {
                map[robot.Y][robot.X] = '.';
                map[next.Y][next.X] = '@';
                robot = next;
            }
            else if (c == 'O')
            {
                var block = next;
                while (c == 'O')
                {
                    block = Vectors.Add(block, vector);
                    c = map[block.Y][block.X];
                }

                if (c == '.')
                {
                    map[block.Y][block.X] = 'O';
                    map[robot.Y][robot.X] = '.';
                    map[next.Y][next.X] = '@';
                    robot = next;
                }
            }
            //DataD15.Display(map);
            //Thread.Sleep(300);
        }

        var res = 0;
        for (var y = 0; y < map.Count; y++)
        {
            var line = map[y];
            for (var x = 0; x < line.Count; x++)
            {
                if (map[y][x] == 'O')
                {
                    res += 100 * y + x;
                }
            }
        }
        Console.WriteLine(res);
    }

    public void Run()
    {
        DataD15.Read();

        var robot = DataD15.Robot;
        var map = DataD15.Map2;

        for (var y = 0; y < map.Count; y++)
        {
            var line = map[y];
            for (var x = 0; x < line.Count; x++)
            {
                if (map[y][x] == '@')
                {
                    robot = new Coordinate(x, y);
                }
            }
        }

        foreach (var movement in DataD15.Movements)
        {
            //DataD15.Display(map);
            //Thread.Sleep(150);
            Coordinate vector;

            var lateral = false;
            if (movement == '>')
            {
                lateral = true;
                vector = Vectors.RIGHT;
            }
            else if (movement == '<')
            {
                lateral = true;
                vector = Vectors.LEFT;
            }
            else if (movement == '^')
            {
                vector = Vectors.TOP;
            }
            else
            {
                vector = Vectors.BOTTOM;
            }

            var next = Vectors.Add(robot, vector);
            var c = map[next.Y][next.X];

            if (c == '.')
            {
                map[robot.Y][robot.X] = '.';
                map[next.Y][next.X] = '@';
                robot = next;
            }
            else if (c == '[' || c == ']')
            {
                if (lateral)
                {
                    var block = next;

                    while (c == '[' || c == ']')
                    {
                        block = Vectors.Add(block, vector);
                        c = map[block.Y][block.X];
                    }

                    if (c == '.')
                    {
                        var newVector = new Coordinate(vector.X * -1, vector.Y);
                        next = Vectors.Add(block, newVector);
                        while (c != '@')
                        {
                            c = map[next.Y][next.X];
                            map[block.Y][block.X] = map[next.Y][next.X];
                            block = next;
                            next = Vectors.Add(block, newVector);
                        }
                        //DataD15.Display(map);
                        map[robot.Y][robot.X] = '.';
                        robot = Vectors.Add(block, vector);
                        //DataD15.Display(map);
                    }
                }
                else
                {
                    var blocks = new HashSet<Coordinate>() { next };
                    if (c == '[')
                        blocks.Add(Vectors.Add(next, Vectors.RIGHT));
                    else
                        blocks.Add(Vectors.Add(next, Vectors.LEFT));

                    var y = next.Y;
                    next = Vectors.Add(next, vector);
                    var allEmpty = false;
                    var wall = false;
                    while (y != next.Y && !allEmpty && !wall)
                    {
                        allEmpty = true;
                        var newBlocks = new HashSet<Coordinate>();
                        wall = false;
                        foreach (var block in blocks.Where(b => b.Y == y))
                        {
                            c = map[block.Y][block.X];
                            next.X = block.X;
                            var nextC = map[next.Y][next.X];
                            if (nextC == '#')
                            {
                                wall = true;
                                break;
                            }
                            if (c == '[')
                            {
                                allEmpty = false;
                                if (nextC == '[')
                                {
                                    newBlocks.Add(next);
                                }
                                else if (nextC == ']')
                                {
                                    newBlocks.Add(next);
                                    newBlocks.Add(Vectors.Add(next, Vectors.LEFT));
                                }
                            }
                            else if (c == ']')
                            {
                                allEmpty = false;
                                if (nextC == ']')
                                {
                                    newBlocks.Add(next);
                                }
                                else if (nextC == '[')
                                {
                                    newBlocks.Add(next);
                                    newBlocks.Add(Vectors.Add(next, Vectors.RIGHT));
                                }
                            }
                        }

                        if (!allEmpty && !wall)
                        {
                            foreach (var block in newBlocks)
                            {
                                blocks.Add(block);
                            }
                            y = next.Y;
                            next = Vectors.Add(next, vector);
                        }
                    }

                    if (allEmpty && !wall)
                    {
                        var newVector = new Coordinate(vector.X, vector.Y * -1);

                        if (vector == Vectors.TOP)
                        {
                            blocks = blocks.OrderBy(b => b.Y).ToHashSet();
                        }
                        else if (vector == Vectors.BOTTOM)
                        {
                            blocks = blocks.OrderByDescending(b => b.Y).ToHashSet();
                        }
                        foreach (var block in blocks)
                        {
                            next = Vectors.Add(block, vector);
                            map[next.Y][next.X] = map[block.Y][block.X];
                            map[block.Y][block.X] = '.';
                            //DataD15.Display(map);
                            //if (map[block.Y + newVector.Y][block.X + newVector.X] == '.')
                            //{
                            //    map[block.Y][block.X] = '.';
                            //}
                        }

                        next = Vectors.Add(robot, vector);
                        map[robot.Y][robot.X] = '.';
                        map[next.Y][next.X] = '@';
                        robot = next;

                        next = Vectors.Add(robot, vector);
                        c = map[next.Y][next.X];
                        if (c == '[')
                        {
                            map[robot.Y + Vectors.RIGHT.Y][robot.X + Vectors.RIGHT.X] = '.';
                        }
                        else if (c == ']')
                        {
                            map[robot.Y + Vectors.LEFT.Y][robot.X + Vectors.LEFT.X] = '.';
                        }
                    }
                }
            }
            //DataD15.Display(map);
            //Thread.Sleep(150);
            //if (c == '.')
            //{
            //    map[robot.Y][robot.X] = '.';
            //    map[next.Y][next.X] = '@';
            //    robot = next;
            //}
            //else if (c == 'O')
            //{
            //    var block = next;
            //    while (c == 'O')
            //    {
            //        block = Vectors.Add(block, vector);
            //        c = map[block.Y][block.X];
            //    }

            //    if (c == '.')
            //    {
            //        map[block.Y][block.X] = 'O';
            //        map[robot.Y][robot.X] = '.';
            //        map[next.Y][next.X] = '@';
            //        robot = next;
            //    }
            //}
            //DataD15.Display(map);
            //Console.WriteLine(movement);
            //Thread.Sleep(10);
        }

        var res = 0;
        for (var y = 0; y < map.Count; y++)
        {
            var line = map[y];
            for (var x = 0; x < line.Count; x++)
            {
                if (map[y][x] == '[')
                {
                    res += 100 * y + x;
                }
            }
        }
        Console.WriteLine(res);
    }
}
