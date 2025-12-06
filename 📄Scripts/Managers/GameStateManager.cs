using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public enum GameState
{
    MainMenu,
    LevelPlaying,
    UpgradeScreen,
    Win,
    Lose
}

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }
    public GameState CurrentState { get; private set; }

    // Fired whenever state changes
    public event Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        // Start in MainMenu (or LevelPlaying if you want quick testing)
        ChangeState(GameState.MainMenu);
    }

    public void ChangeState(GameState newState)
    {
        CurrentState = newState;
        Debug.Log("Game State changed to: " + newState);

        // Notify listeners
        OnGameStateChanged?.Invoke(newState);

        HandleState(newState);
    }

    private void HandleState(GameState state)
    {
        switch (state)
        {
            case GameState.MainMenu:
                // Show main menu UI
                // Lock player input
                break;

            case GameState.LevelPlaying:
                // Allow player control
                // Spawn enemies, start timers, enable AI behaviors
                break;

            case GameState.UpgradeScreen:
                // Show upgrade UI
                // Disable player control
                break;

            case GameState.Win:
                // Show win screen
                // Disable movement + shooting
                break;

            case GameState.Lose:
                // Show lose screen
                // Disable movement + shooting
                break;
        }
    }

    // ----------------------------
    // Public API for other scripts
    // ----------------------------

    public void StartLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        ChangeState(GameState.LevelPlaying);
    }

    public void GoToUpgradeScreen()
    {
        ChangeState(GameState.UpgradeScreen);
    }

    public void WinLevel()
    {
        ChangeState(GameState.Win);
    }

    public void LoseLevel()
    {
        ChangeState(GameState.Lose);
    }

    public void ReturnToMainMenu(string menuScene)
    {
        SceneManager.LoadScene(menuScene);
        ChangeState(GameState.MainMenu);
    }
}
