using UnityEngine;

[RequireComponent(typeof(EnemyStateManger))]
public abstract class EnemyBaseState : BaseState
{
    #region BaseState to EnemyState

    public override void EnterState(StateMachineManger entity) => EnterState((EnemyStateManger)_parent);
    public override void UpdateState(StateMachineManger entity) => UpdateState((EnemyStateManger)_parent);
    public override void ExitState(StateMachineManger entity) => ExitState((EnemyStateManger)_parent);

    #endregion

    
    #region Functions called by state's

    protected abstract void EnterState(EnemyStateManger enemy);
    protected abstract void UpdateState(EnemyStateManger enemy);
    protected abstract void ExitState(EnemyStateManger enemy);

    #endregion
}
