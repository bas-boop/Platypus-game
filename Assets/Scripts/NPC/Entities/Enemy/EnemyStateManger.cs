using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManger : MonoBehaviour
{
    private EnemyBaseState _currentState;
    public EnemyIdleState IdleState = new EnemyIdleState();

    public void SwitchState(EnemyBaseState state)
    {
        _currentState = state;
        state.EnterState(this);
    }
}
