using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    public abstract void EnterState(StateMachineManger entity);
    public abstract void UpdateState(StateMachineManger entity);
    public abstract void ExitState(StateMachineManger entity);
}
