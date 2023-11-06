using TicTacToe.Abstract;

namespace TicTacToe;
internal class StateEngine
{
    private readonly Stack<IEngineState> _engineStates = new();
    public bool IsRunning { get; private set; } = true;

    public void Quit()
    {
        IsRunning = false;
    }

    public void PushState(IEngineState state)
    {

    }

    public void PopState(IEngineState state) 
    {

    }

    public void SwitchState(IEngineState state)
    {

    }

    public void ProcessInput(ConsoleKeyInfo key)
    {

    }
}
