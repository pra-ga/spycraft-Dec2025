using UnityEngine;

public class Enemy : Entity
{
    public float chaseRange = 8f;
    public float attackRange = 3f;
    public float attackCooldown = 1f;
    public float rotationSpeed = 10f;

    public Transform target;  // usually the player

    public EnemyIdleState IdleState { get; private set; }
    public EnemyChaseState ChaseState { get; private set; }
    public EnemyAttackState AttackState { get; private set; }
    public EnemyDeadState DeadState { get; private set; }
    public Animator animator;

    protected override void Awake()
    {
        base.Awake();

        IdleState = new EnemyIdleState(this);
        ChaseState = new EnemyChaseState(this);
        AttackState = new EnemyAttackState(this);
        DeadState = new EnemyDeadState(this);
        
    }

    private void Start()
    {
        StateMachine.ChangeState(IdleState);
    }
}
