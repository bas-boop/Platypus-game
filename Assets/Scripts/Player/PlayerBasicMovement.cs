using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private GroundChecker _gc;
    // private SpriteRenderer _sp;

    private float _acceleration;
    
    private bool _canRoll;
    private bool _isWalking;

    private Vector2 _lastMoveDirection;

    [Header("Value's")]
    [SerializeField] private float topSpeed;
    [SerializeField] private Vector2 moveDirection;
    [SerializeField] private bool canMove;
    [SerializeField] private bool isRolling;

    [Header("ATRIBUTES")]
    [SerializeField] private float deadzone;
    [SerializeField] private int accelerateFrames;
    [SerializeField] private float groundedSpeed;
    [SerializeField] private float airedSpeed;
    [SerializeField] private float rollPower;
    [SerializeField] private float rollTime;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _gc = GetComponent<GroundChecker>();
        // _sp = GetComponent<SpriteRenderer>(); 
    }

    private void FixedUpdate()
    {
        // Debug.Log(_rb.velocity);

        if(!canMove) return;
        
        Walking();
    }

    private void Walking()
    {
        if(isRolling) return;
        
        topSpeed = _gc.IsGrounded ? groundedSpeed : airedSpeed;

        var step = 0;

        while (_isWalking)
        {
            step++;
            Debug.Log(step);
            if(step > accelerateFrames) break;
        }
        
        var acceleration = topSpeed / accelerateFrames;

        var currentSpeed = topSpeed - (acceleration * step);
        
        if (currentSpeed > topSpeed) currentSpeed = topSpeed;

        var appliedForce = new Vector2(currentSpeed, _rb.velocity.y);
        // Debug.Log("Acceleration: " + acceleration + " current speed: " + currentSpeed);
        _rb.velocity = appliedForce;
    }
    
    public void ActivateRoll()
    {
        if(isRolling || !_gc.IsGrounded || _lastMoveDirection.x == 0) return;

        // StartCoroutine(Roll(_lastMoveDirection.x));
    }
    
    IEnumerator Roll(float rollDirection)
    {
        ToggleCanMove(false);

        var rollForce = Vector2.zero;
        rollForce.x = rollDirection * rollPower / 100;
        
        var timer = rollTime;
        while (timer > 0)
        {
            isRolling = true;
            _rb.AddForce(rollForce, ForceMode2D.Impulse);

            timer -= Time.deltaTime;
        }

        yield return new WaitForSeconds(rollTime);
        
        isRolling = false;
        _rb.velocity = Vector2.zero;
        ToggleCanMove(true);

        yield return null;
    }

    public void SetMoveDirection(Vector2 input)
    {
        if (input.x > deadzone) input.x = 1;
        else if (input.x < -deadzone) input.x = -1;
        else input.x = 0;

        moveDirection = input;

        _isWalking = moveDirection != Vector2.zero;
        if(moveDirection != Vector2.zero) _lastMoveDirection = moveDirection;
    }

    public void ToggleCanMove(bool input)
    {
        canMove = input;
    }
}
