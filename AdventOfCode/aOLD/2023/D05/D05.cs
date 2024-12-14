namespace AdventOfCode.Y2023.D05;

public class D05
{
    public int Explore(int index, int number)
    {
        var rules = DataD05.Categories[index];

        if (index < DataD05.Categories.Count - 1)
        {

        }

        return 0;
    }

    public void Run()
    {
       var a = DataD05.Categories;

        foreach (var seed in DataD05.Seeds)
        {
            Explore(0, seed);
        }

    }
}
