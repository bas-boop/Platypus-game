using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DashAbillity : MonoBehaviour
{
    private Rigidbody2D _rb;
    private PlayerInput _playerInput;
    private InputActionAsset _playerControlsActions;
    
    private Vector2 _mouseWorldPosition;
    // private Vector2 _stickDirection;
    
    public bool isDashing;

    [SerializeField] private float dashPower;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        
        _playerInput = GetComponent<PlayerInput>();
        _playerControlsActions = _playerInput.actions;
    }

    public void ActivateDash()
    {
        if(isDashing) return;
        
        isDashing = true;

        _mouseWorldPosition = SetMousePos();
        // _stickDirection = SetDashDirection();
        
        Dash();
        
        isDashing = false;
    }

    private void Dash()
    {
        var currentPos = new Vector2(transform.position.x, transform.position.y);

        var yes = _mouseWorldPosition - currentPos;

        _rb.AddForce(yes * dashPower, ForceMode2D.Impulse);
    }
    
    private Vector2 SetMousePos()
    {
        var mousePos = _playerControlsActions["MousePosition"].ReadValue<Vector2>();
        return Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Mathf.Abs(Camera.main.transform.position.z)));
    }

    private Vector2 SetDashDirection()
    {
        return _playerControlsActions["Move"].ReadValue<Vector2>();
    }
}
