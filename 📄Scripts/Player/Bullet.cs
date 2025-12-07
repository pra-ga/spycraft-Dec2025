using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float speed = 20f;
    public float lifetime = 5f; // Destroy bullet after 5 seconds if it doesn't hit
    private float damage = 10f;

    private Transform target; // optional for homing bullets; currently unused
    PlayerCombat playerCombat;

    private void Start()
    {
        playerCombat = FindAnyObjectByType<PlayerCombat>();
        Destroy(gameObject, lifetime); // auto destroy after lifetime
    }

    private void Update()
    {
        // Move bullet forward
        transform.position += playerCombat.dir * speed * Time.deltaTime;
    }

    /// <summary>
    /// Optional: set damage dynamically from player script
    /// </summary>
    public void SetDamage(float dmg)
    {
        damage = dmg;
    }

    /// <summary>
    /// Optional: set target if you want homing bullets
    /// </summary>
    public void SetTarget(Transform t)
    {
        target = t;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Only damage objects tagged "Enemy"
        if (other.CompareTag("Enemy"))
        {
            // Try to find EnemyHealth script on enemy
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            // Destroy bullet on hit
            Destroy(gameObject);
        }
        else if (!other.CompareTag("Player"))
        {
            // Destroy bullet if hits any obstacle except player
            //Destroy(gameObject);
        }
    }
}
