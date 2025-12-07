// UpgradeState.cs
using UnityEngine;

public class UpgradeState : IState
{
    private readonly GameStateMachine fsm;

    public UpgradeState(GameStateMachine fsm) { this.fsm = fsm; }

    public void Enter()
    {
        Time.timeScale = 0f;
        GameManager.Instance.uiManager.ShowUpgradeMenu(true);
        GameManager.Instance.uiManager.RefreshEconomyTexts();
    }

    public void Exit()
    {
        GameManager.Instance.uiManager.ShowUpgradeMenu(false);
        Time.timeScale = 1f;
    }

    public void Tick() { }

    // Called by UI button handlers
    public void OnBuySpeed() { GameManager.Instance.Upgrades.TryBuySpeed(); }
    public void OnBuyROF() { GameManager.Instance.Upgrades.TryBuyROF(); }
    public void OnBuyDamage() { GameManager.Instance.Upgrades.TryBuyDamage(); }

    public void OnNextLevel()
    {
        GameManager.Instance.Upgrades.Save();
        GameManager.Instance.Economy.Save();
        LevelManager.Instance.LoadNextLevel();
        // Move to GameplayState after load - loading scene will re-run GameManager Start; to be safe:
        fsm.ChangeState(new GameplayState(fsm));
    }
}
