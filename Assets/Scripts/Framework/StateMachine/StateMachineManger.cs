using UnityEngine;

public abstract class StateMachineManger : MonoBehaviour
{
    [SerializeField] public BaseState startingState;
    
    protected static BaseState CurrentState;

    private void Update()
    {
        CurrentState.UpdateState(this);
    }

    public abstract void SwitchState(BaseState state);
}
