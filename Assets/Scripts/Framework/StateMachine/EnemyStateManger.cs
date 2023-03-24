using UnityEngine;

public class EnemyStateManger : StateMachineManger
{
    [Header("States")]
    public EnemyIdleState idleState;
    public EnemyMovingState movingState;
    public EnemyAttackState attackState;

    [SerializeField] private EnemyBaseState[] states;

    private void Awake()
    {
        idleState = GetComponent<EnemyIdleState>();
        movingState = GetComponent<EnemyMovingState>();
        attackState = GetComponent<EnemyAttackState>();

        SetStatesParent();
        
        _currentState = idleState;
        _currentState.EnterState(this);
    }

    private void SetStatesParent()
    {
        foreach (var state in states)
        {
            state.SetParent(this);
        }
    }

    public override void SwitchState(BaseState state)
    {
        _currentState.ExitState(this);
        _currentState = state;
        state.EnterState(this);
    }
}
