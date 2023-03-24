using UnityEngine;

[RequireComponent(typeof(EnemyStateManger))]
public abstract class EnemyBaseState : BaseState
{
    public abstract void UpdateState(EnemyStateManger entity);
    public abstract void ExitState(EnemyStateManger entity);
    public abstract void EnterState(EnemyStateManger entity);
    
    /*public abstract override void UpdateState(StateMachineManger entity);
    public abstract override void ExitState(StateMachineManger entity);
    public abstract override void EnterState(StateMachineManger entity);*/
}
