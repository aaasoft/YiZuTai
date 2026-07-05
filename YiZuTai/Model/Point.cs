namespace YiZuTai.Model;

public struct Point : IEquatable<Point>
{
    public double X, Y;

    public Point(double x, double y)
    {
        X = x;
        Y = y;
    }
    public bool Equals(Point other)
    {
        return other.X == X && other.Y == Y;
    }
}