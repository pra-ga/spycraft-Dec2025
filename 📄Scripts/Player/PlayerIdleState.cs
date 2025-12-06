public class PlayerIdleState : EntityState
{
    private Player player;

    public PlayerIdleState(Player player) : base(player)
    {
        this.player = player;
    }

    public override void Update()
    {
        if (player.movement.HasMovementInput)
        {
            player.StateMachine.ChangeState(player.WalkState);
            return;
        }

        if (player.combat.HasTarget)
        {
            player.StateMachine.ChangeState(player.ShootState);
        }
    }
}
