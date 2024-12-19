using System.Numerics;

namespace AdventOfCode.aOLD.D13;

public class D13
{

    public void Run()
    {
        BigInteger tokens = 0;
        foreach (var machine in DataD13.Machines)
        {
            var px = machine.Px + 10000000000000;
            var py = machine.Py + 10000000000000;

            var nom = py * machine.Bx - px * machine.By;
            var den = machine.Ay * machine.Bx - machine.Ax * machine.By;

            var a = nom / den;
            var bx2 = px - a * machine.Ax;

            if (nom % den == 0 && bx2 % machine.Bx == 0)
                tokens += 3 * a + bx2 / machine.Bx;
        }
        Console.WriteLine(tokens);
    }
}
