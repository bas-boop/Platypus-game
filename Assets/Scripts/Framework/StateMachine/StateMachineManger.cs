using UnityEngine;

public abstract class StateMachineManger : MonoBehaviour
{
    protected static BaseState _currentState;

    private void Update()
    {
        _currentState.UpdateState(this);
    }

    public abstract void SwitchState(BaseState state);
}
