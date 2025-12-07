using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Combat Settings")]
    public float detectionRange = 10f;
    public float fireRate = 0.5f;
    public bool HasTarget => currentTarget != null;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    public Vector3 dir;

    [Header("References")]
    public Animator animator;
    public LayerMask enemyMask;

    private float nextFireTime = 0f;

    private Transform currentTarget;
    private bool isShooting;

    private void Update()
    {
        FindTarget();
        HandleCombat();
    }

    private void FindTarget()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRange, enemyMask);

        if (hits.Length == 0)
        {
            // Stop shooting when no enemies
            SetShooting(false);
            currentTarget = null;
            return;
        }

        // Pick closest enemy
        float bestDist = Mathf.Infinity;
        Transform best = null;

        foreach (var h in hits)
        {
            float d = Vector3.Distance(transform.position, h.transform.position);
            if (d < bestDist)
            {
                bestDist = d;
                best = h.transform;
            }
        }

        currentTarget = best;
        SetShooting(true);
    }

    /* private void HandleCombat()
    {
        if (!isShooting) return;
        if (currentTarget == null) return;

        // Rotate toward target
        Vector3 dir = currentTarget.position - transform.position;
        dir.y = 0;

        if (dir.sqrMagnitude > 0.01f)
        {
            Quaternion targetRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 10f * Time.deltaTime);
        }

        // Fire at fireRate
        if (Time.time >= nextFireTime)
        {
            // Shooting animation is continuous, so no trigger
            animator.Play("UB_Shoot", 1, 0f); // Force playback from start
            nextFireTime = Time.time + fireRate;
        }
    } */

    private void HandleCombat()
{
    if (!isShooting) return;
    if (currentTarget == null) return;

    // Direction to target
    dir = currentTarget.position - transform.position;
    dir.y = 0;

    if (dir.sqrMagnitude < 0.01f) return; // too close, ignore

    // Rotate toward target smoothly
    Quaternion targetRot = Quaternion.LookRotation(dir);
    transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 10f * Time.deltaTime);

    // Check if player is facing target within a certain angle (e.g., 15 degrees)
    float angleToTarget = Vector3.Angle(transform.forward, dir.normalized);
    if (angleToTarget > 15f) return; // don't shoot if facing away

    // Fire bullets at fireRate
    if (Time.time >= nextFireTime)
    {
        

        // Instantiate bullet
        if (bulletPrefab != null && firePoint != null)
        {
            

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            
            // Optional: set bullet damage, speed, or target if your bullet script has it
            Bullet b = bullet.GetComponent<Bullet>();
            if (b != null)
            {
                b.SetTarget(currentTarget); // assumes your bullet script has this method
                //b.SetDamage(damage);       // optional
                // Play shooting animation
                animator.Play("UB_Shoot", 1, 0f); // Force playback from start
            }
        }

        nextFireTime = Time.time + fireRate;
    }
}


    public void SetShooting(bool value)
    {
        if (isShooting == value) return;
        isShooting = value;
        animator.SetBool("isShooting", value);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
