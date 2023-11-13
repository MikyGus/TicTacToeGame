using TicTacToe.Abstract;

namespace TicTacToe.Models.Components;
internal class PlayerComponent : Component
{
    public IPlayer Player { get; init; }
}
