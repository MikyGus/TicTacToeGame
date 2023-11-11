using TicTacToe.Models;
using TicTacToe.Models.Components;

namespace TicTacToe.Renderers;
internal static class ConsoleDraw
{
    public static void WriteAtPosition(Vector2 position, SpriteComponent spriteComponent)
    {
        WriteAtPosition(position, spriteComponent, Console.BackgroundColor);
    }
    public static void WriteAtPosition(Vector2 position, SpriteComponent spriteComponent, ConsoleColor backgroundColor)
    {
        var tempBackgroundColor = Console.BackgroundColor;
        var tempFontColor = Console.ForegroundColor;
        Console.BackgroundColor = backgroundColor;
        Console.ForegroundColor = spriteComponent.SpriteColor;

        Console.SetCursorPosition(position.X, position.Y);
        Console.Write(spriteComponent.Sprite);

        Console.BackgroundColor = tempBackgroundColor;
        Console.ForegroundColor = tempFontColor;
    }

    public static void Border(Vector2 startPosition, Vector2 gridSize)
        => Border(startPosition, gridSize, ConsoleColor.Black);
    public static void Border(Vector2 startPosition, Vector2 gridSize, ConsoleColor borderColor)
    {
        // ╔ ═ ╗ ╚ ╝ ║ 
        // |

        if (gridSize.X < 3 || gridSize.Y < 3)
            throw new ArgumentOutOfRangeException(nameof(gridSize));

        var leftTop = "╔";
        var rightTop = "╗";
        var rowMiddle = new String('═', gridSize.X - 2);
        var leftBottom = "╚";
        var rightBottom = "╝";
        var leftRightEdge = "║";

        var topRow = leftTop + rowMiddle + rightTop;
        var bottomRow = leftBottom + rowMiddle + rightBottom;

        var previousColor = Console.ForegroundColor;
        Console.ForegroundColor = borderColor;

        for (var y = startPosition.Y; y < startPosition.Y + gridSize.Y; y++)
        {
            if (y == startPosition.Y)
            {
                Console.SetCursorPosition(startPosition.X, y);
                Console.Write(topRow);
            }
            else if (y == startPosition.Y + gridSize.Y - 1)
            {
                Console.SetCursorPosition(startPosition.X, y);
                Console.Write(bottomRow);
            }
            else
            {
                Console.SetCursorPosition(startPosition.X, y);
                Console.Write(leftRightEdge);
                Console.SetCursorPosition(startPosition.X + gridSize.X - 1, y);
                Console.Write(leftRightEdge);
            }
        }
        Console.ForegroundColor = previousColor;
    }
}
