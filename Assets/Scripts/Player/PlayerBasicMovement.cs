using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private GroundChecker _gc;
    // private SpriteRenderer _sp;

    [Header("Value's")]
    [SerializeField] private float currentSpeed;
    [SerializeField] private Vector2 moveDirection;
    [SerializeField] private bool canMove = true;

    [Header("ATRIBUTES")]
    [SerializeField] private float deadzone;
    [SerializeField] private float groundedSpeed;
    [SerializeField] private float airedSpeed;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _gc = GetComponent<GroundChecker>();
        // _sp = GetComponent<SpriteRenderer>(); 
    }

    private void FixedUpdate()
    {
        if(!canMove) return;
        
        Walking();
    }

    private void Walking()
    {
        currentSpeed = _gc.IsGrounded ? groundedSpeed : airedSpeed;
        var appliedSpeed = moveDirection.x * currentSpeed;
        var appliedForce = new Vector2(appliedSpeed, 0);

        _rb.AddForce(appliedForce);
    }
    
    public void SetMoveDirection(Vector2 input)
    {
        if (input.x > deadzone) input.x = 1;
        else if (input.x < -deadzone) input.x = -1;
        else input.x = 0;

        moveDirection = input;
    }

    public void ToggleCanMove(bool input)
    {
        canMove = input;
    }
}
