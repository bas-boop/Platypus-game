using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateManager : StateMachineManager
{
    private PlayerInput _playerInput;
    private InputActionAsset _playerControlsActions;

    public PlayerMoveData moveData;
    
    [Header("States")]
    public PlayerIdleState idleState;
    public PlayerWalkingState walkingState;
    public PlayerRollState rollState;
    public PlayerDashState dashState;
    public PlayerSmackState smackState;

    private new void Awake()
    {
        idleState = GetComponent<PlayerIdleState>();
        walkingState = GetComponent<PlayerWalkingState>();
        rollState = GetComponent<PlayerRollState>();
        dashState = GetComponent<PlayerDashState>();
        smackState = GetComponent<PlayerSmackState>();
        
        base.Awake();
        
        AddListeners();
    }

    private new void FixedUpdate()
    {
        base.FixedUpdate();
        
        var moveInput = _playerControlsActions["Move"].ReadValue<Vector2>();

        if (moveInput.x == 0) SwitchState(idleState);
        else
        {
            if (CurrentState != walkingState) SwitchState(walkingState);
            moveData.SetMoveDirection(moveInput);
        }
    }

    #region Inputs
    
    private void AddListeners()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerControlsActions = _playerInput.actions;
        
        _playerControlsActions["Roll"].performed += Roll;
        _playerControlsActions["Dash"].performed += Dash;
        _playerControlsActions["Smack"].performed += Smack;

        _playerControlsActions["DisableMovement"].performed += DisableMovement;
    }
    
    private void OnDestroy()
    {
        RemoveListeners();
    }

    public void RemoveListeners()
    {
        _playerControlsActions["Roll"].performed -= Roll;
        _playerControlsActions["Dash"].performed -= Dash;
        _playerControlsActions["Smack"].performed -= Smack;
        
        _playerControlsActions["DisableMovement"].performed -= DisableMovement;
    }

    private void Roll(InputAction.CallbackContext context) => SwitchState(rollState);
    private void Dash(InputAction.CallbackContext context) => SwitchState(dashState);
    private void Smack(InputAction.CallbackContext context) => SwitchState(smackState);
    private void DisableMovement(InputAction.CallbackContext context)
    {
        moveData.ToggleCanMove();
        SwitchState(idleState);
    }

    #endregion
}
