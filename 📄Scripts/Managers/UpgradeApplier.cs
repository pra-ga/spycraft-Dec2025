using UnityEngine;

public class UpgradeApplier : MonoBehaviour
{
    private PlayerMovement movement;
    private PlayerCombat combat;

    void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        combat = GetComponent<PlayerCombat>();
    }

    void Start()
    {
        ApplyUpgrades();
    }

    public void ApplyUpgrades()
    {
        var u = GameManager.Instance.Upgrades;

        // -------- SPEED -------------
        movement.moveSpeed = movement.baseMoveSpeed * (1f + 0.1f * u.SpeedLevel);

        // -------- FIRE RATE ----------
        combat.fireRate = combat.baseFireRate / (1f + 0.1f * u.RateOfFireLevel);

        // -------- DAMAGE -------------
        combat.currentDamage = combat.baseDamage + (2f * u.DamageLevel);

        Debug.Log($"Upgrades Applied → SPEED:{movement.moveSpeed} ROF:{combat.fireRate} DMG:{combat.currentDamage}");
    }
}
