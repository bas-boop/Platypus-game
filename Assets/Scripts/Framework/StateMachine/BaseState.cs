using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    protected StateMachineManger Parent;
    public bool IsValidToSwitch { get; protected set; }

    public void SetParent(StateMachineManger targetMachine) => Parent = targetMachine;

    public virtual void EnterState(StateMachineManger stateMachine) => IsValidToSwitch = false;
    public abstract void UpdateState(StateMachineManger stateMachine);
    public abstract void FixedUpdateState(StateMachineManger stateMachine);
    public abstract void ExitState(StateMachineManger stateMachine);
}
