using TicTacToe.Abstract;
using TicTacToe.Models;
using TicTacToe.Models.Components;

namespace TicTacToe.Renderers;
internal class PlayRenderer : IGridSubscriber
{
    private readonly GameGrid _gameGrid;
    private readonly SpriteComponent[,] _spriteBuffer;
    private readonly SpriteComponent _emptySpriteComponent;
    public PlayRenderer(GameGrid gameGrid)
    {
        _gameGrid = gameGrid;
        _spriteBuffer = new SpriteComponent[_gameGrid.SizeOfGrid.X, _gameGrid.SizeOfGrid.Y];
        _emptySpriteComponent = new SpriteComponent() { Sprite = ' ', Parent = null, SpriteColor = ConsoleColor.Black };

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

        ConsoleDraw.Border(new Vector2(5,5), _gameGrid.SizeOfGrid);

    }

    public void OnMarkedCellMoved(Vector2 oldPosition, Vector2 newPosition)
    {
        var spriteComponentOld = _spriteBuffer[oldPosition.X, oldPosition.Y] ?? _emptySpriteComponent;
        ConsoleDraw.WriteAtPosition(oldPosition, spriteComponentOld, ConsoleColor.Gray);

        var spriteComponentNew = _spriteBuffer[newPosition.X, newPosition.Y] ?? _emptySpriteComponent;
        ConsoleDraw.WriteAtPosition(newPosition, spriteComponentNew, ConsoleColor.Red);
    }

    public void OnCellSet(Vector2 position, SpriteComponent spriteComponent)
    {
        ConsoleDraw.WriteAtPosition(position, spriteComponent, ConsoleColor.Gray);
        _spriteBuffer[position.X, position.Y] = spriteComponent;
    }

    public void OnNextPlayer(IPlayer oldPlayer, IPlayer newPlayer)
    {
        
    }
}
