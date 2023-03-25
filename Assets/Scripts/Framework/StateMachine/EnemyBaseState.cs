using UnityEngine;

[RequireComponent(typeof(EnemyStateManger))]
public abstract class EnemyBaseState : BaseState
{
    #region BaseState to EnemyState

    public override void EnterState(StateMachineManger entity) => EnterState((EnemyStateManger)Parent);
    public override void UpdateState(StateMachineManger entity) => UpdateState((EnemyStateManger)Parent);
    public override void FixedUpdateState(StateMachineManger entity) => FixedUpdateState((EnemyStateManger)Parent);
    public override void ExitState(StateMachineManger entity) => ExitState((EnemyStateManger)Parent);

    #endregion

    
    #region Functions called by state's

    protected abstract void EnterState(EnemyStateManger enemy);
    protected abstract void UpdateState(EnemyStateManger enemy);
    protected abstract void FixedUpdateState(EnemyStateManger enemy);
    protected abstract void ExitState(EnemyStateManger enemy);

    #endregion
}
