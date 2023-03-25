using UnityEngine;

public abstract class StateMachineManger : MonoBehaviour
{
    [Header("Base StateMachine")]
    [SerializeField] protected BaseState startingState;
    [SerializeField] protected BaseState[] states;
    
    private static BaseState _currentState;

    private void Update() => _currentState.UpdateState(this);
    private void FixedUpdate() => _currentState.FixedUpdateState(this);

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

    public void SwitchState(BaseState state)
    {
        _currentState.ExitState(this);
        _currentState = state;
        state.EnterState(this);
    }
}
