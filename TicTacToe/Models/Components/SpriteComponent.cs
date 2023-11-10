using TicTacToe.Abstract;

namespace TicTacToe.Models.Components;
internal class SpriteComponent : Component
{
    public required char Sprite { get; init; }
    public ConsoleColor SpriteColor { get; init; }
}
