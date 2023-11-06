﻿using TicTacToe.Abstract;

namespace TicTacToe.States;
internal class PlayState : IEngineState
{
    public void Activate()
    {
        Console.Clear();
        Console.WriteLine("Play State");
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
            Program.GameEngine.PushState(new MainMenuPausedState());
    }
}

