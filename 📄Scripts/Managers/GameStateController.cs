using UnityEngine;

public class GameStateController : MonoBehaviour
{
    public static GameStateController Instance { get; private set; }

    public GameStateMachine FSM { get; private set; }

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // Create the FSM instance here
        FSM = new GameStateMachine();

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        // Run current state's logic
        FSM.Tick();
    }
}
