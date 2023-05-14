using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(PlayerMoveData))]
public class PlayerDashState : PlayerBaseState
{
    [Header("Value")]
    [SerializeField] private float maxDashBound;
    [SerializeField] private float dashForcePower;
    [SerializeField] private float dashTime;

    [Header("threshold's")]
    [SerializeField] private float minY;
    [SerializeField] private Vector2 longDistance;
    
    protected override void EnterState(PlayerStateManager player)
    {
        ActivateDash(player);
    }

    protected override void UpdateState(PlayerStateManager player) { }
    protected override void FixedUpdateState(PlayerStateManager player) { }
    protected override void ExitState(PlayerStateManager player) { }

    private void ActivateDash(PlayerStateManager player)
    {
        if (!player.moveData.CanMove || player.moveData.IsDashing || !player.moveData.GroundChecker.IsGrounded)
        {
            IsValidToSwitch = true;
            player.SwitchState(PlayerState.Idle);
            return;
        }
        
        StartCoroutine(StartDash(player));
    }
    
    private IEnumerator StartDash(PlayerStateManager player)
    {
        player.moveData.IsDashing = true;
        player.moveData.Animator.SetBool("IsDashing", true);
        
        yield return new WaitForSeconds(0.4f);
        
        Dash(player);
        
        yield return new WaitForSeconds(dashTime);

        IsValidToSwitch = true;
        player.moveData.IsDashing = false;
        player.moveData.Animator.SetBool("IsDashing", false);
        
        player.SwitchState(PlayerState.Falling);
        
        yield return null;
    }

    private void Dash(PlayerStateManager player)
    {
        // DashDirection(player.moveData.MouseWorldPosition);
        
        // if(dashDirection.y < longDistance.y && Mathf.Abs(dashDirection.x) > longDistance.x || dashDirection.y < minY) return; //todo: Failed dash state

        Debug.Log(player.moveData.MouseWorldPosition + " " + player.moveData.MouseWorldPosition.magnitude);
        
        if (player.moveData.MouseWorldPosition.magnitude < maxDashBound)
        {
            Debug.Log("In");
            player.moveData.Rigidbody.AddForce(DashDirection(player.moveData.MouseWorldPosition) * 3f, ForceMode2D.Impulse);
        }
        else
        {
            Debug.Log("Out");
            player.moveData.Rigidbody.AddForce(DashDirection(player.moveData.MouseWorldPosition).normalized * dashForcePower, ForceMode2D.Impulse);
        }
        
    }

    private Vector2 DashDirection(Vector2 mouseWorldPos)
    {
        var currentPos = (Vector2)transform.position;
        return mouseWorldPos - currentPos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxDashBound);
    }
}
