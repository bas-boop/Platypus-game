using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GroundChecker))]
public class DashAbillity : MonoBehaviour
{
    [Header("Refrence's")]
    [SerializeField] private Animator animator;
    private Rigidbody2D _rb;
    private GroundChecker _gc;
    private PlayerInput _playerInput;
    private InputActionAsset _playerControlsActions;
    
    private Vector2 _mouseWorldPosition;

    private bool _canDash = true;
    private bool _isDashing;

    [Header("Value")]
    [SerializeField] private float dashForcePower;
    [SerializeField] private float dashTime;

    [Header("threshold's")]
    [SerializeField] private float minY;
    [SerializeField] private Vector2 longDistance;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _gc = GetComponent<GroundChecker>();
        
        _playerInput = GetComponent<PlayerInput>();
        _playerControlsActions = _playerInput.actions;
    }

    public void ActivateDash()
    {
        // var notAllowedToDash = _isDashing || !_gc.IsGrounded;
        if(_isDashing || !_gc.IsGrounded) return;
        _isDashing = true;
        
        _mouseWorldPosition = SetMousePos();
        if(_canDash) StartCoroutine(StartDash());
    }
    
    IEnumerator StartDash()
    {
        animator.SetBool("IsDashing", true);
        
        yield return new WaitForSeconds(0.4f);
        
        Dash();
        
        yield return new WaitForSeconds(dashTime);

        _isDashing = false;
        animator.SetBool("IsDashing", false);
        
        yield return null;
    }

    private void Dash()
    {
        var currentPos = new Vector2(transform.position.x, transform.position.y);
        var dashDirection = _mouseWorldPosition - currentPos;

        if(dashDirection.y < minY) return;
        if(dashDirection.y < longDistance.y && Mathf.Abs(dashDirection.x) > longDistance.x) return;
        
        _rb.AddForce(dashDirection * dashForcePower, ForceMode2D.Impulse);
    }

    private Vector2 SetMousePos()
    {
        var mousePos = _playerControlsActions["MousePosition"].ReadValue<Vector2>();
        return Camera.main.ScreenToWorldPoint(new Vector2(mousePos.x, mousePos.y));
    }

    private Vector2 SetDashDirection()
    {
        return _playerControlsActions["Move"].ReadValue<Vector2>();
    }

    public void SetIsDashing(bool input) => _isDashing = input;
    public void ToggleCanDash() => _canDash = !_canDash;

    public bool IsDashing
    {
        get => _isDashing;
        private set => _isDashing = value;
    }
}
