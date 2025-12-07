// GameOverState.cs
using UnityEngine;

public class GameOverState : IState
{
    private readonly GameStateMachine fsm;
    private const int RESTART_COST = 100;

    public GameOverState(GameStateMachine fsm) { this.fsm = fsm; }

    public void Enter()
    {
        Time.timeScale = 0f;
        GameManager.Instance.uiManager.ShowGameOver(true);
        GameManager.Instance.uiManager.ShowGameplayUI(false);

        // Set interactability for restart button depending on coins
        bool canPay = GameManager.Instance.Economy.Coins >= RESTART_COST;
        GameManager.Instance.uiManager.ShowGameOverWithCost(canPay);
    }

    public void Exit()
    {
        GameManager.Instance.uiManager.ShowGameOver(false);
        Time.timeScale = 1f;
    }

    public void Tick() { }

    public void OnMainMenu()
    {
        fsm.ChangeState(new MainMenuState(fsm));
    }

    public void OnRestartLevel()
    {
        if (!GameManager.Instance.Economy.TrySpendCoins(RESTART_COST))
        {
            // not enough coins - button should have been disabled, but safeguard
            return;
        }

        // reload current level
        LevelManager.Instance.ReloadCurrentLevel();
        fsm.ChangeState(new GameplayState(fsm));
    }
}
