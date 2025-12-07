// PlayerHealth.cs
using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    public float MaxHP = 100f;
    private float currentHP;

    public static event Action OnPlayerDeath;

    private void Awake()
    {
        currentHP = MaxHP;
    }

    public void TakeDamage(float amount)
    {
        currentHP -= amount;
        if (currentHP <= 0)
        {
            currentHP = 0;
            Die();
        }
    }

    private void Die()
    {
        OnPlayerDeath?.Invoke();
    }

    public void HealFull()
    {
        currentHP = MaxHP;
    }

    public float GetPercent() => currentHP / MaxHP;
}
