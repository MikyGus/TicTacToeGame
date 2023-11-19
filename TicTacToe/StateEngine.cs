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
        if (_engineStates.Count > 0)
            _engineStates.Peek().Deactivate();
        _engineStates.Push(state);
        _engineStates.Peek().Activate();

    }

    public void PopState() 
    {
        if (_engineStates.Count == 0)
            throw new IndexOutOfRangeException("No states present");
        var currentState = _engineStates.Pop();
        currentState.Deactivate();
        currentState.Dispose();

        if (_engineStates.Count > 0)
            _engineStates.Peek().Activate();
    }

    /// <summary>
    /// Switch one state for another.
    /// </summary>
    /// <param name="state">The state to replace the current one with</param>
    public void SwitchState(IEngineState state)
    {
        if (_engineStates.Count > 0)
        {
            var oldState = _engineStates.Pop();
            oldState.Deactivate();
            oldState.Dispose();
        }

        _engineStates.Push(state);
        _engineStates.Peek().Activate();
    }

    /// <summary>
    /// Sends user input to the currently active state
    /// </summary>
    /// <param name="key"></param>
    public void ProcessInput(ConsoleKeyInfo key)
    {
        if (_engineStates.Count > 0)
            _engineStates.Peek().ProcessInput(key);
    }
}
