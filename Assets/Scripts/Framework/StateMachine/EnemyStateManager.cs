using UnityEngine;

public class EnemyStateManager : StateMachineManager
{
    [Header("States")]
    public EnemyIdleState idleState;
    public EnemyMovingState movingState;
    public EnemyAttackState attackState;

    private new void Awake()
    {
        idleState = GetComponent<EnemyIdleState>();
        movingState = GetComponent<EnemyMovingState>();
        attackState = GetComponent<EnemyAttackState>();
        
        base.Awake();
    }
}
