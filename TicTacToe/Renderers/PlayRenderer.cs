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
        DrawAllSprites();
    }

    public void OnMarkedCellMoved(Vector2 oldPosition, Vector2 newPosition)
    {
        ClearMarkerAtPosition(oldPosition);
        DrawMarkerAtPosition(newPosition);
    }

    public void OnCellSet(Vector2 position, SpriteComponent spriteComponent)
    {
        ConsoleDraw.WriteAtPosition(position + _positionOffset, spriteComponent, ConsoleColor.Gray);
        _spriteBuffer[position.X, position.Y] = spriteComponent;
    }

    public void OnNextPlayer(IPlayer oldPlayer, IPlayer newPlayer)
    {
        ConsoleDraw.Border(_borderOffset, _borderSize, _gameGrid.CurrentPlayer().SpriteColor);
        DrawMarkerAtPosition(_gameGrid.CurrentPlayer().MarkerPosition);
    }

    private void DrawMarkerAtPosition(Vector2 position)
    {
        var color = _spriteBuffer[position.X, position.Y] is not null ? ConsoleColor.Red : ConsoleColor.Green;
        var spriteComponent = _spriteBuffer[position.X, position.Y] is not null
            ? _spriteBuffer[position.X, position.Y]
            : _emptySpriteComponent;
        ConsoleDraw.WriteAtPosition(position + _positionOffset, spriteComponent, color);
    }

    private void ClearMarkerAtPosition(Vector2 position)
    {
        var spriteComponent = _spriteBuffer[position.X, position.Y] is not null
            ? _spriteBuffer[position.X, position.Y]
            : _emptySpriteComponent;
        ConsoleDraw.WriteAtPosition(position + _positionOffset, spriteComponent, ConsoleColor.Gray);
    }

    private void DrawAllSprites()
    {
        var markedPosition = _gameGrid.CurrentPlayer().MarkerPosition;
        foreach (SpriteComponent sprite in _spriteBuffer)
            if (sprite is not null)
            {
                ConsoleColor bgColor = markedPosition.Equals(sprite.Parent.Position) ? ConsoleColor.Red : ConsoleColor.Gray;
                ConsoleDraw.WriteAtPosition(sprite.Parent.Position + _positionOffset, sprite, bgColor);
            }
    }
}
