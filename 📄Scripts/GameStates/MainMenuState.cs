// MainMenuState.cs
using UnityEngine;

public class MainMenuState : IState
{
    private readonly GameStateMachine fsm;
    public MainMenuState(GameStateMachine fsm) { this.fsm = fsm; }

    public void Enter()
    {
        GameManager.Instance.uiManager.ShowMainMenu(true);
        Time.timeScale = 0f;
    }

    public void Exit()
    {
        GameManager.Instance.uiManager.ShowMainMenu(false);
    }

    public void Tick() { }
}
