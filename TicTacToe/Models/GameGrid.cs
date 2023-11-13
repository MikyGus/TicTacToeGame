using TicTacToe.Abstract;
using TicTacToe.Factories;
using TicTacToe.Models.Components;

namespace TicTacToe.Models;
internal class GameGrid
{
    private readonly CellEntity[,] _cells;
    private bool _havePlacedFirstMark = false;
    private readonly HashSet<IGridSubscriber> _gridSubscribers;
    private readonly PlayerTurnEngine _turnEngine;

    public Vector2 SizeOfGrid {  get; private set; }

    public GameGrid(Vector2 sizeOfGrid)
    {
        SizeOfGrid = sizeOfGrid;
        _cells = new CellEntity[SizeOfGrid.X, SizeOfGrid.Y];
        _gridSubscribers = new HashSet<IGridSubscriber>();
        _turnEngine = new PlayerTurnEngine(
            new List<IPlayer>
            {
                new Player("Player 1", 'X', ConsoleColor.Blue) { },
                new Player("Player 2", 'O', ConsoleColor.Yellow)
            });
    }

    public IPlayer CurrentPlayer() => _turnEngine.CurrentPlayer;

    public IEnumerable<CellEntity> Cells()
    {
        foreach (var cell in _cells)
            if (cell is not null)
                yield return cell;
    }

    public void MoveCellMarker(Vector2 moveDirection)
    {
        var newPosition = _turnEngine.CurrentPlayer.MarkerPosition + moveDirection;

        if (IsInGrid(newPosition))
        {
            foreach (IGridSubscriber subscriber in _gridSubscribers)
                subscriber.OnMarkedCellMoved(_turnEngine.CurrentPlayer.MarkerPosition, newPosition);
            _turnEngine.CurrentPlayer.MarkerPosition = newPosition;
        }
    }

    public void SetSprite()
    {
        if (MaySetAtPosition(_turnEngine.CurrentPlayer.MarkerPosition) == false)
            return;

        var cellEntity = new CellEntity() { Position = _turnEngine.CurrentPlayer.MarkerPosition };
        var spriteComponent = ComponentFactory.SpriteComponent(_turnEngine.CurrentPlayer);
        var playerComponent = ComponentFactory.PlayerComponent(_turnEngine.CurrentPlayer);
        cellEntity.AddComponent(spriteComponent);
        cellEntity.AddComponent(playerComponent);

        _cells[_turnEngine.CurrentPlayer.MarkerPosition.X, _turnEngine.CurrentPlayer.MarkerPosition.Y] = cellEntity;

        var sprite = cellEntity.GetComponent<SpriteComponent>();
        foreach (IGridSubscriber subscriber in _gridSubscribers)
            subscriber.OnCellSet(_turnEngine.CurrentPlayer.MarkerPosition, sprite);

        _havePlacedFirstMark = true;
        MoveToNextPlayer();
    }

    private void MoveToNextPlayer()
    {
        var oldPlayer = _turnEngine.CurrentPlayer;
        var newPlayer = _turnEngine.NextPlayer();

        foreach (IGridSubscriber subscriber in _gridSubscribers)
            subscriber.OnNextPlayer(oldPlayer, newPlayer);
    }



    // ***********************************
    // Subscribers
    public void AddSubscriber(IGridSubscriber subscriber) 
    {
        if (_gridSubscribers.Add(subscriber) == false)
            throw new ArgumentException($"The subscriber {nameof(subscriber)} have already been added!");
    }

    public void RemoveSubscriber(IGridSubscriber subscriber)
    {
        if (_gridSubscribers.Remove(subscriber) == false)
            throw new ArgumentException($"Could not remove {nameof(subscriber)}, not subscribed.");
    }
    // ***********************************


    private bool IsInGrid(Vector2 position)
        => (position.X >= 0 && position.Y >= 0 &&
            position.X < SizeOfGrid.X && position.Y < SizeOfGrid.Y);

    private bool IsPositionUsed(Vector2 position) => (_cells[position.X, position.Y] is not null);

    public bool MaySetAtPosition(Vector2 position)
    {
        if (IsInGrid(position) == false)
            return false;
        if (_havePlacedFirstMark == false)
            return true;
        if (IsPositionUsed(position) == true)
            return false;

        var positionsToCheck = new List<Vector2>()
        {
            position + Vector2.LEFT,
            position + Vector2.RIGHT,
            position + Vector2.DOWN,
            position + Vector2.UP,
            position + Vector2.LEFT + Vector2.UP,
            position + Vector2.LEFT + Vector2.DOWN,
            position + Vector2.RIGHT + Vector2.UP,
            position + Vector2.RIGHT + Vector2.DOWN,
        };
        foreach (var pos in positionsToCheck)
        {
            if (IsInGrid(pos) && _cells[pos.X, pos.Y] is not null)
                return true;
        }
        return false;
    }
    
}
