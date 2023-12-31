﻿using TicTacToe.Abstract;

namespace TicTacToe.States;
internal class MainMenuState : IEngineState
{
    public void Activate()
    {
        // TODO: MOVE THIS TO A RENDERER
        Console.Clear();
        Console.WriteLine("MAIN MENU");
        Console.WriteLine("=========");
        Console.WriteLine("[N]ew game");
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
        if (key.Key == ConsoleKey.N)
            Program.GameEngine.SwitchState(new PlayState());
        else if (key.Key == ConsoleKey.S)
            Program.GameEngine.PushState(new SettingsState());
        else if (key.Key == ConsoleKey.Q)
            Program.GameEngine.Quit();
    }
}
