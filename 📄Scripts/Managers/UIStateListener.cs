using UnityEngine;

public class UIStateListener : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject mainMenuUI;
    public GameObject gameplayUI;
    public GameObject upgradeUI;
    public GameObject winUI;
    public GameObject loseUI;

    private void OnEnable()
    {
        // Subscribe to game state change
        GameStateManager.Instance.OnGameStateChanged += OnStateChanged;
    }

    private void OnDisable()
    {
        // Unsubscribe
        GameStateManager.Instance.OnGameStateChanged -= OnStateChanged;
    }

    private void Start()
    {
        // Make sure UI reflects current state on scene load
        OnStateChanged(GameStateManager.Instance.CurrentState);
    }

    private void OnStateChanged(GameState state)
    {
        // Hide everything first
        HideAll();

        // Show the panel for the state
        switch (state)
        {
            case GameState.MainMenu:
                mainMenuUI?.SetActive(true);
                break;

            case GameState.LevelPlaying:
                gameplayUI?.SetActive(true);
                break;

            case GameState.UpgradeScreen:
                upgradeUI?.SetActive(true);
                break;

            case GameState.Win:
                winUI?.SetActive(true);
                break;

            case GameState.Lose:
                loseUI?.SetActive(true);
                break;
        }
    }

    private void HideAll()
    {
        mainMenuUI?.SetActive(false);
        gameplayUI?.SetActive(false);
        upgradeUI?.SetActive(false);
        winUI?.SetActive(false);
        loseUI?.SetActive(false);
    }
}
