namespace AdventOfCode;

public class CustomLinkedList<T> where T : Node
{
    public Node First = null!;

    public void AddFirst(T node)
    {
        First = node;
    }
}