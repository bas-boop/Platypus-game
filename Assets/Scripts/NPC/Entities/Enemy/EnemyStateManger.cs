using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManger : MonoBehaviour
{
    private EnemyBaseState _currentState;
    public EnemyIdleState IdleState;
    public EnemyMovingState MovingState;

    private void Awake()
    {
        IdleState = GetComponent<EnemyIdleState>();
        MovingState = GetComponent<EnemyMovingState>();
    }

    public void SwitchState(EnemyBaseState state)
    {
        _currentState = state;
        state.EnterState(this);
    }
}
