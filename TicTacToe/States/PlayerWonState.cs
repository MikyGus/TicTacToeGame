using TicTacToe.Abstract;
using TicTacToe.Models;
using TicTacToe.Renderers;

namespace TicTacToe.States;
internal class PlayerWonState : IEngineState
{
    private readonly IPlayer _player;

    public PlayerWonState(IPlayer player)
    {
        _player = player;
    }

    public void Activate()
    {
        var widthOfRow = 90;
        var barToRender = String.Format("[{0}]-{1} <<--- Is the winner!!!!!!! Press any key to return to main menu", _player.Sprite, _player.Name);
        barToRender = barToRender[..(widthOfRow < barToRender.Length ? widthOfRow : barToRender.Length)].PadRight(widthOfRow);

        ConsoleDraw.WriteAtPosition(new Vector2(0, 0), barToRender, ConsoleColor.Black, _player.SpriteColor);
    }

    public void Deactivate()
    {
    }

    public void Dispose()
    {
    }

    public void ProcessInput(ConsoleKeyInfo key)
    {
        Program.GameEngine.SwitchState(new MainMenuState());
    }
}
