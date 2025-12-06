public class PlayerShootState : EntityState
{
    private Player player;

    public PlayerShootState(Player player) : base(player)
    {
        this.player = player;
    }

    public override void Enter()
    {
        player.combat.SetShooting(true);
    }

    public override void Exit()
    {
        player.combat.SetShooting(false);
    }

    public override void Update()
    {
        if (!player.combat.HasTarget)
        {
            // Switch back to movement states
            if (player.movement.HasMovementInput)
                player.StateMachine.ChangeState(player.WalkState);
            else
                player.StateMachine.ChangeState(player.IdleState);
        }
    }
}
