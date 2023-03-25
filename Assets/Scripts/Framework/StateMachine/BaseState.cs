using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    protected StateMachineManger Parent;

    public void SetParent(StateMachineManger targetMachine) => Parent = targetMachine;
    
    public abstract void EnterState(StateMachineManger stateMachine);
    public abstract void UpdateState(StateMachineManger stateMachine);
    public abstract void FixedUpdateState(StateMachineManger stateMachine);
    public abstract void ExitState(StateMachineManger stateMachine);
}
