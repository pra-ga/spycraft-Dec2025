using UnityEngine;

public class EnemyAttackState : EntityState
{
    Enemy enemy;
    float nextAttackTime = 0f;

    public EnemyAttackState(Enemy e) : base(e) { enemy = e; }

    public override void Update()
    {
        float dist = Vector3.Distance(enemy.transform.position, enemy.target.position);

        if (dist > enemy.attackRange)
        {
            enemy.StateMachine.ChangeState(enemy.ChaseState);
            return;
        }

        if (Time.time >= nextAttackTime)
        {
            Debug.Log("Enemy attacks!");
            nextAttackTime = Time.time + enemy.attackCooldown;
        }
    }
}
