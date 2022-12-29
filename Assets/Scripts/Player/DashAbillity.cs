using UnityEngine;
using UnityEngine.InputSystem;

public class DashAbillity : MonoBehaviour
{
    private Rigidbody2D _rb;
    private PlayerInput _playerInput;
    private InputActionAsset _playerControlsActions;
    
    private Vector2 _mouseWorldPosition;

    private bool _isDashing;
    
    [SerializeField] private float dashPower;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        
        _playerInput = GetComponent<PlayerInput>();
        _playerControlsActions = _playerInput.actions;
    }

    public void ActivateDash()
    {
        if(_isDashing) return;
        _isDashing = true;
        
        _mouseWorldPosition = SetMousePos();
        Dash();
        
        _isDashing = false;
    }

    private void Dash()
    {
        var currentPos = new Vector2(transform.position.x, transform.position.y);
        var dashDirection = _mouseWorldPosition - currentPos;
        
        _rb.AddForce(dashDirection * dashPower, ForceMode2D.Impulse);
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
}
