namespace AdventOfCode;

public class D09
{
    public void Run_P1()
    {

        var files = new List<Dfile>();
        var idRight = 0;
        var i = 0;

        var maxSpace = 0;

        for (i = 0; i < D09Data.Map.Count; i++) { 
            files.Add(new Dfile(idRight, D09Data.Map[i], i));
            idRight++;
            i++;
            if (i < D09Data.Map.Count)
            {
                maxSpace += D09Data.Map[i];
            }
        }

        var space = new List<int>();

        var idLeft = 0;
        idRight = files.Count - 1;
        var fill = 0;
        var idFill = 0;
        var empty = 0;

        i = 0;

        while (i < D09Data.Map.Count && idLeft <= idRight)
        {
            var current = files[idLeft];
            var size = current.Size;
            while (size > 0)
            {
                space.Add(current.Id);
                size--;
            }
            idLeft++;
            i++;

            if (i >= D09Data.Map.Count)
            {
                break;
            }

            empty = D09Data.Map[i];

            while (empty > 0 && idLeft <= idRight)
            {
                if (fill == 0)
                {
                    fill = files[idRight].Size;
                    idFill = files[idRight].Id;
                    idRight--;
                }

                space.Add(idFill);

                empty--;
                fill--;
            }
            i++;

            //D09Data.Display(space);
        }

        while (empty > 0 ||fill > 0)
        {
            if (fill == 0)
            {
                fill = files[idRight].Size;
                idFill = files[idRight].Id;
                idRight--;
            }

            space.Add(idFill);

            empty--;
            fill--;
        }

        //D09Data.Display(space);

        long count = 0;
        for (i = 0; i < space.Count; i++)
        {
            count += i * space[i];
        }

        Console.WriteLine(count);
    }

    public void Display(List<Dfile> space, List<Dfile> emptySpaces)
    {
        var r = "";
        var j = 0;
        for (var i = 0; i < space.Count; i++)
        {
            while (j < emptySpaces.Count && emptySpaces[j].Position < space[i].Position)
            {
                for (var x = 0; x < emptySpaces[j].Size; x++)
                {
                    r += ".";
                }
                j++;
            }

            for (var x = 0; x < space[i].Size; x++)
            {
                r += space[i].Id.ToString();
            }
        }
        Console.WriteLine(r);
    }

    public void Run()
    {

        var files = new List<Dfile>();
        var emptySpaces = new List<Dfile>();
        var idRight = 0;
        var i = 0;
        var j = 0;
        var position = 0;

        for (i = 0; i < D09Data.Map.Count; i++)
        {
            var file = new Dfile(idRight, D09Data.Map[i], position);
            files.Add(file);
            position += file.Size;
            idRight++;
            i++;
            if (i < D09Data.Map.Count)
            {
                file = new Dfile(idRight, D09Data.Map[i], position);
                emptySpaces.Add(file);
                position += file.Size;
            }
        }

        idRight = files.Count - 1;
        var space = files.Select(f => f).ToList();

        while (idRight > 0)
        {
            var file = files[idRight];
            for (i = 0; i < emptySpaces.Count && file.Position > emptySpaces[i].Position; i++)
            {
                var emptySpace = emptySpaces[i];
                if (emptySpace.Size >= file.Size)
                {
                    j = 0;
                    for (; j < space.Count; j++)
                    {
                        if (space[j].Position > emptySpace.Position)
                        {
                            break;
                        }
                    }
                    //j--;
                    var temp = space.First(s => s.Id == file.Id);
                    var newSpace = new Dfile(temp.Id, temp.Size, temp.Position);
                    space.Remove(temp);
                    temp.Position = emptySpace.Position;
                    space.Insert(j, temp);

                    emptySpace.Size -= file.Size;
                    emptySpace.Position += file.Size;
                    emptySpaces.RemoveAt(i);
                    if (emptySpace.Size != 0)
                    {
                        emptySpaces.Insert(i, emptySpace);
                    }
                    emptySpaces.Add(newSpace);
                    emptySpaces = emptySpaces.OrderBy(s => s.Position).ToList();

                    //Display(space, emptySpaces);
                    break;
                }
            }
            idRight--;
        }

        Display(space, emptySpaces);

        var result = new List<int>();
        j = 0;
        for (i = 0; i < space.Count; i++)
        {
            while (j < emptySpaces.Count && emptySpaces[j].Position < space[i].Position)
            {
                for (var x = 0; x < emptySpaces[j].Size; x++) { 
                    result.Add(0);
                }
                j++;
            }

            for (var x = 0; x < space[i].Size; x++)
            {
                result.Add(space[i].Id);
            }
        }

        long count = 0;
        for (i = 0; i < result.Count; i++)
        {
            count += i * result[i];
        }

        //D09Data.Display(result);
        Console.WriteLine(count);
    }
}
