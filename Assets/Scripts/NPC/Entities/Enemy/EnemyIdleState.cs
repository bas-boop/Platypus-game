using System.Collections;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    [SerializeField] private float maxIdleTime = 2;
    
    public override void EnterState(EnemyStateManger enemy)
    {
        Debug.Log("IdleState");
        //todo: Idle Animation
        StartCoroutine(WaitToWalk(enemy));
    }

    public override void UpdateState(EnemyStateManger enemy) { }

    IEnumerator WaitToWalk(EnemyStateManger enemy)
    {
        var waitTime = Random.Range(1, maxIdleTime);
        yield return new WaitForSeconds(waitTime);
        enemy.SwitchState(enemy.movingState);
    }
}
