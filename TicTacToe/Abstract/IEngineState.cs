namespace TicTacToe.Abstract;
internal interface IEngineState : IDisposable
{
    void ProcessInput(ConsoleKeyInfo key);
    void Activate();
    void Deactivate();
}
