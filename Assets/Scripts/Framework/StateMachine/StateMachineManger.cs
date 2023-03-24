using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineManger : MonoBehaviour
{
    private EnemyBaseState _currentState;
    
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
