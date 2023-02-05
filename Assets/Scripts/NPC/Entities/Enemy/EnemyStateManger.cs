using UnityEngine;

public class EnemyStateManger : MonoBehaviour
{
    private EnemyBaseState _currentState;
    
    [Header("States")]
    public EnemyIdleState idleState;
    public EnemyMovingState movingState;

    private void Awake()
    {
        idleState = GetComponent<EnemyIdleState>();
        movingState = GetComponent<EnemyMovingState>();

        _currentState = idleState;
        _currentState.EnterState(this);
    }

    private void Update()
    {
        _currentState.UpdateState(this);
    }

    public void SwitchState(EnemyBaseState state)
    {
        _currentState = state;
        state.EnterState(this);
    }
}
