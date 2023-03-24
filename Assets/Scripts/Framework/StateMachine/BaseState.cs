using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    protected StateMachineManger Parent;
    
    public void SetParent(StateMachineManger targetMachine) => Parent = targetMachine;
    
    public abstract void EnterState(StateMachineManger entity);
    public abstract void UpdateState(StateMachineManger entity);
    public abstract void ExitState(StateMachineManger entity);
}
