using TicTacToe.Abstract;

namespace TicTacToe.Models;
internal class GameGrid
{
    private readonly CellEntity[,] _cells;
    
    private readonly HashSet<IGridSubscriber> _gridSubscribers;
    public Vector2 CellMarked {  get; private set; }
    
    public Vector2 SizeOfGrid {  get; private set; }

    public GameGrid(Vector2 sizeOfGrid)
    {
        SizeOfGrid = sizeOfGrid;
        _cells = new CellEntity[SizeOfGrid.X, SizeOfGrid.Y];
        CellMarked = new Vector2(0, 0);
        _gridSubscribers = new HashSet<IGridSubscriber>();
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

    public void AddSprite(CellEntity cellEntity, Vector2 position)
    {
        //if (_cells[position.X, position.Y] is not null)
        //    throw new ArgumentException("Position is not empty");
        if (IsInGrid(position) == false)
            throw new ArgumentException("Position is outside set grid");
        if (cellEntity is null)
            throw new ArgumentNullException(nameof(cellEntity), string.Format("{0} needs to contain a marker.", nameof(cellEntity)));
        if (IsPositionUsed(position))
            return;
        
        _cells[position.X, position.Y] = cellEntity;

        foreach (IGridSubscriber subscriber in _gridSubscribers)
            subscriber.OnCellSet(CellMarked, cellEntity.GetComponent());
    }



    public void AddSubsriber(IGridSubscriber subscriber) 
    {
        if (_gridSubscribers.Add(subscriber) == false)
            throw new ArgumentException("The subscriber have already been added!");
    }

    public void RemoveSubscriber(IGridSubscriber subscriber)
    {
        if (_gridSubscribers.Remove(subscriber) == false)
            throw new ArgumentException("Could not remove subscriber, not subscribed.");
    }



    private bool IsInGrid(Vector2 position)
        => (position.X >= 0 && position.Y >= 0 &&
            position.X < SizeOfGrid.X && position.Y < SizeOfGrid.Y);

    public bool IsPositionUsed(Vector2 position) => (_cells[position.X, position.Y] is not null);
}
