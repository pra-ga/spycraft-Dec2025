// GameStateMachine.cs
public class GameStateMachine
{

    private IState _current;

    public IState Current => _current;

    public void ChangeState(IState next)
    {
        _current?.Exit();
        _current = next;
        _current?.Enter();
    }

    public void Tick()
    {
        _current?.Tick();
    }
}
