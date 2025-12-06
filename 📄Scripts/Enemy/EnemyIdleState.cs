using UnityEngine;

public class EnemyIdleState : EntityState
{
    Enemy enemy;

    public EnemyIdleState(Enemy e) : base(e) { enemy = e; }

    public override void Update()
    {
        float dist = Vector3.Distance(enemy.transform.position, enemy.target.position);

        if (dist < enemy.chaseRange)
        {
            enemy.StateMachine.ChangeState(enemy.ChaseState);
        }
    }
}
