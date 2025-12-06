public class EntityStateMachine
{
    public EntityState CurrentState { get; private set; }

    public void ChangeState(EntityState newState)
    {
        if (CurrentState == newState) return;

        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState?.Enter();
    }

    public void Update()
    {
        CurrentState?.Update();
    }
}
