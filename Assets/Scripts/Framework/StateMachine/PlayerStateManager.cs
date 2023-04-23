using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateManager : StateMachineManager
{
    private PlayerInput _playerInput;
    private InputActionAsset _playerControlsActions;

    public PlayerMoveData moveData;
    
    [HideInInspector] public PlayerIdleState idleState;
    [HideInInspector] public PlayerWalkingState walkingState;
    [HideInInspector] public PlayerRollState rollState;
    [HideInInspector] public PlayerDashState dashState;
    [HideInInspector] public PlayerSmackState smackState;

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

        if (moveInput.x != 0)
        {
            if(CurrentState != walkingState) SwitchState(walkingState);
            moveData.SetMoveDirection(moveInput);
        }
        else if(CurrentState != idleState) SwitchState(idleState);
    }

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

    private void Roll(InputAction.CallbackContext context) => SwitchState(rollState);
    private void Dash(InputAction.CallbackContext context)
    {
        moveData.MouseWorldPosition = SetMousePos();
        SwitchState(dashState);
    }

    private void Smack(InputAction.CallbackContext context) => SwitchState(smackState);
    private void DisableMovement(InputAction.CallbackContext context)
    {
        moveData.ToggleCanMove();
        SwitchState(idleState);
    }
    
    private void Remove(InputAction.CallbackContext context) => PickupSystem.Instance.RemovePickup("Stick");

    #endregion
}
