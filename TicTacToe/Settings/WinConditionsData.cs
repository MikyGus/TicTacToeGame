namespace TicTacToe.Settings;

internal class WinConditionsData
{
    public int MarkersInRow { get; set; } = 4;
    public WinConditionsData Duplicate() => new()
    {
        MarkersInRow = this.MarkersInRow
    };
}