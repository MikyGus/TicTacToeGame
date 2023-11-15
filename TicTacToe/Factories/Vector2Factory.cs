using TicTacToe.Models;

namespace TicTacToe.Factories;
internal static class Vector2Factory
{
    public static Vector2 UP => new(0, -1);
    public static Vector2 DOWN => new(0, 1);
    public static Vector2 LEFT => new(-1, 0);
    public static Vector2 RIGHT => new(1, 0);
    public static Vector2 LEFT_UP => new(-1, -1);
    public static Vector2 LEFT_DOWN => new(-1, 1);
    public static Vector2 RIGHT_UP => new(1, -1);
    public static Vector2 RIGHT_DOWN => new(1, 1);

    public static IEnumerable<Vector2> Neighbours(this Vector2 position)
    {
        yield return position + UP;
        yield return position + DOWN;
        yield return position + LEFT;
        yield return position + RIGHT;
        yield return position + LEFT_UP;
        yield return position + RIGHT_UP;
        yield return position + LEFT_DOWN;
        yield return position + RIGHT_DOWN;
    }
}
