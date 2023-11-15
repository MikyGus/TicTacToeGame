using TicTacToe.Abstract;

namespace TicTacToe.Models.Components;
internal class MaySetMarkerComponent : Component
{
    public bool MaySetInCell { get; init; }
    public ConsoleColor BackgroundColor { get; init; }
}
