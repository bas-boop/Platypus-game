using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    protected StateMachineManager Parent;
    public bool IsValidToSwitch { get; protected set; }

    public void SetParent(StateMachineManager targetMachine) => Parent = targetMachine;

    public virtual void EnterState(StateMachineManager stateMachine) => IsValidToSwitch = false;
    public abstract void UpdateState(StateMachineManager stateMachine);
    public abstract void FixedUpdateState(StateMachineManager stateMachine);
    public abstract void ExitState(StateMachineManager stateMachine);
}
