using TicTacToe.States;

namespace TicTacToe;

internal class Program
{
    internal static StateEngine GameEngine { get; private set; }

    internal static void Main()
    {
        Console.CursorVisible = false;
        Console.Title = "TicTacToe";

        GameEngine = new StateEngine();
        GameEngine.PushState(new MainMenuState());

        while (GameEngine.IsRunning)
        {
            GameEngine.ProcessInput(Console.ReadKey(true));
        }
    }
}


