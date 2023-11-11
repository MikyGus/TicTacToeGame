using TicTacToe.Abstract;
using TicTacToe.Models;
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
        _gameGrid.AddSubscriber(_renderer);
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
        if (key.Key == ConsoleKey.Escape)
            Program.GameEngine.PushState(new MainMenuPausedState());
        else if (key.Key == ConsoleKey.A)
            _gameGrid.MoveCellMarker(Vector2.LEFT);
        else if (key.Key == ConsoleKey.W)
            _gameGrid.MoveCellMarker(Vector2.UP);
        else if (key.Key == ConsoleKey.D)
            _gameGrid.MoveCellMarker(Vector2.RIGHT);
        else if (key.Key == ConsoleKey.S)
            _gameGrid.MoveCellMarker(Vector2.DOWN);
        else if (key.Key == ConsoleKey.E)
            _gameGrid.SetSprite();
    }
}

