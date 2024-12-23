namespace AdventOfCode.aOLD.D17;

public class D17
{

    public void Run_P1()
    {
        DataD17.Read();

        var a = DataD17.A;
        var b = DataD17.B;
        var c = DataD17.C;

        var program = DataD17.Program;

        var output = new List<long>();

        for (var i = 0; i < program.Count;)
        {
            var op = program[i];
            var literal = program[i + 1];

            var combo = literal switch
            {
                0 => 0,
                1 => 1,
                2 => 2,
                3 => 3,
                4 => a,
                5 => b,
                6 => c,
                _ => int.MaxValue,
            };

            var hasToJump = false;
            var res = 0L;

            if (op == 0)
            {
                res = (long)(a / Math.Pow(2, combo));
                a = res;
            }
            else if (op == 1)
            {
                res = b ^ literal;
                b = res;
            }
            else if (op == 2)
            {
                res = combo % 8;
                b = res;
            }
            else if (op == 3)
            {
                if (a != 0)
                {
                    i = (int)literal;
                    hasToJump = true;
                }
            }
            else if (op == 4)
            {
                res = b ^ c;
                b = res;
            }
            else if (op == 5)
            {
                output.Add(combo % 8);
            }
            else if (op == 6)
            {
                res = (long)(a / Math.Pow(2, combo));
                b = res;
            }
            else if (op == 7)
            {
                res = (long)(a / Math.Pow(2, combo));
                c = res;
            }

            if (!hasToJump)
            {
                i += 2;
            }

            //if (output.Count > program.Count)
            //{
            //    break;
            //}
        }

        Console.WriteLine(string.Join(",", output));

    }

    public void Run()
    {
        DataD17.Read();

        var needs = new List<long>() { 2, 4, 1, 3, 7, 5, 0, 3, 1, 5, 4, 4, 5, 5, 3, 0 };
        //var start = 16325865043552L << 3;
        needs = needs.Skip(0).ToList();
        var j = 0;
        while (j < 8)
        {

            //Console.Write("For a: " +  start + " => ");
            var a = 236539226447469L;
            var b = DataD17.B;
            var c = DataD17.C;

            var program = DataD17.Program;

            var output = new List<long>();

            for (var i = 0; i < program.Count;)
            {
                var op = program[i];
                var literal = program[i + 1];

                var combo = literal switch
                {
                    0 => 0,
                    1 => 1,
                    2 => 2,
                    3 => 3,
                    4 => a,
                    5 => b,
                    6 => c,
                    _ => int.MaxValue,
                };

                var hasToJump = false;
                var res = 0L;

                if (op == 0)
                {
                    res = (long)(a / Math.Pow(2, combo));
                    a = res;
                }
                else if (op == 1)
                {
                    res = b ^ literal;
                    b = res;
                }
                else if (op == 2)
                {
                    res = combo % 8;
                    b = res;
                }
                else if (op == 3)
                {
                    if (a != 0)
                    {
                        i = (int)literal;
                        hasToJump = true;
                    }
                }
                else if (op == 4)
                {
                    res = b ^ c;
                    b = res;
                }
                else if (op == 5)
                {
                    output.Add(combo % 8);
                }
                else if (op == 6)
                {
                    res = (long)(a / Math.Pow(2, combo));
                    b = res;
                }
                else if (op == 7)
                {
                    res = (long)(a / Math.Pow(2, combo));
                    c = res;
                }

                if (!hasToJump)
                {
                    i += 2;
                }

                //if (output.Count > program.Count)
                //{
                //    break;
                //}
            }

            //if (start % 8000000 == 0)
            {
                Console.WriteLine($"For a: {130606920348432L} => {string.Join(",", output)} ({output.Count}) for needs ({string.Join(",", needs)})");
            }

            if (needs.SequenceEqual(output))
            {
                break;
            }
            j++;
            //start += 1;
        }
    }
}
