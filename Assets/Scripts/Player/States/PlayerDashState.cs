using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerMoveData))]
public class PlayerDashState : PlayerBaseState
{
    [Header("Value")]
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

    public void ActivateDash(PlayerStateManager player)
    {
        if (!player.moveData.CanDash) return;
        if(player.moveData.IsDashing || !player.moveData.GroundChecker.IsGrounded) return;
        
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
        
        yield return null;
    }

    private void Dash(PlayerStateManager player)
    {
        var currentPos = new Vector2(transform.position.x, transform.position.y);
        var dashDirection = player.moveData.MouseWorldPosition - currentPos;

        if(dashDirection.y < minY) return;
        if(dashDirection.y < longDistance.y && Mathf.Abs(dashDirection.x) > longDistance.x) return;
        
        player.moveData.Rigidbody.AddForce(dashDirection * dashForcePower, ForceMode2D.Impulse);
    }
}
