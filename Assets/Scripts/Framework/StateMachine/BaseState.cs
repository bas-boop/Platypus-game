using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    protected StateMachineManger _parent;
    
    public void SetParent(StateMachineManger targetMachine) => _parent = targetMachine;
    
    public abstract void EnterState(StateMachineManger entity);
    public abstract void UpdateState(StateMachineManger entity);
    public abstract void ExitState(StateMachineManger entity);
}
