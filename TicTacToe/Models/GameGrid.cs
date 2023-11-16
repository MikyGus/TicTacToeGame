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
    private readonly IPlayer _nullPlayer;

    public Vector2 SizeOfGrid {  get; private set; }

    public GameGrid(Vector2 sizeOfGrid)
    {
        SizeOfGrid = sizeOfGrid;
        _cells = new CellEntity[SizeOfGrid.X, SizeOfGrid.Y];
        _gridSubscribers = new HashSet<IGridSubscriber>();
        _turnEngine = new PlayerTurnEngine(
            new List<IPlayer>
            {
                new Player("Player 1", 'X', ConsoleColor.Blue),
                new Player("Player 2", 'O', ConsoleColor.Yellow)
            });
        _nullPlayer = new Player("No player found", ' ', ConsoleColor.Black);
    }

    #region Actions to notify Subscribers
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
        SetMaySetMarkComponents(_turnEngine.CurrentPlayer.MarkerPosition);
        MoveToNextPlayer();
    }

    private void MoveToNextPlayer()
    {
        var oldPlayer = _turnEngine.CurrentPlayer;
        var newPlayer = _turnEngine.NextPlayer();

        foreach (IGridSubscriber subscriber in _gridSubscribers)
            subscriber.OnNextPlayer(oldPlayer, newPlayer);
    }
    #endregion

    #region Subscribers Add and remove
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
    #endregion

    #region Helper Methods for subscriber classes
    public IEnumerable<CellEntity> Cells()
    {
        foreach (var cell in _cells)
            if (cell is not null)
                yield return cell;
    }

    public IPlayer PlayerAtPosition(Vector2 position)
    {
        if (IsInGrid(position) == false)
            return _nullPlayer;
        
        return Cell(position)?.GetComponent<PlayerComponent>()?.Player ?? _nullPlayer;
    }

    public IPlayer CurrentPlayer() => _turnEngine.CurrentPlayer;

    public IEnumerable<MaySetMarkerComponent> GetAllMaySetMarkerComponents()
    {
        foreach (CellEntity entity in _cells)
        {
            var component = entity?.GetComponent<MaySetMarkerComponent>();
            if (component is not null)
                yield return component;
        }
    }

    public bool IsInGrid(Vector2 position)
        => (position.X >= 0 && position.Y >= 0 &&
            position.X < SizeOfGrid.X && position.Y < SizeOfGrid.Y);

    public bool MaySetAtPosition(Vector2 position)
    {
        if (IsInGrid(position) == false)
            return false;
        if (_havePlacedFirstMark == false)
            return true;
        return Cell(position)?.GetComponent<MaySetMarkerComponent>() is not null;
    }
    #endregion

    #region Internal Helper Functions
    private CellEntity Cell(Vector2 position) => _cells[position.X, position.Y];

    private bool IsPositionUsed(Vector2 position)
        => Cell(position)?.GetComponent<PlayerComponent>() is not null;

    private void SetMaySetMarkComponents(Vector2 position)
    {
        var component = Cell(position)?.GetComponent<MaySetMarkerComponent>();
        if (component is not null)
            Cell(position).RemoveComponent(component);

        foreach (Vector2 pos in position.Neighbours())
        {
            if (IsInGrid(pos) == false)
                continue;
            if (IsPositionUsed(pos))
                continue;
            var maySetMarkerComponent = Cell(pos)?.GetComponent<MaySetMarkerComponent>();
            if (maySetMarkerComponent is null)
                if (Cell(pos) is CellEntity entity)
                {
                    entity.AddComponent(ComponentFactory.MaySetMarkerComponent());
                }
                else
                {
                    _cells[pos.X, pos.Y] = new CellEntity() { Position = pos };
                    Cell(pos).AddComponent(ComponentFactory.MaySetMarkerComponent());
                }
        }
    }
    #endregion

}
