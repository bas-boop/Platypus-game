using UnityEngine;

public abstract class StateMachineManager : MonoBehaviour
{
    [Header("Base StateMachine")]
    [SerializeField] protected BaseState startingState;
    [SerializeField] protected BaseState[] states;
    
    protected BaseState CurrentState;

    protected void Awake()
    {
        InitStateMachine();
    }

    protected void Update()
    {
        CurrentState.UpdateState(this);
    }
    protected void FixedUpdate()
    {
        CurrentState.FixedUpdateState(this);
    }
    
    private void InitStateMachine()
    {
        SetStatesParent();
        EnterStartingState();
    }

    private void SetStatesParent()
    {
        foreach (var state in states)
        {
            state.SetParent(this);
        }
    }

    private void EnterStartingState()
    {
        CurrentState = startingState;
        CurrentState.EnterState(this);
    }

    /// <summary>
    /// Switch the current targetState to a different one.
    /// Is it valid to switch targetState?
    /// </summary>
    /// <param name="targetState">Give target state to switch into.</param>
    /// <param name="isCalledFormExitState">If the SwitchState is called form a ExitState function, this needs to be true!</param>
    public void SwitchState(BaseState targetState, bool isCalledFormExitState = false)
    {
        if (!CurrentState.IsValidToSwitch)
        {
            Debug.LogWarning("Switching targetState was not valid!!!\n" + CurrentState);
            return;
        }
        
        if (!isCalledFormExitState) CurrentState.ExitState(this);
        CurrentState = targetState;
        targetState.EnterState(this);
    }
}
