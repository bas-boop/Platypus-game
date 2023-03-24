using UnityEngine;

public abstract class StateMachineManger : MonoBehaviour
{
    [SerializeField] protected BaseState startingState;
    
    protected static BaseState CurrentState;

    private void Update()
    {
        CurrentState.UpdateState(this);
    }

    public abstract void SwitchState(BaseState state);
}
