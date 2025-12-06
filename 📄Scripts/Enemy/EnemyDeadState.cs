using UnityEngine;

public class EnemyDeadState : EntityState
{
    Enemy enemy;

    public EnemyDeadState(Enemy e) : base(e) { enemy = e; }

    public override void Enter()
    {
        Debug.Log("Enemy died.");
        // play death animation, disable collider etc.
    }
}
