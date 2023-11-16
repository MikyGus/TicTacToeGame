using TicTacToe.Abstract;
using TicTacToe.Factories;
using TicTacToe.Models.Components;
using TicTacToe.Renderers;
using TicTacToe.States;

namespace TicTacToe.Models;
internal class WinnerChecker : IGridSubscriber
{
    private GameGrid _gameGrid;
    private readonly Vector2 _positionRenderOffset;
    public WinnerChecker(GameGrid gameGrid)
    {
        _gameGrid = gameGrid;
        _positionRenderOffset = new Vector2(2,2);
    }


    public void OnCellSet(Vector2 position, SpriteComponent spriteComponent)
    {
        var winnerPositions = new List<Vector2>();
        var player = spriteComponent?.Parent?.GetComponent<PlayerComponent>()?.Player;
        if (player is null)
            throw new ArgumentNullException();

        var horizontalMatches = CheckForWinner(position, player,
            p => p += Vector2Factory.RIGHT, p => p += Vector2Factory.LEFT).ToList();
        if (horizontalMatches.Count >= 4)
            winnerPositions.AddRange(horizontalMatches);

        var longitudeMatches = CheckForWinner(position, player,
            p => p += Vector2Factory.UP, p => p += Vector2Factory.DOWN).ToList();
        if (longitudeMatches.Count >= 4)
            winnerPositions.AddRange(longitudeMatches);

        var slashMatches = CheckForWinner(position, player,
            p => p += Vector2Factory.RIGHT_UP, p => p += Vector2Factory.LEFT_DOWN).ToList();
        if (slashMatches.Count >= 4)
            winnerPositions.AddRange(slashMatches);

        var backslashMatches = CheckForWinner(position, player,
            p => p += Vector2Factory.LEFT_UP, p => p += Vector2Factory.RIGHT_DOWN).ToList();
        if (backslashMatches.Count >= 4)
            winnerPositions.AddRange(backslashMatches);

        if (winnerPositions.Count > 0)
        {
            foreach (Vector2 pos in winnerPositions)
                ConsoleDraw.WriteAtPosition(pos + _positionRenderOffset, player.Sprite.ToString(), player.SpriteColor, ConsoleColor.Green);
            Program.GameEngine.SwitchState(new PlayerWonState());
        }
    }

    public void OnMarkedCellMoved(Vector2 oldPosition, Vector2 newPosition)
    {
    }

    public void OnNextPlayer(IPlayer oldPlayer, IPlayer newPlayer)
    {
    }

    private IEnumerable<Vector2> CheckForWinner(Vector2 startPosition, IPlayer player, Func<Vector2, Vector2> moveToNextPosition, Func<Vector2, Vector2> moveToNextPositionOposite)
    {
        //int foundMatches = 0;
        //var matches = new List<Vector2>
        //{
        //    startPosition
        //};
        yield return startPosition;

        Vector2 positionToCheck = startPosition;
        while (true)
        {
            positionToCheck = moveToNextPosition(positionToCheck);
            var checkPlayer = _gameGrid.PlayerAtPosition(positionToCheck);
            if (checkPlayer != player)
                break;
            //foundMatches++;
            //matches.Add(positionToCheck);
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

        //return foundMatches;
    }
}
