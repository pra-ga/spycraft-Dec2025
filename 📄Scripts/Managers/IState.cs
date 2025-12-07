// IState.cs
public interface IState
{
    void Enter();
    void Exit();
    void Tick(); // called each frame by GameManager
}
