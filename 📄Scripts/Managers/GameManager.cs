// GameManager.cs
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameStateMachine StateMachine { get; private set; }
    public EconomyManager Economy { get; private set; }
    public UpgradeManager Upgrades { get; private set; }

    LevelManager levelManager;

    // UIManager should be assigned in inspector or found at Start
    public UIManager uiManager;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        levelManager = FindAnyObjectByType<LevelManager>();

        StateMachine = new GameStateMachine();
        Economy = new EconomyManager();
        Upgrades = new UpgradeManager();

        // Load saved progress
        Economy.Load();
        Upgrades.Load();

        // Start at Main Menu
        StateMachine.ChangeState(new MainMenuState(StateMachine));

        Instance = this;

        if (uiManager == null)
            uiManager = FindAnyObjectByType<UIManager>();
    }

    private void Start()
    {
        // Decide initial game state based on scene
        if (LevelManager.Instance.IsGameplayScene())
        {
            // You are in Level1, Level2, Level3...
            StateMachine.ChangeState(new GameplayState(StateMachine));
        }
        else
        {
            // You are in Level0 (Main Menu)
            StateMachine.ChangeState(new MainMenuState(StateMachine));
        }

        if (uiManager == null)
            uiManager = FindObjectOfType<UIManager>();

            if (SceneManager.GetActiveScene().name.StartsWith("Level") &&
        SceneManager.GetActiveScene().name != "Level0")
        {
            // Every gameplay scene starts in GameplayState
            StateMachine.ChangeState(new GameplayState(StateMachine));
        }
        else
        {
            // Menu scene
            StateMachine.ChangeState(new MainMenuState(StateMachine));
        }
    }

    private void Update()
    {
        StateMachine.Tick();
    }

    private void OnApplicationQuit()
    {
        SaveAll();
    }

    public void SaveAll()
    {
        Economy.Save();
        Upgrades.Save();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Only trigger FSM if this is a gameplay scene
        if (LevelManager.Instance.IsGameplayScene())
        {
            // Show Gameplay HUD
            GameManager.Instance.uiManager.ShowGameplayUI(true);
            GameManager.Instance.uiManager.ShowMainMenu(false);

            // Enter GameplayState safely AFTER all objects exist
            StateMachine.ChangeState(new GameplayState(StateMachine));
        }
    }
}
