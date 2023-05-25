using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateManager : StateMachineManager
{
    [Header("Player State Manager")]
    public PlayerMoveData moveData;

    private PlayerInput _playerInput;
    private InputActionAsset _playerControlsActions;
    
    private PlayerState _currentPlayerState;
    public PlayerState PreviousPlayState { get; private set; }

    private PlayerIdleState _idleState;
    private PlayerWalkingState _walkingState;
    private PlayerRollState _rollState;
    private PlayerDashState _dashState;
    private PlayerSmackState _smackState;
    private PlayerFallingState _fallingState;

    private new void Awake()
    {
        _idleState = GetComponent<PlayerIdleState>();
        _walkingState = GetComponent<PlayerWalkingState>();
        _rollState = GetComponent<PlayerRollState>();
        _dashState = GetComponent<PlayerDashState>();
        _smackState = GetComponent<PlayerSmackState>();
        _fallingState = GetComponent<PlayerFallingState>();
        
        base.Awake();
        
        AddListeners();
    }

    private new void FixedUpdate()
    {
        base.FixedUpdate();
        
        if (!currentState.IsValidToSwitch) return;

        var moveInput = _playerControlsActions["Move"].ReadValue<Vector2>();
        
        if (moveInput.x != 0)
        {
            if(_currentPlayerState != PlayerState.Walking) SwitchState(PlayerState.Walking);
            moveData.SetMoveDirection(moveInput);
        }
        else if (_currentPlayerState != PlayerState.Idle && moveData.GroundChecker.IsGrounded) SwitchState(PlayerState.Idle);
    }

    #region Switch State

    /// <summary>
    /// Switch the current targetState to a different one.
    /// Is it valid to switch targetState?
    /// </summary>
    /// <param name="targetState">Give target state to switch into.</param>
    public void SwitchState(PlayerState targetState)
    {
        var state = targetState switch
        {
            PlayerState.Idle => _idleState,
            PlayerState.Walking => _walkingState,
            PlayerState.Dashing => _dashState,
            PlayerState.Rolling => _rollState,
            PlayerState.Smacking => _smackState,
            PlayerState.Falling => _fallingState,
            _ => startingState
        };
        
        base.SwitchState(state);
        if (currentState != state) return;
        
        PreviousPlayState = _currentPlayerState;
        _currentPlayerState = targetState;
    }
    
    /// <summary>
    /// This function is used for Unity Events, because they can not have an Enum as parameter.
    /// </summary>
    /// <param name="enumValue">The PlayerState in int variable.</param>
    public void SwitchStateEvent(int enumValue) => SwitchState((PlayerState)enumValue);

    #endregion

    #region Inputs
    
    public Vector2 SetMousePos()
    {
        var mousePos = _playerControlsActions["MousePosition"].ReadValue<Vector2>();
        return Camera.main.ScreenToWorldPoint(new Vector2(mousePos.x, mousePos.y));
    }
    
    private void AddListeners()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerControlsActions = _playerInput.actions;
        
        _playerControlsActions["Roll"].performed += Roll;
        _playerControlsActions["Dash"].performed += Dash;
        _playerControlsActions["Smack"].performed += Smack;

        _playerControlsActions["DisableMovement"].performed += DisableMovement;

        _playerControlsActions["Temp-remove-pickup"].performed += Remove;
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
        
        _playerControlsActions["Temp-remove-pickup"].performed -= Remove;
    }

    private void Roll(InputAction.CallbackContext context) => SwitchState(PlayerState.Rolling);
    private void Dash(InputAction.CallbackContext context)
    {
        moveData.MouseWorldPosition = SetMousePos();
        SwitchState(PlayerState.Dashing);
    }

    private void Smack(InputAction.CallbackContext context) => SwitchState(PlayerState.Smacking);
    private void DisableMovement(InputAction.CallbackContext context)
    {
        moveData.ToggleCanMove();
        SwitchState(PlayerState.Idle);
    }
    
    private void Remove(InputAction.CallbackContext context) => PickupSystem.Instance.RemovePickup(PickupType.Stick);

    #endregion
}
