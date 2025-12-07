// PauseState.cs
using UnityEngine;

public class PauseState : IState
{
    private readonly GameStateMachine fsm;
    public PauseState(GameStateMachine fsm) { this.fsm = fsm; }
    UIManager uIManager;

    public void Enter()
    {
        Time.timeScale = 0f;
        GameManager.Instance.uiManager.ShowPauseMenu(true);
    }

    public void Exit()
    {
        GameManager.Instance.uiManager.ShowPauseMenu(false);
        Time.timeScale = 1f;
    }

    public void Tick() { }
}
