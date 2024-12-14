using System.Text.RegularExpressions;

namespace AdventOfCode.D03;

public class D03
{
    public static bool GetIndex(string text, int count)
    {
        var iDo = text.LastIndexOf("do()", count);
        var iDont = text.LastIndexOf("don't()", count);

        if (iDo == -1 && iDont == -1)
        {
            return true;
        }

        if (iDo == -1 && iDont >= 0)
        {
            return false;
        }

        if (iDont == -1 && iDo >= 0)
        {
            return true;
        }

        if (iDont >= iDo)
        {
            return false;
        }

        if (iDo >= iDont)
        {
            return true;
        }

        return true;
    }

    public void Run()
    {
        var line = File.ReadAllText("D03/InputTest.txt");
        var pattern = @"mul\(([0-9]{1,3})\,([0-9]{1,3})\)";

        var res = 0;
        foreach (Match m in Regex.Matches(line, pattern))
        {
            var n1 = int.Parse(m.Groups[1].Value);
            var n2 = int.Parse(m.Groups[2].Value);
            if (GetIndex(line, m.Index))
            {
                res += n1 * n2;
            }
        }

        Console.WriteLine(res);
    }
}
