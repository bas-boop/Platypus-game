using UnityEngine;

public class EnemyStateManager : StateMachineManager
{
    [HideInInspector] public EnemyIdleState idleState;
    [HideInInspector] public EnemyMovingState movingState;
    [HideInInspector] public EnemyAttackState attackState;

    private new void Awake()
    {
        idleState = GetComponent<EnemyIdleState>();
        movingState = GetComponent<EnemyMovingState>();
        attackState = GetComponent<EnemyAttackState>();
        
        base.Awake();
    }
}
