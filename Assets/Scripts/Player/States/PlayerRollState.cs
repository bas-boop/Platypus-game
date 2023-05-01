using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerMoveData))]
public class PlayerRollState : PlayerBaseState
{
    [SerializeField] private float rollPower;
    [SerializeField] private float rollTime;

    protected override void EnterState(PlayerStateManager player)
    {
        ActivateRoll(player);
    }

    protected override void UpdateState(PlayerStateManager player) { }
    protected override void FixedUpdateState(PlayerStateManager player) { }

    protected override void ExitState(PlayerStateManager player)
    {
        CancelRoll(player);
    }

    private void ActivateRoll(PlayerStateManager player)
    {
        if (!player.moveData.CanMove || player.moveData.IsRolling || !player.moveData.GroundChecker.IsGrounded || player.moveData.LastMoveDirection.x == 0)
        {
            IsValidToSwitch = true;
            player.SwitchState(PlayerState.Idle);
            return;
        }

        StartCoroutine(Roll(player.moveData.LastMoveDirection.x, player));
    }

    private void CancelRoll(PlayerStateManager player)
    {
        StopCoroutine(Roll(player.moveData.LastMoveDirection.x, player));
        player.moveData.IsRolling = false;
    }
    
    private IEnumerator Roll(float rollDirection, PlayerStateManager player)
    {
        player.moveData.ToggleCanMove();
        player.moveData.Animator.SetBool("IsRolling", true);
        
        yield return new WaitForSeconds(0.2f);
        
        var rollForce = Vector2.zero;
        rollForce.x = rollDirection * rollPower;
        
        var timer = rollTime;
        while (timer > 0)
        {
            player.moveData.IsRolling = true;
            player.moveData.Rigidbody.velocity = rollForce;

            timer -= Time.deltaTime;
        }

        yield return new WaitForSeconds(rollTime);
        
        player.moveData.IsRolling = false;
        if (!player.moveData.IsDashing) player.moveData.Rigidbody.velocity = Vector2.zero;
        player.moveData.Animator.SetBool("IsRolling", false);
        player.moveData.ToggleCanMove();
        IsValidToSwitch = true;

        yield return null;
    }
}
