namespace TicTacToe.Settings;

internal class ConfigData
{
    public GridData Grid { get; set; } = new();
    public PlayerData Player { get; set; } = new();
    public WinConditionsData WinConditions { get; set; } = new();

    public ConfigData Duplicate() => new()
    {
        Grid = this.Grid.Duplicate(),
        Player = this.Player.Duplicate(),
        WinConditions = this.WinConditions.Duplicate(),
    };

}
