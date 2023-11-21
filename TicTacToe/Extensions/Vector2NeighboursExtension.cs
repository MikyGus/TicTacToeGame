using TicTacToe.Models;

namespace TicTacToe.Extensions;
internal static class Vector2NeighboursExtension
{
    public static IEnumerable<Vector2> Neighbours(this Vector2 position)
    {
        yield return position + Vector2.UP;
        yield return position + Vector2.DOWN;
        yield return position + Vector2.LEFT;
        yield return position + Vector2.RIGHT;
        yield return position + Vector2.LEFT_UP;
        yield return position + Vector2.RIGHT_UP;
        yield return position + Vector2.LEFT_DOWN;
        yield return position + Vector2.RIGHT_DOWN;
    }
}
