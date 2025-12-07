// UIManager.cs
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject MainMenuPanel;
    public GameObject GameplayHUD;
    public GameObject PauseMenu;
    public GameObject UpgradeMenu;
    public GameObject GameOverMenu;

    [Header("HUD Elements")]
    public TMP_Text LevelText;

    [Header("Upgrade UI")]
    public TMP_Text CoinsText;
    public TMP_Text XPText;
    public TMP_Text SpeedLevelText;
    public TMP_Text RofLevelText;
    public TMP_Text DmgLevelText;
    public Button UpgradeSpeedButton;
    public Button UpgradeRofButton;
    public Button UpgradeDmgButton;
    public Button UpgradeNextButton;

    [Header("GameOver UI")]
    public TMP_Text GameOverCoinsText;
    public Button GameOverRestartButton; // restart costs 100 coins
    public Button GameOverMainMenuButton;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    private void Start()
    {
        // ensure initial state is correct
        ShowMainMenu(true);
        ShowGameplayUI(false);
        ShowPauseMenu(false);
        ShowUpgradeMenu(false);
        ShowGameOver(false);
        RefreshEconomyTexts();
    }

    public void ShowMainMenu(bool v) => SetActiveSafe(MainMenuPanel, v);
    public void ShowGameplayUI(bool v) => SetActiveSafe(GameplayHUD, v);
    public void ShowPauseMenu(bool v) => SetActiveSafe(PauseMenu, v);
    public void ShowUpgradeMenu(bool v) => SetActiveSafe(UpgradeMenu, v);
    public void ShowGameOver(bool v) => SetActiveSafe(GameOverMenu, v);

    private void SetActiveSafe(GameObject g, bool v) { if (g != null) g.SetActive(v); }

    public void SetLevelText(int level)
    {
        if (LevelText != null) LevelText.text = $"Level {level}";
    }

    public void RefreshEconomyTexts()
    {
        if (CoinsText != null) CoinsText.text = $"Coins: {GameManager.Instance.Economy.Coins}";
        if (XPText != null) XPText.text = $"XP: {GameManager.Instance.Economy.XP}";
        if (SpeedLevelText != null) SpeedLevelText.text = $"Speed Lvl: {GameManager.Instance.Upgrades.SpeedLevel}";
        if (RofLevelText != null) RofLevelText.text = $"ROF Lvl: {GameManager.Instance.Upgrades.RateOfFireLevel}";
        if (DmgLevelText != null) DmgLevelText.text = $"Damage Lvl: {GameManager.Instance.Upgrades.DamageLevel}";
    }

    public void ShowGameOverWithCost(bool interactable)
    {
        if (GameOverRestartButton != null)
            GameOverRestartButton.interactable = interactable;
        if (GameOverCoinsText != null)
            GameOverCoinsText.text = $"Coins: {GameManager.Instance.Economy.Coins}";
    }
}
