using UnityEngine;

[RequireComponent(typeof(EnemyStateManger))]
public abstract class EnemyBaseState : MonoBehaviour
{
    public abstract void EnterState(EnemyStateManger enemy);
    public abstract void UpdateState(EnemyStateManger enemy);
    public abstract void ExitState(EnemyStateManger enemy);
}
