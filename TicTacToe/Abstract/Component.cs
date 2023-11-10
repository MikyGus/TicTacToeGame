using TicTacToe.Models;

namespace TicTacToe.Abstract;
internal class Component : IComponent
{
    public IPlayer Parent { get; set; }
}
