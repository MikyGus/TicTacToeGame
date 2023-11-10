using TicTacToe.Abstract;
using TicTacToe.Models.Components;

namespace TicTacToe.Models;
internal class CellEntity
{
    private readonly IComponent _spriteComponent;
    public required Vector2 Position { get; set; }

    public CellEntity(SpriteComponent spriteComponent)
    {
        _spriteComponent = spriteComponent;
    }

    public SpriteComponent GetComponent() => (SpriteComponent)_spriteComponent;
}
