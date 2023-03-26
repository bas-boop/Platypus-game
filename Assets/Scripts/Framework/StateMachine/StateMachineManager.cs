using UnityEngine;

public abstract class StateMachineManager : MonoBehaviour
{
    [Header("Base StateMachine")]
    [SerializeField] protected BaseState startingState;
    [SerializeField] protected BaseState[] states;
    
    private static BaseState _currentState;

    private void Update() => _currentState.UpdateState(this);
    private void FixedUpdate() => _currentState.FixedUpdateState(this);

    /// <summary>
    /// Initialization of the state machine
    /// </summary>
    /// <list type="Functions">
    ///     <listheader>
    ///         <term>Functions</term>
    ///         <description>description</description>
    ///     </listheader>
    ///     <item>
    ///         <term>SetStatesParent</term>
    ///         <description>Sets the state parent, that's a state machine.</description>
    ///     </item>
    ///     <item>
    ///         <term>EnterStartingState</term>
    ///         <description>Enters the starting state.</description>
    ///     </item>
    /// </list>>
    protected void InitStateMachine()
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
        _currentState = startingState;
        _currentState.EnterState(this);
    }

    /// <summary>
    /// Switch the current state to a different one.
    /// Is it valid to switch state?
    /// </summary>
    /// <param name="state">Target state to switch into.</param>
    public void SwitchState(BaseState state)
    {
        if (!_currentState.IsValidToSwitch)
        {
            Debug.LogWarning("Switching state was not valid!!!");
            return;
        }
        
        _currentState.ExitState(this);
        _currentState = state;
        state.EnterState(this);
    }
}
