// UIButtonRouter.cs
using UnityEngine;

public class UIButtonRouter : MonoBehaviour
{
    private GameStateMachine fsm => GameManager.Instance.StateMachine;

    // Main Menu
    public void OnPlayPressed()
    {
        fsm.ChangeState(new GameplayState(fsm));
        DebugConsolePrintState();
        // If level scenes are separate and not loaded yet:
        //Note: 06Dec25 Is this really needed? Uncommented
        LevelManager.Instance.LoadLevelDirect(1);
    }

    void DebugConsolePrintState()
    {
        Debug.Log("State == " + fsm.Current.ToString());
    }

    public void OnQuitPressed()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }

    // Pause menu
    public void OnPausePressed()
    {
        Debug.Log("Pause Pressed");
        fsm.ChangeState(new PauseState(fsm));
        DebugConsolePrintState();
    }

    public void OnResumePressed()
    {
        fsm.ChangeState(new GameplayState(fsm));
        DebugConsolePrintState();
    }

    public void OnPauseMainMenuPressed()
    {
        fsm.ChangeState(new MainMenuState(fsm));
        DebugConsolePrintState();
    }

    // Upgrade menu
    public void OnBuySpeedPressed()
    {
        var up = new UpgradeState(fsm);
        up.OnBuySpeed();
        GameManager.Instance.uiManager.RefreshEconomyTexts();
    }

    public void OnBuyRofPressed()
    {
        var up = new UpgradeState(fsm);
        up.OnBuyROF();
        GameManager.Instance.uiManager.RefreshEconomyTexts();
    }

    public void OnBuyDmgPressed()
    {
        var up = new UpgradeState(fsm);
        up.OnBuyDamage();
        GameManager.Instance.uiManager.RefreshEconomyTexts();
    }

    public void OnNextLevelPressed()
    {
        /* var up = new UpgradeState(fsm);
        up.OnNextLevel(); */
        LevelManager.Instance.LoadNextLevel();
        DebugConsolePrintState();
    }

    // Game Over
    public void OnGameOverMainMenuPressed()
    {
        var go = new GameOverState(fsm);
        go.OnMainMenu();
        DebugConsolePrintState();
    }

    public void OnGameOverRestartPressed()
    {
        var go = new GameOverState(fsm);
        go.OnRestartLevel();
        DebugConsolePrintState();
    }

    public void ClearAllPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("All PlayerPrefs cleared.");
        GameManager.Instance.uiManager.RefreshEconomyTexts();
    }
}
