using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public EntityStateMachine StateMachine { get; private set; }

    protected virtual void Awake()
    {
        StateMachine = new EntityStateMachine();
    }

    protected virtual void Update()
    {
        StateMachine.Update();
    }
}
