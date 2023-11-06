using TicTacToe.Abstract;

namespace TicTacToe.States;
internal class SettingsState : IEngineState
{
    public void Activate()
    {
        // TODO: MOVE THIS TO A RENDERER
        Console.Clear();
        Console.WriteLine("SETTINGS");
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
    }
}
