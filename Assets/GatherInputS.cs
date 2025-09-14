using UnityEngine;
using UnityEngine.InputSystem;

public class GatherInput : MonoBehaviour
{
    private Control myControl;
    public float valueX;
    public bool jumpInput;

    private void Awake()
    {
        // Initialize input control
        myControl = new Control();
    }

    private void OnEnable()
    {
        // Subscribe to input events
        myControl.Player.Move.performed += OnMovePerformed;
        myControl.Player.Move.canceled += OnMoveCanceled;

        myControl.Player.Jump.performed += OnJumpStarted;
        myControl.Player.Jump.canceled += OnJumpCanceled;

        myControl.Player.Enable();
    }

    private void OnDisable()
    {
        // Unsubscribe from input events
        myControl.Player.Move.performed -= OnMovePerformed;
        myControl.Player.Move.canceled -= OnMoveCanceled;

        myControl.Player.Jump.performed -= OnJumpStarted;
        myControl.Player.Jump.canceled -= OnJumpCanceled;

        myControl.Player.Disable();

        // Reset values when disabled
        valueX = 0;
        jumpInput = false;
    }

    private void OnMovePerformed(InputAction.CallbackContext ctx)
    {
        valueX = ctx.ReadValue<float>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext ctx)
    {
        valueX = 0;
    }

    private void OnJumpStarted(InputAction.CallbackContext ctx)
    {
        jumpInput = true;
    }

    private void OnJumpCanceled(InputAction.CallbackContext ctx)
    {
        jumpInput = false;
    }
}