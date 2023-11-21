using TicTacToe.Abstract;
using TicTacToe.Factories;
using TicTacToe.Models;
using TicTacToe.Renderers;

namespace TicTacToe.States;
internal class PlayState : IEngineState
{
    private readonly GameGrid _gameGrid;
    private readonly PlayRenderer _renderer;
    private readonly TopBarRenderer _topBarRenderer;
    private readonly WinnerChecker _winnerChecker;

    public PlayState()
    {
        _gameGrid = new GameGrid();
        _renderer = new PlayRenderer(_gameGrid);
        _topBarRenderer = new TopBarRenderer(_gameGrid);
        _winnerChecker = new WinnerChecker(_gameGrid);
        _gameGrid.AddSubscriber(_renderer);
        _gameGrid.AddSubscriber(_topBarRenderer);
        _gameGrid.AddSubscriber(_winnerChecker);
    }
    public void Activate()
    {
        _renderer.RenderAll();
        _topBarRenderer.RenderAll();
    }

    public void Deactivate()
    {
    }

    public void Dispose()
    {
        _gameGrid.RemoveSubscriber(_renderer);
        _gameGrid.RemoveSubscriber(_topBarRenderer);
        _gameGrid.RemoveSubscriber(_winnerChecker);
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

