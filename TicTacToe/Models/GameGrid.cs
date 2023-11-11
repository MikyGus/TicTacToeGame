using TicTacToe.Abstract;
using TicTacToe.Factories;
using TicTacToe.Models.Components;

namespace TicTacToe.Models;
internal class GameGrid
{
    private readonly CellEntity[,] _cells;
    
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
    public Vector2 CellMarked
    {
        get => _turnEngine.CurrentPlayer.CurrentMarkerPosition;
        private set => _turnEngine.CurrentPlayer.CurrentMarkerPosition = value;
    }

    public IEnumerable<CellEntity> Cells()
    {
        foreach (var cell in _cells)
            if (cell is not null)
                yield return cell;
    }

    public void MoveCellMarker(Vector2 newPosition)
    {
        if (IsInGrid(newPosition))
        {
            foreach (IGridSubscriber subscriber in _gridSubscribers)
                subscriber.OnMarkedCellMoved(CellMarked, newPosition);
            CellMarked = newPosition;
        }
    }

    public void SetSprite()
    {
        if (IsPositionUsed(CellMarked))
            return;

        var cellEntity = new CellEntity() { Position = CellMarked };
        var spriteComponent = ComponentFactory.SpriteComponent(_turnEngine.CurrentPlayer);
        cellEntity.AddComponent(spriteComponent);

        _cells[CellMarked.X, CellMarked.Y] = cellEntity;

        var sprite = cellEntity.GetComponent<SpriteComponent>();
        foreach (IGridSubscriber subscriber in _gridSubscribers)
            subscriber.OnCellSet(CellMarked, sprite);

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
}
