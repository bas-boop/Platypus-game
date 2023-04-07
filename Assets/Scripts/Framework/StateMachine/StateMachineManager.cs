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
    /// Switch the current state to a different one.
    /// Is it valid to switch state?
    /// </summary>
    /// <param name="state">Target state to switch into.</param>
    public void SwitchState(BaseState state)
    {
        if (!CurrentState.IsValidToSwitch)
        {
            Debug.LogWarning("Switching state was not valid!!!");
            return;
        }
        
        CurrentState.ExitState(this);
        CurrentState = state;
        state.EnterState(this);
    }
}
