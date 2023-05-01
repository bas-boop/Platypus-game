using UnityEngine;

public class PlayerFallingState : PlayerBaseState
{
    [SerializeField] [Range(0, -20)] private float greaterGravity;
    
    protected override void EnterState(PlayerStateManager player)
    {
        player.moveData.Animator.SetBool("Isn'tGrounded", true);
        player.moveData.Gravity = greaterGravity;
        IsValidToSwitch = true;
    }

    protected override void UpdateState(PlayerStateManager player) { }

    protected override void FixedUpdateState(PlayerStateManager player) { }

    protected override void ExitState(PlayerStateManager player)
    {
        player.moveData.Animator.SetBool("Isn'tGrounded", false);
    }
}
