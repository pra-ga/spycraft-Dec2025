// UIButtonRouter.cs
using UnityEngine;

public class UIButtonRouter : MonoBehaviour
{
    private GameStateMachine fsm => GameManager.Instance.StateMachine;

    // Main Menu
    public void OnPlayPressed()
    {
        fsm.ChangeState(new GameplayState(fsm));
        Debug.Log("State == "+fsm.Current.ToString());
        // If level scenes are separate and not loaded yet:
        //Note: 06Dec25 Is this really needed?
        //LevelManager.Instance.LoadLevelDirect(1);
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
        fsm.ChangeState(new PauseState(fsm));
    }

    public void OnResumePressed()
    {
        fsm.ChangeState(new GameplayState(fsm));
    }

    public void OnPauseMainMenuPressed()
    {
        fsm.ChangeState(new MainMenuState(fsm));
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
        var up = new UpgradeState(fsm);
        up.OnNextLevel();
    }

    // Game Over
    public void OnGameOverMainMenuPressed()
    {
        var go = new GameOverState(fsm);
        go.OnMainMenu();
    }

    public void OnGameOverRestartPressed()
    {
        var go = new GameOverState(fsm);
        go.OnRestartLevel();
    }
}
