using UnityEngine;

public class EnemyChaseState : EntityState
{
    Enemy enemy;

    public EnemyChaseState(Enemy e) : base(e) { enemy = e; }

    

    public override void Update()
    {
        float dist = Vector3.Distance(enemy.transform.position, enemy.target.position);

        if (dist <= enemy.attackRange)
        {
            enemy.StateMachine.ChangeState(enemy.AttackState);
            return;
        }

        /* if (dist > enemy.chaseRange)
        {
            enemy.StateMachine.ChangeState(enemy.IdleState);
            return;
        } */

        

        // Chase movement
        Vector3 dir = (enemy.target.position - enemy.transform.position).normalized;
        enemy.transform.position += dir * 3f * Time.deltaTime;

        // 3. If there's movement → rotate towards direction
        if (dir.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            enemy.transform.rotation = Quaternion.Slerp(
                enemy.transform.rotation,
                targetRotation,
                enemy.rotationSpeed * Time.deltaTime
            );
        }

        // 5. Animate
        float speedPercent = dir.magnitude;
        enemy.animator.SetFloat("Speed", speedPercent); // 0 = idle, >0 = walk
    }
}
