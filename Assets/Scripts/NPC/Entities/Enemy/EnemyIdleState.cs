using System.Collections;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    [SerializeField] private float maxIdleTime;
    
    public override void EnterState(EnemyStateManger enemy)
    {
        //Idle Animation
        StartCoroutine(WaitToWalk(enemy));
    }

    public override void UpdateState(EnemyStateManger enemy) { }

    IEnumerator WaitToWalk(EnemyStateManger enemy)
    {
        var waitTime = Random.Range(0, maxIdleTime);
        yield return new WaitForSeconds(waitTime);
        enemy.SwitchState(enemy.movingState);
    }
}
