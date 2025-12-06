using UnityEngine;

public class Player : Entity
{
    public PlayerMovement movement;
    public PlayerCombat combat;

    public PlayerIdleState IdleState { get; private set; }
    public PlayerWalkState WalkState { get; private set; }
    public PlayerShootState ShootState { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        IdleState = new PlayerIdleState(this);
        WalkState = new PlayerWalkState(this);
        ShootState = new PlayerShootState(this);
    }

    private void Start()
    {
        StateMachine.ChangeState(IdleState);
    }
}
