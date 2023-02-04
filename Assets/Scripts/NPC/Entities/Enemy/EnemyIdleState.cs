using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    [SerializeField] private bool isOn;

    public override void EnterState(EnemyStateManger enemy)
    {
        Debug.Log("Idle mode activated!");
    }

    public override void UpdateState(EnemyStateManger enemy)
    {
        
    }
}
