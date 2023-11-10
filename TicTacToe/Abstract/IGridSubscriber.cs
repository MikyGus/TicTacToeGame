using TicTacToe.Models;
using TicTacToe.Models.Components;

namespace TicTacToe.Abstract;
internal interface IGridSubscriber
{
    void OnMarkedCellMoved(Vector2 oldPosition, Vector2 newPosition);
    void OnCellSet(Vector2 position, SpriteComponent spriteComponent);
}
