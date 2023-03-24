using UnityEngine;

public class EnemyStateManger : StateMachineManger
{
    [Header("States")]
    public EnemyIdleState idleState;
    public EnemyMovingState movingState;
    public EnemyAttackState attackState;

    [SerializeField] private BaseState[] states;

    private void Awake()
    {
        idleState = GetComponent<EnemyIdleState>();
        movingState = GetComponent<EnemyMovingState>();
        attackState = GetComponent<EnemyAttackState>();

        SetStatesParent();
        
        CurrentState = startingState;
        CurrentState.EnterState(this);
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
        CurrentState.ExitState(this);
        CurrentState = state;
        state.EnterState(this);
    }
}
