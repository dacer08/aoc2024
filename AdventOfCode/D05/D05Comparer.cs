namespace AdventOfCode;
public class D05Comparer : IComparer<int>
{
    public int Compare(int p1, int p2)
    {
        if (p1 == p2) return 0;

        if (D05Data.PageOrders.Any(p => p.P1 == p2 && p.P2 == p1))
        {
            return 1;
        }

        return -1;
    }
}