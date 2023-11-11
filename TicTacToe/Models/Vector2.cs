namespace TicTacToe.Models;
internal class Vector2
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

    public static Vector2 UP => new(0, -1);
    public static Vector2 DOWN => new(0, 1);
    public static Vector2 LEFT => new(-1, 0);
    public static Vector2 RIGHT => new(1, 0);
}
