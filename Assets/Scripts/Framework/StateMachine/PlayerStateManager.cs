using UnityEngine;

public class PlayerStateManager : StateMachineManager
{
    [Header("States")]
    public PlayerIdleState idleState;
    public PlayerWalkingState walkingState;
    public PlayerRollState rollState;
    public PlayerDashState dashState;
    public PlayerSmackState smackState;

    private  void Awake()
    {
        idleState = GetComponent<PlayerIdleState>();
        walkingState = GetComponent<PlayerWalkingState>();
        rollState = GetComponent<PlayerRollState>();
        dashState = GetComponent<PlayerDashState>();
        smackState = GetComponent<PlayerSmackState>();
        
        InitStateMachine();
    }
}
