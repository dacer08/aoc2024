using static System.Net.Mime.MediaTypeNames;

namespace AdventOfCode;

public class Node
{
    public Node Next = null!;
    public long Value;

    public void AddNext(long data)
    {
        Node toAdd = new Node();
        toAdd.Value = data;
        toAdd.Next = Next;
        Next = toAdd;
    }
}

public class LinkedListLong
{
    public Node First = null!;

    public void AddFirst(long data)
    {
        Node toAdd = new Node();
        toAdd.Value = data;
        First = toAdd;
    }
}