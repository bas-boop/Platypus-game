using UnityEngine;

[RequireComponent(typeof(PlayerStateManager))]
public abstract class PlayerBaseState : BaseState
{
    #region BaseState to EnemyState

    public override void EnterState(StateMachineManager entity)
    {
        base.EnterState(Parent);
        EnterState((PlayerStateManager) Parent);
    }

    public override void UpdateState(StateMachineManager entity) => UpdateState((PlayerStateManager)Parent);
    public override void FixedUpdateState(StateMachineManager entity) => FixedUpdateState((PlayerStateManager)Parent);
    public override void ExitState(StateMachineManager entity) => ExitState((PlayerStateManager)Parent);

    #endregion

    
    #region Functions called by state's

    protected abstract void EnterState(PlayerStateManager player);
    protected abstract void UpdateState(PlayerStateManager player);
    protected abstract void FixedUpdateState(PlayerStateManager player);
    protected abstract void ExitState(PlayerStateManager player);

    #endregion
}
