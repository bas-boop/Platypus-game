using UnityEngine;

public abstract class EnemyBaseState
{
    public abstract void EnterState(EnemyStateManger enemy);
    public abstract void UpdateState(EnemyStateManger enemy);
}
