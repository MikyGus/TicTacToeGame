using TicTacToe.Models;

namespace TicTacToe.Settings;

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
        GridSize = this.GridSize.Duplicate(),
        GridOffsetFromBorder = this.GridOffsetFromBorder.Duplicate(),
        BorderStartPositionTopLeft = this.BorderStartPositionTopLeft.Duplicate(),
    };
}


