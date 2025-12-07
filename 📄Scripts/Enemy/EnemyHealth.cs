using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float MaxHP = 50f;
    private float currentHP;

    private void Awake()
    {
        currentHP = MaxHP;
    }

    public void TakeDamage(float dmg)
    {
        currentHP -= dmg;
        if (currentHP <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        // Add coin/xp drop
        EnemyDeath death = GetComponent<EnemyDeath>();
        if (death != null) death.Die();

        Destroy(gameObject);
    }
}
