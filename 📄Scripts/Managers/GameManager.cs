// GameManager.cs
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameStateMachine StateMachine { get; private set; }
    public EconomyManager Economy { get; private set; }
    public UpgradeManager Upgrades { get; private set; }

    // UIManager should be assigned in inspector or found at Start
    public UIManager uiManager;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

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
        if (uiManager == null)
            uiManager = FindObjectOfType<UIManager>();
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
}
