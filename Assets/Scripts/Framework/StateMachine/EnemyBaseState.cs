using UnityEngine;

[RequireComponent(typeof(EnemyStateManager))]
public abstract class EnemyBaseState : BaseState
{
    #region BaseState to EnemyState

    public override void EnterState(StateMachineManager entity)
    {
        base.EnterState(Parent);
        EnterState((EnemyStateManager) Parent);
    }

    public override void UpdateState(StateMachineManager entity) => UpdateState((EnemyStateManager)Parent);
    public override void FixedUpdateState(StateMachineManager entity) => FixedUpdateState((EnemyStateManager)Parent);
    public override void ExitState(StateMachineManager entity) => ExitState((EnemyStateManager)Parent);

    #endregion

    
    #region Functions called by state's

    protected abstract void EnterState(EnemyStateManager enemy);
    protected abstract void UpdateState(EnemyStateManager enemy);
    protected abstract void FixedUpdateState(EnemyStateManager enemy);
    protected abstract void ExitState(EnemyStateManager enemy);

    #endregion
}
