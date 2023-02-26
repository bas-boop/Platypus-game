using UnityEngine;

public class EnemyStateManger : MonoBehaviour
{
    private EnemyBaseState _currentState;
    
    [Header("States")]
    public EnemyIdleState idleState;
    public EnemyMovingState movingState;
    public EnemyAttackState attackState;

    private void Awake()
    {
        idleState = GetComponent<EnemyIdleState>();
        movingState = GetComponent<EnemyMovingState>();
        attackState = GetComponent<EnemyAttackState>();

        _currentState = idleState;
        _currentState.EnterState(this);
    }

    private void Update()
    {
        _currentState.UpdateState(this);
    }

    public void SwitchState(EnemyBaseState state)
    {
        _currentState.ExitState(this);
        _currentState = state;
        state.EnterState(this);
    }
}
