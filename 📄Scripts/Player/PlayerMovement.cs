using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public bool HasMovementInput => moveInput.sqrMagnitude > 0.01f;

    [Header("References")]
    public Transform cameraTransform;
    public Animator animator;

    private CharacterController controller;
    private Vector2 moveInput;
    private Vector3 velocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();

        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;
    }

    #region Input Callbacks
    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
        
    }
    #endregion

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        // 1. Camera-relative forward & right vectors
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        // Keep camera flattened
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        // 2. Convert WASD to world-space direction
        Vector3 moveDirection = camForward * moveInput.y + camRight * moveInput.x;
        


        // 3. If there's movement → rotate towards direction
        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        // 4. Move controller
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        // 5. Animate
        float speedPercent = moveDirection.magnitude;
        animator.SetFloat("Speed", speedPercent); // 0 = idle, >0 = walk
    }
}
