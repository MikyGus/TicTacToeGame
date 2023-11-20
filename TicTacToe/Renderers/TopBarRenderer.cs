using TicTacToe.Abstract;
using TicTacToe.Models;
using TicTacToe.Models.Components;

namespace TicTacToe.Renderers;
internal class TopBarRenderer : IGridSubscriber
{
    private readonly GameGrid _gameGrid;

    public TopBarRenderer(GameGrid gameGrid)
    {
        _gameGrid = gameGrid;
    }

    public void RenderAll()
    {
        var widthOfRow = 60;
        var player = _gameGrid.CurrentPlayer();
        var barToRender = String.Format("[{0}]-{1}",player.Sprite, player.Name);
        barToRender = barToRender[..(widthOfRow < barToRender.Length ? widthOfRow : barToRender.Length)].PadRight(widthOfRow);

        ConsoleDraw.WriteAtPosition(
            new Vector2(0, 0),
            barToRender,
            ConsoleColor.Black,
            player.SpriteColor);
    }

    public void OnCellSet(Vector2 position, SpriteComponent spriteComponent)
    {
    }

    public void OnMarkedCellMoved(Vector2 oldPosition, Vector2 newPosition)
    {
    }

    public void OnNextPlayer(IPlayer oldPlayer, IPlayer newPlayer)
    {
        RenderAll();
    }
}
