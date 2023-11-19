namespace TicTacToe.Models;
internal class Vector2 : IEquatable<Vector2>
{
    public int X { get; set; }
    public int Y { get; set; }

    public Vector2(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static Vector2 operator +(Vector2 v1, Vector2 v2)
        => new(v1.X + v2.X, v1.Y + v2.Y);

    public static Vector2 operator -(Vector2 v1, Vector2 v2)
        => new(v1.X - v2.X, v1.Y - v2.Y);

    public static Vector2 operator *(Vector2 v1, int i)
        => new(v1.X * i, v1.Y * i);

    public bool Equals(Vector2 other)
    {
        if (other is null)
            return false;
        if (ReferenceEquals(this, other))
            return true;
        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object other)
    {
        if (other is null)
            return false;
        if (ReferenceEquals(this, other))
            return true;
        return other is Vector2 vector2 && Equals(vector2);
    }

    public static bool operator ==(Vector2 left, Vector2 right)
        => left.Equals(right);

    public static bool operator !=(Vector2 left, Vector2 right)
        => left.Equals(right) == false;

    public override int GetHashCode() => HashCode.Combine(X, Y);

}
