namespace AdventOfCode;

public static class Vectors
{
    public static Coordinate LEFT = new Coordinate(-1, 0);
    public static Coordinate RIGHT = new Coordinate(1, 0);
    public static Coordinate BOTTOM = new Coordinate(0, 1);
    public static Coordinate TOP = new Coordinate(0, -1);
    public static Coordinate NULL = new Coordinate(0, 0);

    public static Coordinate Add(Coordinate a, Coordinate b)
    {
        return new Coordinate(a.X + b.X, a.Y + b.Y);
    }

    public static Coordinate Back(Coordinate a, Coordinate b)
    {
        return new Coordinate(a.X - b.X, a.Y - b.Y);
    }

    // TOP => LEFT => BOTTOM => RIGTH
    public static Coordinate Rotate90d(Coordinate a)
    {
        return new Coordinate(-a.Y, a.X);
    }

    public static Coordinate RotateMinus90d(Coordinate a)
    {
        return new Coordinate(a.Y, -a.X);
    }
}