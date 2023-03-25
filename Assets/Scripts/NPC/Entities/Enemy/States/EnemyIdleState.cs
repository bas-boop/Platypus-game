using System.Collections;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    [SerializeField] [Range(0, 30)] private float minIdleTime = 0.1f;
    [SerializeField] [Range(1, 60)] private float maxIdleTime = 2;

    private bool _wasMoving;
    
    protected override void EnterState(EnemyStateManger enemy)
    {
        //todo: Idle Animation
        
        StartCoroutine(WaitToWalk(enemy));
    }

    protected override void UpdateState(EnemyStateManger enemy) { }
    protected override void FixedUpdateState(EnemyStateManger enemy) { }
    protected override void ExitState(EnemyStateManger enemy) { }

    private IEnumerator WaitToWalk(EnemyStateManger enemy)
    {
        var waitTime = Random.Range(minIdleTime, maxIdleTime);
        yield return new WaitForSeconds(waitTime);

        IsValidToSwitch = true;
        
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
}
