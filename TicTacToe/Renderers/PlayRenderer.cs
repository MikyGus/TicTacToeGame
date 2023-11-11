using TicTacToe.Abstract;
using TicTacToe.Models;
using TicTacToe.Models.Components;

namespace TicTacToe.Renderers;
internal class PlayRenderer : IGridSubscriber
{
    private readonly GameGrid _gameGrid;
    private readonly SpriteComponent[,] _spriteBuffer;
    private readonly SpriteComponent _emptySpriteComponent;
    private readonly Vector2 _positionOffset;
    private readonly Vector2 _borderOffset;
    private readonly Vector2 _borderSize;
    public PlayRenderer(GameGrid gameGrid)
    {
        _gameGrid = gameGrid;
        _spriteBuffer = new SpriteComponent[_gameGrid.SizeOfGrid.X, _gameGrid.SizeOfGrid.Y];
        _emptySpriteComponent = new SpriteComponent() { Sprite = ' ', Parent = null, SpriteColor = ConsoleColor.Black };
        _borderOffset = new(1,1);
        _positionOffset = _borderOffset + new Vector2(1, 1);
        _borderSize = _gameGrid.SizeOfGrid + new Vector2(2,2);

        foreach (CellEntity cell in _gameGrid.Cells())
        {
            var component = cell.GetComponent<SpriteComponent>();
            if (component is not null)
                _spriteBuffer[cell.Position.X, cell.Position.Y] = component;
        }
    }


    public void RenderAll()
    {
        Console.Clear();
        Console.WriteLine("Play State");

        ConsoleDraw.Border(_borderOffset, _borderSize, _gameGrid.CurrentPlayer().SpriteColor);
    }

    public void OnMarkedCellMoved(Vector2 oldPosition, Vector2 newPosition)
    {
        var spriteComponentOld = _spriteBuffer[oldPosition.X, oldPosition.Y] ?? _emptySpriteComponent;
        ConsoleDraw.WriteAtPosition(oldPosition + _positionOffset, spriteComponentOld, ConsoleColor.Gray);

        var spriteComponentNew = _spriteBuffer[newPosition.X, newPosition.Y] ?? _emptySpriteComponent;
        ConsoleDraw.WriteAtPosition(newPosition + _positionOffset, spriteComponentNew, ConsoleColor.Red);
    }

    public void OnCellSet(Vector2 position, SpriteComponent spriteComponent)
    {
        ConsoleDraw.WriteAtPosition(position + _positionOffset, spriteComponent, ConsoleColor.Gray);
        _spriteBuffer[position.X, position.Y] = spriteComponent;
    }

    public void OnNextPlayer(IPlayer oldPlayer, IPlayer newPlayer)
    {
        ConsoleDraw.Border(_borderOffset, _borderSize, _gameGrid.CurrentPlayer().SpriteColor);
    }
}
