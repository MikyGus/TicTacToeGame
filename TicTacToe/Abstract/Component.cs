using TicTacToe.Models;

namespace TicTacToe.Abstract;
internal class Component : IComponent
{
    public CellEntity Parent { get; set; }
}
