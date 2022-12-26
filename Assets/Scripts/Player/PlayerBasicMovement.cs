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
    [SerializeField] private Vector2 moveDirection;
    
    [Header("ATRIBUTES")]
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
        Walking();
    }

    private void Walking()
    {
        var currentSpeed = _gc.IsGrounded ? groundedSpeed : airedSpeed;
        var appliedSpeed = moveDirection.x * currentSpeed;
        var appliedForce = new Vector2(appliedSpeed, 0);

        _rb.AddForce(appliedForce);
    }
    
    public void SetMoveDirection(Vector2 input)
    {
        moveDirection = input;
    }
}
