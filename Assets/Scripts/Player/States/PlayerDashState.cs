using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMoveData))]
public class PlayerDashState : PlayerBaseState
{
    private PlayerInput _playerInput;
    private InputActionAsset _playerControlsActions;
    
    private Vector2 _mouseWorldPosition;

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

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerControlsActions = _playerInput.actions;
    }

    public void ActivateDash(PlayerStateManager player)
    {
        if (!player.moveData.CanDash) return;
        if(player.moveData.IsDashing || !player.moveData.GroundChecker.IsGrounded) return;

        _mouseWorldPosition = SetMousePos();
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
        var dashDirection = _mouseWorldPosition - currentPos;

        if(dashDirection.y < minY) return;
        if(dashDirection.y < longDistance.y && Mathf.Abs(dashDirection.x) > longDistance.x) return;
        
        player.moveData.Rigidbody.AddForce(dashDirection * dashForcePower, ForceMode2D.Impulse);
    }

    private Vector2 SetMousePos()
    {
        var mousePos = _playerControlsActions["MousePosition"].ReadValue<Vector2>();
        return Camera.main.ScreenToWorldPoint(new Vector2(mousePos.x, mousePos.y));
    }

    private Vector2 SetDashDirection() => _playerControlsActions["Move"].ReadValue<Vector2>();
}
