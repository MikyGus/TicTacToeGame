using TicTacToe.Models;

namespace TicTacToe.Abstract;
internal interface IComponent
{
    CellEntity Parent { get; set; }
    IPlayer Owner { get; init; }
}
