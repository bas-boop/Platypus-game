using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateManager : StateMachineManager
{
    private PlayerInput _playerInput;
    private InputActionAsset _playerControlsActions;

    public PlayerMoveData moveData;
    [SerializeField] private PlayerState currentState;
    
    [HideInInspector] public PlayerIdleState idleState;
    [HideInInspector] public PlayerWalkingState walkingState;
    [HideInInspector] public PlayerRollState rollState;
    [HideInInspector] public PlayerDashState dashState;
    [HideInInspector] public PlayerSmackState smackState;
    
    // private BaseState[] _switchStateQueue;
    private List<BaseState> _switchStateQueue = new List<BaseState>();

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
        
        if (!CurrentState.IsValidToSwitch) return;

        var moveInput = _playerControlsActions["Move"].ReadValue<Vector2>();
        
        if (moveInput.x != 0)
        {
            if(currentState != PlayerState.Walking) SwitchState(PlayerState.Walking);
            moveData.SetMoveDirection(moveInput);
        }
        else if (currentState != PlayerState.Idle) SwitchState(PlayerState.Idle);
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
            PlayerState.Idle => idleState,
            PlayerState.Walking => walkingState,
            PlayerState.Dashing => dashState,
            PlayerState.Rolling => rollState,
            PlayerState.Smacking => smackState,
            _ => startingState
        };
        
        if(!CurrentState.IsValidToSwitch) StartCoroutine(AddStateInQueue(state));

        base.SwitchState(state);
        currentState = targetState;
    }
    
    private IEnumerator AddStateInQueue(BaseState queueAbleState)
    {
        _switchStateQueue.Add(queueAbleState);

        yield return new WaitUntil(() => CurrentState.IsValidToSwitch);

        SwitchState(_switchStateQueue[0]);
        _switchStateQueue.Remove(_switchStateQueue[0]);
    }

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
