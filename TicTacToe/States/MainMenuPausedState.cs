using TicTacToe.Abstract;

namespace TicTacToe.States;
internal class MainMenuPausedState : IEngineState
{
    public void Activate()
    {
        // TODO: MOVE THIS TO A RENDERER
        Console.Clear();
        Console.WriteLine("MAIN MENU");
        Console.WriteLine("GAME IS PAUSED");
        Console.WriteLine("=========");
        Console.WriteLine("[ESC] Resume game");
        Console.WriteLine("[S]ettings");
        Console.WriteLine();
        Console.WriteLine("[Q]uit");
    }

    public void Deactivate()
    {
    }

    public void Dispose()
    {
    }

    public void ProcessInput(ConsoleKeyInfo key)
    {
        if (key.Key == ConsoleKey.Escape)
            Program.GameEngine.PopState();
        else if (key.Key == ConsoleKey.S)
            Program.GameEngine.PushState(new SettingsState());
        else if (key.Key == ConsoleKey.Q)
            Program.GameEngine.Quit();
    }
}
