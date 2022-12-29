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

        SetMousePos();
        Dash();
        
        isDashing = false;
    }

    private void Dash()
    {
        var currentPos = new Vector2(transform.position.x, transform.position.y);

        var yes = _mouseWorldPosition - currentPos;

        Debug.Log(yes);

        _rb.AddForce(yes * dashPower, ForceMode2D.Impulse);
    }
    
    private void SetMousePos()
    {
        var mousePos = _playerControlsActions["MousePosition"].ReadValue<Vector2>();
        _mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Mathf.Abs(Camera.main.transform.position.z)));
    }
}
