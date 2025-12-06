using UnityEngine;

public abstract class EntityState
{
    protected Entity entity;

    protected EntityState(Entity entity)
    {
        this.entity = entity;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
}
