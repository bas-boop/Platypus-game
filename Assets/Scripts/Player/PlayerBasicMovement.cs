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
        if(!canMove) return;
        
        Walking();
    }

    private void Walking()
    {
        if(isRolling) return;

        topSpeed = _gc.IsGrounded ? groundedSpeed : airedSpeed;
        
        var acceleration = topSpeed / accelerationTime;

        if(_accelerationSpeed < topSpeed) _accelerationSpeed += acceleration * Time.deltaTime;
        else if (_currentSpeed >= topSpeed) _currentSpeed = topSpeed;

        _decerationSpeed = _currentSpeed;
        if(_decerationSpeed > topSpeed) _decerationSpeed -= acceleration * Time.deltaTime;

        _currentSpeed = _isWalking ? _accelerationSpeed : _decerationSpeed;
        // Debug.Log(_currentSpeed);

        var velocity = _rb.velocity;
        
        var moveForce = velocity.x =+ _currentSpeed * Time.deltaTime;
        var appliedVelocity = new Vector2(moveForce * moveDirection.x, velocity.y);
        
        _rb.velocity = appliedVelocity;
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

        if (moveDirection != Vector2.zero)_lastMoveDirection = moveDirection;
        else _accelerationSpeed = 0;
    }

    public void ToggleCanMove(bool input)
    {
        canMove = input;
    }
}
