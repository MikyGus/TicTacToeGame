using TicTacToe.Abstract;
using TicTacToe.Models;

namespace TicTacToe.Settings;

internal class ConfigData
{
    public GridData Grid { get; set; } = new();
    public PlayerData Player { get; set; } = new();
    public WinConditionsData WinConditions { get; set; } = new();

    public ConfigData Duplicate() => new()
    {
        Grid = this.Grid,
        Player = this.Player,
        WinConditions = this.WinConditions,
    };

}

internal class WinConditionsData
{
    public int MarkersInRow { get; set; } = 4;
    public WinConditionsData Duplicate() => new()
    {
        MarkersInRow = this.MarkersInRow
    };
}

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

internal class GridData
{
    /// <summary>
    /// Size of the playable grid area. Width and height.
    /// </summary>
    public Vector2 GridSize { get; set; } = new Vector2(10, 10);

    /// <summary>
    /// Offset from the top-left corner of the Console-window to the top-left of the playable grid area. 
    /// Or you could say, translation from GameGrid to where to draw the players markers.
    /// (BorderStartPositionTopLeft + GridOffsetFromBorder)
    /// </summary>
    public Vector2 GridOffset => BorderStartPositionTopLeft + GridOffsetFromBorder;

    /// <summary>
    /// Offset from the top-left corner of the border to the top-left of the playable grid area.
    /// </summary>
    public Vector2 GridOffsetFromBorder { get; set; } = new Vector2(1,1);

    /// <summary>
    /// Position offset, top left, for the border surrounding the playable grid area.
    /// Or you could say, position to start drawing the border.
    /// </summary>
    public Vector2 BorderStartPositionTopLeft { get; set; } = new Vector2(1, 1);

    /// <summary>
    /// Size of entire border. Border and Grid included.
    /// ((GridOffsetFromBorder * 2) + GridSize)
    /// </summary>
    public Vector2 BorderSize => (GridOffsetFromBorder * 2) + GridSize;

    public GridData Duplicate() => new()
    {
        GridSize = this.GridSize,
        GridOffsetFromBorder = this.GridOffsetFromBorder,
        BorderStartPositionTopLeft = this.BorderStartPositionTopLeft,
    };
}


