public class PlayerWalkState : EntityState
{
    private Player player;

    public PlayerWalkState(Player player) : base(player)
    {
        this.player = player;
    }

    public override void Update()
    {
        if (!player.movement.HasMovementInput)
        {
            player.StateMachine.ChangeState(player.IdleState);
            return;
        }

        if (player.combat.HasTarget)
        {
            player.StateMachine.ChangeState(player.ShootState);
        }
    }
}
