using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DashAbillity : MonoBehaviour
{
    private PlayerInput _playerInput;
    private InputActionAsset _playerControlsActions;
    
    private Vector2 _mouseWorldPosition;
    
    public bool isDashing;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerControlsActions = _playerInput.actions;
    }

    public void ActivateDash()
    {
        isDashing = true;

        SetMousePos();
        Dash();
        
        isDashing = false;
    }

    private void Dash()
    {
        transform.position = _mouseWorldPosition;
    }
    
    private void SetMousePos()
    {
        var mousePos = _playerControlsActions["MousePosition"].ReadValue<Vector2>();
        _mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Mathf.Abs(Camera.main.transform.position.z)));
    }
}
