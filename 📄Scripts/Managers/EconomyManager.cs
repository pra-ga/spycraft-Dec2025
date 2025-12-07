// EconomyManager.cs
using System;
using UnityEngine;

public class EconomyManager
{
    public int Coins { get; private set; }
    public int XP { get; private set; }

    // Events
    public event Action<int> OnCoinsChanged;
    public event Action<int> OnXPChanged;

    // PlayerPrefs keys
    private const string KEY_COINS = "eco_coins";
    private const string KEY_XP = "eco_xp";

    public EconomyManager()
    {
        Coins = 0;
        XP = 0;
    }

    public void AddCoins(int amount)
    {
        Coins += amount;
        OnCoinsChanged?.Invoke(Coins);
        GameManager.Instance?.uiManager?.RefreshEconomyTexts();
    }

    public bool TrySpendCoins(int amount)
    {
        if (Coins < amount) return false;
        Coins -= amount;
        OnCoinsChanged?.Invoke(Coins);
        GameManager.Instance?.uiManager?.RefreshEconomyTexts();
        return true;
    }

    public void AddXP(int amount)
    {
        XP += amount;
        OnXPChanged?.Invoke(XP);
        GameManager.Instance?.uiManager?.RefreshEconomyTexts();
    }

    public void Save()
    {
        PlayerPrefs.SetInt(KEY_COINS, Coins);
        PlayerPrefs.SetInt(KEY_XP, XP);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        Coins = PlayerPrefs.GetInt(KEY_COINS, 0);
        XP = PlayerPrefs.GetInt(KEY_XP, 0);
    }
}
