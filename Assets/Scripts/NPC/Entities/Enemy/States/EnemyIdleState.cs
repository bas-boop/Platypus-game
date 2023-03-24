using System.Collections;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    [SerializeField] [Range(0, 30)] private float minIdleTime = 0.1f;
    [SerializeField] [Range(1, 60)] private float maxIdleTime = 2;

    private bool _wasMoving;
    
    public override void EnterState(EnemyStateManger enemy)
    {
        //todo: Idle Animation
        
        StartCoroutine(WaitToWalk(enemy));
    }

    public override void UpdateState(EnemyStateManger enemy) { }
    
    public override void ExitState(EnemyStateManger enemy) { }

    IEnumerator WaitToWalk(EnemyStateManger enemy)
    {
        var waitTime = Random.Range(minIdleTime, maxIdleTime);
        yield return new WaitForSeconds(waitTime);

        if (!_wasMoving)
        {
            enemy.SwitchState(enemy.movingState);
            _wasMoving = true;
        }
        else
        {
            enemy.SwitchState(enemy.attackState);
            _wasMoving = false;
        }
    }

    public override void EnterState(StateMachineManger entity)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState(StateMachineManger entity)
    {
        throw new System.NotImplementedException();
    }

    public override void ExitState(StateMachineManger entity)
    {
        throw new System.NotImplementedException();
    }
}
