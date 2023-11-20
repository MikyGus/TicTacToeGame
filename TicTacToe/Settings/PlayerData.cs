using TicTacToe.Abstract;
using TicTacToe.Models;

namespace TicTacToe.Settings;

internal class PlayerData
{
    private int _activePlayerCount = 2;
    public IEnumerable<IPlayer> ActivePlayers => AllPlayers.Take(ActivePlayerCount);
    public IEnumerable<IPlayer> AllPlayers { get; } =
        new List<IPlayer>
            {
                new Player("Player 1", 'X', ConsoleColor.Blue),
                new Player("Player 2", 'O', ConsoleColor.Yellow),
                new Player("Player 3", '*', ConsoleColor.Cyan),
                new Player("Player 4", '§', ConsoleColor.Magenta)
            };
    public int ActivePlayerCount
    {
        get => _activePlayerCount;
        set
        {
            if (value is < 2 or > 4)
                throw new ArgumentOutOfRangeException(nameof(value));
            _activePlayerCount = value;
        }
    }

    public PlayerData Duplicate() => new()
    {
        ActivePlayerCount = this.ActivePlayerCount,
    };

}