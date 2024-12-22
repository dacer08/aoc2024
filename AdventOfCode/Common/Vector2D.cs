using System.Numerics;

namespace AdventOfCode;

public class Vector2D : IComparable<Vector2D>
{
    public int X;

    public int Y;

    public Vector2D()
    {
        X = 0;
        Y = 0;
    }

    public Vector2D(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int CompareTo(Vector2D? other)
    {
        if (other == null) return 1;
        if (X == other.X && Y == other.Y) return 0;
        if (X < other.X) return -1;
        if (X == other.X && Y < other.Y) return -1;
        return 1;
    }

    public override string ToString()
    {
        return $"{{ X: {X}, Y: {Y} }}";
    }

    public override bool Equals(object? other)
    {
        if (other is not Vector2D item)
        {
            return false;
        }

        return X == item.X && Y == item.Y;
    }

    public static bool operator ==(Vector2D a, Vector2D b)
    {
        return Equals(a, b);
    }

    public static bool operator !=(Vector2D a, Vector2D b)
    {
        return !Equals(a, b);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y); ;
    }

    public static Vector2D LEFT = new(-1, 0);
    public static Vector2D RIGHT = new(1, 0);
    public static Vector2D BOTTOM = new(0, 1);
    public static Vector2D TOP = new(0, -1);
    public static Vector2D NULL = new(0, 0);

    public Vector2D Add(Vector2D other)
    {
        return new Vector2D(X + other.X, Y + other.Y);
    }

    public Vector2D Back(Vector2D other)
    {
        return new Vector2D(X - other.X, Y - other.Y);
    }

    public static Vector2D Add(Vector2D a, Vector2D b)
    {
        return new Vector2D(a.X + b.X, a.Y + b.Y);
    }

    public static Vector2D Back(Vector2D a, Vector2D b)
    {
        return new Vector2D(a.X - b.X, a.Y - b.Y);
    }

    // TOP => LEFT => BOTTOM => RIGHT
    public static Vector2D Rotate90d(Vector2D other)
    {
        return new Vector2D(-other.Y, other.X);
    }

    public Vector2D Rotate90d()
    {
        return new Vector2D(-Y, X);
    }

    // TOP => RIGHT => BOTTOM => LEFT
    public static Vector2D RotateMinus90d(Vector2D other)
    {
        return new Vector2D(other.Y, -other.X);
    }

    public Vector2D RotateMinus90d()
    {
        return new Vector2D(Y, -X);
    }
}