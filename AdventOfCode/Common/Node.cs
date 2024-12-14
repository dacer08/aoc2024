namespace AdventOfCode;

public abstract class Node
{
    public Node Next = null!;

    public void AddNext<T>(T node) where T : Node
    {
        Node toAdd = node;
        toAdd.Next = Next;
        Next = toAdd;
    }
}