using UnityEngine;

public class EnemyStateManger : StateMachineManger
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

    
}
