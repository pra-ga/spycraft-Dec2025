using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Combat Settings")]
    public float detectionRange = 10f;
    public float fireRate = 0.5f;
    public bool HasTarget => currentTarget != null;


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

    private void HandleCombat()
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
