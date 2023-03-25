using UnityEngine;

public class EnemyStateManger : StateMachineManger
{
    [Header("States")]
    public EnemyIdleState idleState;
    public EnemyMovingState movingState;
    public EnemyAttackState attackState;

    private  void Awake()
    {
        idleState = GetComponent<EnemyIdleState>();
        movingState = GetComponent<EnemyMovingState>();
        attackState = GetComponent<EnemyAttackState>();
        
        InitStateMachine();
    }
}
