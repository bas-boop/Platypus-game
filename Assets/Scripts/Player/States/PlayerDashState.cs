using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerMoveData))]
public class PlayerDashState : PlayerBaseState
{
    [Header("Value")]
    [SerializeField] private float maxDashBound;
    [SerializeField] private float fullDashForcePower;
    [SerializeField] private float dashForcePower;
    [SerializeField] private float dashTime;

    [Header("threshold's")]
    [SerializeField] private float minY;
    [SerializeField] private Vector2 longDistance;

    [Header("Debug")]
    [SerializeField] private bool showGizmos;
    [SerializeField] private Color gizmosColor = Color.green;

    private const float PreDashAnimationTime = 0.2f;
    
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
        if (DashDirection(player.moveData.MouseWorldPosition).y < 0)
        {
            player.moveData.WasDashing = false;
            IsValidToSwitch = true;
            player.SwitchState(PlayerState.Idle);
            yield break;
        }
    
        player.moveData.IsDashing = true;
        player.moveData.Animator.SetBool("IsDashing", true);
        
        yield return new WaitForSeconds(PreDashAnimationTime);
        
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
        var dashDirection = DashDirection(player.moveData.MouseWorldPosition);

        if (dashDirection.magnitude < maxDashBound) player.moveData.Rigidbody.AddForce(dashDirection * dashForcePower, ForceMode2D.Impulse);
        else player.moveData.Rigidbody.AddForce(dashDirection.normalized * fullDashForcePower, ForceMode2D.Impulse);

        player.moveData.SetMoveDirection(dashDirection);
        player.moveData.WasDashing = true;
    }

    private Vector2 DashDirection(Vector2 mouseWorldPos)
    {
        var currentPos = (Vector2)transform.position;
        return mouseWorldPos - currentPos;
    }

    private void OnDrawGizmos()
    {
        if(!showGizmos) return;

        Gizmos.color = gizmosColor;
        Gizmos.DrawWireSphere(transform.position, maxDashBound);
    }
}
