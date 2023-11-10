using TicTacToe.Abstract;

namespace TicTacToe;
internal class PlayerTurnEngine
{
    private readonly Queue<IPlayer> _playerQueue = new();
    public PlayerTurnEngine(IEnumerable<IPlayer> players)
    {
        foreach (IPlayer p in players) 
            if (p is not null) 
                _playerQueue.Enqueue(p);
            else
                throw new ArgumentNullException(nameof(p));
        if (_playerQueue.Count < 2)
            throw new ArgumentException($"Need a minimum of two players, have only {_playerQueue.Count}");
    }
    public int PlayerCount() => _playerQueue.Count;
    public IPlayer CurrentPlayer => _playerQueue.Peek();
    public IEnumerable<IPlayer> Players()
    {
        foreach(IPlayer p in _playerQueue)
            yield return p;
    }
    public IPlayer NextPlayer()
    {
        var player = _playerQueue.Dequeue();
        _playerQueue.Enqueue(player);
        return CurrentPlayer;
    }
}
