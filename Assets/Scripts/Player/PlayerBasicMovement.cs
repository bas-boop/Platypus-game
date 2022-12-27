using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private GroundChecker _gc;
    // private SpriteRenderer _sp;

    private float _currentSpeed;
    private float _accelerationSpeed;
    private float _decerationSpeed;
    private float _moveDuration;
    private float _maxSpeed;

    private bool _canRoll;
    private bool _isWalking;

    private Vector2 _lastMoveDirection;

    [Header("Read value's")]
    [SerializeField] private float topSpeed;
    [SerializeField] private Vector2 moveDirection;
    [SerializeField] private bool canMove;
    [SerializeField] private bool isRolling;

    [Header("Stats")]
    [SerializeField] private float deadzone;
    [SerializeField] private float accelerationTime;
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
        if (_moveDuration > 0) _maxSpeed = _rb.velocity.x;
        
        if(!canMove) return;
        
        Walking();
    }

    private void Walking()
    {
        if(isRolling) return;

        topSpeed = _gc.IsGrounded ? groundedSpeed : airedSpeed;
        
        var acceleration = topSpeed / accelerationTime;

        if(_accelerationSpeed < topSpeed) _accelerationSpeed += acceleration;
        else if (_currentSpeed >= topSpeed) _currentSpeed = topSpeed;

        _currentSpeed = _isWalking ? _accelerationSpeed : _decerationSpeed;

        var velocity = _rb.velocity;
        
        var moveForce = velocity.x =+ _currentSpeed;
        var move = moveForce * moveDirection.x;

        var appliedVelocity = new Vector2(move, velocity.y);

        _rb.velocity = appliedVelocity;
        
        // Decelerate(move);
    }

    private void Decelerate(float power)
    {
        var multiplier = 100f;
        var decelForce = new Vector2(power * multiplier, 0);
        if (moveDirection.x == 0){ _rb.AddForce(decelForce, ForceMode2D.Impulse);}
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
        _moveDuration += Time.deltaTime;
        
        if (input.x > deadzone) input.x = 1;
        else if (input.x < -deadzone) input.x = -1;
        else input.x = 0;

        moveDirection = input;

        _isWalking = moveDirection != Vector2.zero;

        if (moveDirection != Vector2.zero) _lastMoveDirection = moveDirection;
        else _accelerationSpeed = 0;
    }

    public void ToggleCanMove(bool input)
    {
        canMove = input;
    }
}
