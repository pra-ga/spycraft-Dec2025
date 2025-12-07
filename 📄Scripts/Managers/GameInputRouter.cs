using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputRouter : MonoBehaviour
{
    private InputSystem_Actions input;
    private GameStateMachine gameFSM;

    private void Awake()
    {
        input = new InputSystem_Actions();
        gameFSM = GameStateController.Instance.FSM;

    }

    private void OnEnable()
    {
        input.UI.Pause.performed += OnPause;
        input.Enable();
    }

    private void OnDisable()
    {
        input.UI.Pause.performed -= OnPause;
        input.Disable();
    }

    private void OnPause(InputAction.CallbackContext ctx)
    {
        // Only gameplay state can transition into pause
        if (gameFSM.Current is GameplayState)
        {
            gameFSM.ChangeState(new PauseState(gameFSM));
        }
    }
}
