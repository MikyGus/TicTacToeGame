using TicTacToe.Abstract;
using TicTacToe.Models.Components;
using TicTacToe.Renderers;
using TicTacToe.Settings;
using TicTacToe.States;

namespace TicTacToe.Models;
internal class WinnerChecker : IGridSubscriber
{
    private GameGrid _gameGrid;
    private readonly ConfigData _config;

    public WinnerChecker(GameGrid gameGrid)
    {
        _config = Program.Configuration.Data();
        _gameGrid = gameGrid;
    }


    public void OnCellSet(Vector2 position, SpriteComponent spriteComponent)
    {
        var winnerPositions = new List<Vector2>();
        var player = spriteComponent?.Parent?.GetComponent<PlayerComponent>()?.Player;
        if (player is null)
            throw new ArgumentNullException();

        var horizontalMatches = CheckForWinner(position, player,
            p => p += Vector2.RIGHT, p => p += Vector2.LEFT).ToList();
        if (horizontalMatches.Count >= _config.WinConditions.MarkersInRow)
            winnerPositions.AddRange(horizontalMatches);

        var longitudeMatches = CheckForWinner(position, player,
            p => p += Vector2.UP, p => p += Vector2.DOWN).ToList();
        if (longitudeMatches.Count >= _config.WinConditions.MarkersInRow)
            winnerPositions.AddRange(longitudeMatches);

        var slashMatches = CheckForWinner(position, player,
            p => p += Vector2.RIGHT_UP, p => p += Vector2.LEFT_DOWN).ToList();
        if (slashMatches.Count >= _config.WinConditions.MarkersInRow)
            winnerPositions.AddRange(slashMatches);

        var backslashMatches = CheckForWinner(position, player,
            p => p += Vector2.LEFT_UP, p => p += Vector2.RIGHT_DOWN).ToList();
        if (backslashMatches.Count >= _config.WinConditions.MarkersInRow)
            winnerPositions.AddRange(backslashMatches);

        if (winnerPositions.Count > 0)
        {
            foreach (Vector2 pos in winnerPositions)
                ConsoleDraw.WriteAtPosition(pos + _config.Grid.GridOffset, player.Sprite.ToString(), player.SpriteColor, ConsoleColor.Green);
            Program.GameEngine.SwitchState(new PlayerWonState(player));
        }
    }

    public void OnMarkedCellMoved(Vector2 oldPosition, Vector2 newPosition)
    {
    }

    public void OnNextPlayer(IPlayer oldPlayer, IPlayer newPlayer)
    {
    }

    private IEnumerable<Vector2> CheckForWinner(
        Vector2 startPosition, 
        IPlayer player, 
        Func<Vector2, Vector2> moveToNextPosition, 
        Func<Vector2, Vector2> moveToNextPositionOposite)
    {
        yield return startPosition;

        Vector2 positionToCheck = startPosition;
        while (true)
        {
            positionToCheck = moveToNextPosition(positionToCheck);
            var checkPlayer = _gameGrid.PlayerAtPosition(positionToCheck);
            if (checkPlayer != player)
                break;
            yield return positionToCheck;
        }

        positionToCheck = startPosition;
        while (true)
        {
            positionToCheck = moveToNextPositionOposite(positionToCheck);
            var checkPlayer = _gameGrid.PlayerAtPosition(positionToCheck);
            if (checkPlayer != player)
                break;
            yield return positionToCheck;
        }
    }
}
