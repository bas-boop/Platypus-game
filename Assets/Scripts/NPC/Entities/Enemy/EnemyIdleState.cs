using System.Collections;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    [SerializeField] [Range(0, 30)] private float minIdleTime = 0.1f;
    [SerializeField] [Range(1, 60)] private float maxIdleTime = 2;
    
    public override void EnterState(EnemyStateManger enemy)
    {
        Debug.Log("IdleState");
        //todo: Idle Animation
        StartCoroutine(WaitToWalk(enemy));
    }

    public override void UpdateState(EnemyStateManger enemy) { }
    
    public override void ExitState(EnemyStateManger enemy)
    {
        Debug.Log("Exiting IdleState");
    }

    IEnumerator WaitToWalk(EnemyStateManger enemy)
    {
        var waitTime = Random.Range(minIdleTime, maxIdleTime);
        yield return new WaitForSeconds(waitTime);
        enemy.SwitchState(enemy.movingState);
    }
}
