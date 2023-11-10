using TicTacToe.Models;

namespace TicTacToe.Abstract;
internal interface IComponent
{
    IPlayer Parent { get; set; }
}
