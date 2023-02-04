using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovingState : EnemyBaseState
{
    public int ja = 69;
    
    [Header("Kiekeboe")]
    [SerializeField] [Range(0,1)] private float slider;

    [Header("Iets")]
    [SerializeField] private Vector2 pos;
    
    public override void EnterState(EnemyStateManger enemy)
    {
        
    }

    public override void UpdateState(EnemyStateManger enemy)
    {
        
    }
}
