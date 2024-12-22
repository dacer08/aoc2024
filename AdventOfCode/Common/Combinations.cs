namespace AdventOfCode;

// https://stackoverflow.com/a/10629938
public static class Combinations
{
    public static IEnumerable<IEnumerable<T>> GetKCombsWithRept<T>(IEnumerable<T> list, int length) where T : IComparable<T>
    {
        if (length == 1) return list.Select(t => new T[] { t });
        return GetKCombsWithRept(list, length - 1)
            .SelectMany(t => list.Where(o => o.CompareTo(t.Last()) >= 0),
                (t1, t2) => t1.Concat([t2]));
    }

    public static IEnumerable<IEnumerable<T>> GetKCombs<T>(IEnumerable<T> list, int length) where T : IComparable<T>
    {
        if (length == 1) return list.Select(t => new T[] { t });
        return GetKCombs(list, length - 1)
            .SelectMany(t => list.Where(o => o.CompareTo(t.Last()) > 0),
                (t1, t2) => t1.Concat([t2]));
    }
}