// EnemyDeath.cs
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public int coinValue = 1;
    public int xpValue = 1;

    public void Die()
    {
        GameManager.Instance.Economy.AddCoins(coinValue);
        GameManager.Instance.Economy.AddXP(xpValue);
        Destroy(gameObject);
    }
}
