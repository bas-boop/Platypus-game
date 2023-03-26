using UnityEngine;

public class PlayerStateManager : StateMachineManager
{
    [Header("States")]
    public PlayerBaseState idleState;

    private  void Awake()
    {
        
        
        InitStateMachine();
    }
}
