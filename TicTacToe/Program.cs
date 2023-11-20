using TicTacToe.Models;
using TicTacToe.States;
using TicTacToe.Settings;

namespace TicTacToe;

internal class Program
{
    internal static StateEngine GameEngine { get; private set; }
    internal static Configuration Configuration { get; private set; }

    internal static void Main()
    {
        Console.CursorVisible = false;
        Console.Title = "TicTacToe";

        Configuration = new Configuration();
        Configuration.Configure(c =>
        {
            c.Grid.GridSize = new Vector2(55,20);
        });

        GameEngine = new StateEngine();
        GameEngine.PushState(new MainMenuState());

        while (GameEngine.IsRunning)
        {
            GameEngine.ProcessInput(Console.ReadKey(true));
        }
    }
}


