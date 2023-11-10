using TicTacToe.Abstract;
using TicTacToe.Models;
using TicTacToe.Models.Components;
using TicTacToe.Renderers;

namespace TicTacToe.States;
internal class PlayState : IEngineState
{
    private readonly PlayRenderer _renderer;
    private readonly GameGrid _gameGrid;

    public PlayState()
    {
        _gameGrid = new GameGrid(new Vector2(50,30));
        _renderer = new PlayRenderer(_gameGrid);
        _gameGrid.AddSubsriber(_renderer);
    }
    public void Activate()
    {
        _renderer.RenderAll();
    }

    public void Deactivate()
    {
    }

    public void Dispose()
    {
        _gameGrid.RemoveSubscriber(_renderer);
    }

    public void ProcessInput(ConsoleKeyInfo key)
    {
        var mark = _gameGrid.CellMarked;
        if (key.Key == ConsoleKey.Escape)
            Program.GameEngine.PushState(new MainMenuPausedState());
        else if (key.Key == ConsoleKey.A)
            _gameGrid.MoveCellMarker(new Vector2(mark.X - 1, mark.Y));
        else if (key.Key == ConsoleKey.W)
            _gameGrid.MoveCellMarker(new Vector2(mark.X, mark.Y - 1));
        else if (key.Key == ConsoleKey.D)
            _gameGrid.MoveCellMarker(new Vector2(mark.X + 1, mark.Y));
        else if (key.Key == ConsoleKey.S)
            _gameGrid.MoveCellMarker(new Vector2(mark.X, mark.Y + 1));
        else if (key.Key == ConsoleKey.E)
        {
            var cellEntity = new CellEntity(new SpriteComponent()
            {
                Sprite = 'X',
                Parent = null,
                SpriteColor = ConsoleColor.Blue
            })
            { Position = mark };
            _gameGrid.AddSprite(cellEntity,mark);
        }

    }
}

