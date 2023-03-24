using UnityEngine;

[RequireComponent(typeof(EnemyStateManger))]
public abstract class EnemyBaseState : BaseState
{
    private EnemyStateManger _enemyStateManger;

    public void SetParent(EnemyStateManger enemyStateManger) => _enemyStateManger = enemyStateManger;

    #region BaseState to EnemyState

    public override void EnterState(StateMachineManger entity) => EnterState(_enemyStateManger);

    public override void UpdateState(StateMachineManger entity) => UpdateState(_enemyStateManger);

    public override void ExitState(StateMachineManger entity) => ExitState(_enemyStateManger);

    #endregion

    #region Functions called by state's

    protected abstract void EnterState(EnemyStateManger enemy);

    protected abstract void UpdateState(EnemyStateManger enemy);

    protected abstract void ExitState(EnemyStateManger enemy);

    #endregion
}
