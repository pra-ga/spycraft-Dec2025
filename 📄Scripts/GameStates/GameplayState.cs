// GameplayState.cs
using UnityEngine;

public class GameplayState : IState
{
    private readonly GameStateMachine fsm;
    public GameplayState(GameStateMachine fsm) { this.fsm = fsm; }

    public void Enter()
    {
        /* Debug.Log("GM: " + GameManager.Instance);
        Debug.Log("UI: " + GameManager.Instance?.uiManager);
        Debug.Log("LM: " + LevelManager.Instance); */

        Time.timeScale = 1f;
        GameManager.Instance.uiManager.ShowGameplayUI(true);
        GameManager.Instance.uiManager.ShowPauseMenu(false);
        GameManager.Instance.uiManager.ShowUpgradeMenu(false);
        GameManager.Instance.uiManager.ShowGameOver(false);

        // Ensure LevelManager parses current scene
        LevelManager.Instance?.ParseCurrentSceneIndex();
        //06Dec 25 Note: to be fixed!
        GameManager.Instance.uiManager.SetLevelText(LevelManager.Instance.CurrentLevelNumber);

        // Hook to player death
        PlayerHealth.OnPlayerDeath += OnPlayerDeath;

        Debug.Log("---- Gameplay State ENTER ----");

        // Apply upgrades once player exists
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            var applier = player.GetComponent<UpgradeApplier>();
            if (applier != null)
                applier.ApplyUpgrades();
        }
    }

    public void Exit()
    {
        PlayerHealth.OnPlayerDeath -= OnPlayerDeath;
        GameManager.Instance.uiManager.ShowGameplayUI(false);
    }

    public void Tick()
    {
        /* if (Input.GetKeyDown(KeyCode.Escape))
        {
            fsm.ChangeState(new PauseState(fsm));
        } */
    }

    private void OnPlayerDeath()
    {
        // go to game over state
        fsm.ChangeState(new GameOverState(fsm));
    }
}
