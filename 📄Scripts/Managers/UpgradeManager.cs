// UpgradeManager.cs
using UnityEngine;

public class UpgradeManager
{
    public int SpeedLevel { get; private set; }
    public int RateOfFireLevel { get; private set; }
    public int DamageLevel { get; private set; }

    // keys
    private const string KEY_SPEED = "up_speed";
    private const string KEY_ROF = "up_rof";
    private const string KEY_DMG = "up_dmg";

    public UpgradeManager()
    {
        SpeedLevel = 0;
        RateOfFireLevel = 0;
        DamageLevel = 0;
    }

    public int GetCostForNext(int currentLevel)
    {
        int nextLevel = currentLevel + 1;
        return 5 * nextLevel * nextLevel;
    }

    public bool TryBuySpeed()
    {
        int cost = GetCostForNext(SpeedLevel);
        if (!GameManager.Instance.Economy.TrySpendCoins(cost)) return false;
        SpeedLevel++;
        GameManager.Instance?.uiManager?.RefreshEconomyTexts();
        return true;
    }

    public bool TryBuyROF()
    {
        int cost = GetCostForNext(RateOfFireLevel);
        if (!GameManager.Instance.Economy.TrySpendCoins(cost)) return false;
        RateOfFireLevel++;
        GameManager.Instance?.uiManager?.RefreshEconomyTexts();
        return true;
    }

    public bool TryBuyDamage()
    {
        int cost = GetCostForNext(DamageLevel);
        if (!GameManager.Instance.Economy.TrySpendCoins(cost)) return false;
        DamageLevel++;
        GameManager.Instance?.uiManager?.RefreshEconomyTexts();
        return true;
    }

    public void Save()
    {
        PlayerPrefs.SetInt(KEY_SPEED, SpeedLevel);
        PlayerPrefs.SetInt(KEY_ROF, RateOfFireLevel);
        PlayerPrefs.SetInt(KEY_DMG, DamageLevel);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        SpeedLevel = PlayerPrefs.GetInt(KEY_SPEED, 0);
        RateOfFireLevel = PlayerPrefs.GetInt(KEY_ROF, 0);
        DamageLevel = PlayerPrefs.GetInt(KEY_DMG, 0);
    }
}
