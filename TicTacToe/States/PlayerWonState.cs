using TicTacToe.Abstract;

namespace TicTacToe.States;
internal class PlayerWonState : IEngineState
{
    public PlayerWonState()
    {
        
    }

    public void Activate()
    {
        Console.SetCursorPosition(20, 0);
        Console.WriteLine("We have a winner!!!!!!!!!!");
    }

    public void Deactivate()
    {
    }

    public void Dispose()
    {
    }

    public void ProcessInput(ConsoleKeyInfo key)
    {
    }
}
