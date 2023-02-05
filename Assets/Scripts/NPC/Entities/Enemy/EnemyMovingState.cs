using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovingState : EnemyBaseState
{
    private GroundChecker _groundChecker;
    
    [SerializeField] private float speed;
    
    [SerializeField] [Range(0,10)] private float minDistanceCanWalk;
    [SerializeField] [Range(1,20)] private float maxDistanceCanWalk;
    
    public override void EnterState(EnemyStateManger enemy)
    {
        //Moving Animation
        Walk(GetWalkPoint());
    }

    public override void UpdateState(EnemyStateManger enemy)
    {
        
    }

    private void Awake()
    {
        _groundChecker = GetComponent<GroundChecker>();
    }

    private Vector2 GetWalkPoint()
    {
        var desiredPosition = new Vector2();
        
        //logica
        
        return desiredPosition;
    }

    private void Walk(Vector2 desiredPosition)
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer != _groundChecker.ThisIsGround) return;
        
        //zet GetWalkPoint() tegen de muur
        //of laat GetWalkPoint() dat zelf al doen
    }
}
