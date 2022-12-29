using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Processors;

public class InputParcer : MonoBehaviour
{
    private PlayerInput _playerInput;
    private InputActionAsset _playerControlsActions;

    [Header("Scripts")] 
    [SerializeField] private PlayerBasicMovement playerMovement;
    [SerializeField] private DashAbillity dashAbillity;
    
    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerControlsActions = _playerInput.actions;
        
        _playerControlsActions["Roll"].performed += Roll;
        _playerControlsActions["Dash"].performed += Dash;
    }

    private void FixedUpdate()
    {
        var moveInput = _playerControlsActions["Move"].ReadValue<Vector2>();
        playerMovement.SetMoveDirection(moveInput);

        if (!dashAbillity.isDashing) return;

        var mousePos = _playerControlsActions["MousePosition"].ReadValue<Vector2>();
        dashAbillity.SetMousePos(mousePos);
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    public void RemoveListeners()
    {
        _playerControlsActions["Roll"].performed -= Roll;
    }

    private void Roll(InputAction.CallbackContext context) => playerMovement.ActivateRoll();
    private void Dash(InputAction.CallbackContext context) => dashAbillity.ActivateDash();
}
