namespace AdventOfCode;

// https://stackoverflow.com/a/10629938
public static class Permutations
{
    public static IEnumerable<IEnumerable<T>> GetPermutationsWithRept<T>(IEnumerable<T> list, int length)
    {
        if (length == 1) return list.Select(t => new T[] { t });
        return GetPermutationsWithRept(list, length - 1)
            .SelectMany(t => list,
                (t1, t2) => t1.Concat([t2]));
    }

    public static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
    {
        if (length == 1) return list.Select(t => new T[] { t });
        return GetPermutations(list, length - 1)
            .SelectMany(t => list.Where(o => !t.Contains(o)),
                (t1, t2) => t1.Concat([t2]));
    }
}