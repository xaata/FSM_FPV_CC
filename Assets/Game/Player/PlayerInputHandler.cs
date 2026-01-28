using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputHandler : MonoBehaviour
{
    private InputSystem_Actions _inputActions;
    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool RunPressed { get; private set; }
    public bool CrouchPressed { get; private set; }

    private void Awake()
    {
        _inputActions = new InputSystem_Actions();
    }
    private void OnEnable()
    {
        _inputActions.Player.Enable();

        _inputActions.Player.Move.performed += OnMove;
        _inputActions.Player.Move.canceled += OnMove;

        _inputActions.Player.Run.performed += OnRun;
        _inputActions.Player.Run.canceled += OnRun;

        _inputActions.Player.Look.performed += OnLook;
        _inputActions.Player.Look.canceled += OnLook;

        _inputActions.Player.Jump.started += OnJump;
        _inputActions.Player.Jump.canceled += OnJump;

        _inputActions.Player.Crouch.started += OnCrouch;
        _inputActions.Player.Crouch.canceled += OnCrouch;
    }
    private void OnDisable()
    {
        _inputActions.Player.Disable();

        _inputActions.Player.Move.performed -= OnMove;
        _inputActions.Player.Move.canceled -= OnMove;

        _inputActions.Player.Run.performed -= OnRun;
        _inputActions.Player.Run.canceled -= OnRun;

        _inputActions.Player.Look.performed -= OnLook;
        _inputActions.Player.Look.canceled -= OnLook;

        _inputActions.Player.Jump.started -= OnJump;
        _inputActions.Player.Jump.canceled -= OnJump;

        _inputActions.Player.Crouch.started -= OnCrouch;
        _inputActions.Player.Crouch.canceled -= OnCrouch;
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }
    public void OnRun(InputAction.CallbackContext context)
    {
        RunPressed = context.performed;
    }
    private void OnLook(InputAction.CallbackContext context)
    {
        LookInput = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        JumpPressed = context.ReadValueAsButton();
    }
    public void OnCrouch(InputAction.CallbackContext context)
    {
        CrouchPressed = context.ReadValueAsButton(); ;
    }
}