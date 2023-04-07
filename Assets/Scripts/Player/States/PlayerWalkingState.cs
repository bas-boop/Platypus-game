using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkingState : PlayerBaseState
{
    [Header("Refrence's")]
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Animator animator;
    private Rigidbody2D _rigidbody;
    private GroundChecker _groundChecker;

    private float _gravity;
    private float _accelerationSpeed;

    private bool _isWalking;
    private bool _isDecelerating;

    private Vector2 _lastMoveDirection;
    
    [Header("Read value's")]
    [SerializeField] private float topSpeed;
    [SerializeField] private Vector2 moveDirection;
    [SerializeField] private bool canMove = true;
    [SerializeField] private bool isRolling;

    [Header("Walk")]
    [SerializeField] private float groundedSpeed;
    [SerializeField] private float airedSpeed;
    [SerializeField] private float accelerationTime;
    [SerializeField] private float decelerationStrength;

    [Header("Other")]
    [SerializeField] private float deadzone;

    protected override void EnterState(PlayerStateManager player)
    {
        IsValidToSwitch = true;
    }
    
    protected override void UpdateState(PlayerStateManager player) { }

    protected override void FixedUpdateState(PlayerStateManager player)
    {
        _gravity = _rigidbody.velocity.y;

        if(canMove) Walking();
    }

    protected override void ExitState(PlayerStateManager player)
    {
        Decelerate();
    }
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _groundChecker = GetComponent<GroundChecker>();
    }
    
    private void Walking()
    {
        if(isRolling) return;

        topSpeed = _groundChecker.IsGrounded ? groundedSpeed : airedSpeed;
        
        var acceleration = topSpeed / accelerationTime;

        if(_accelerationSpeed < topSpeed) _accelerationSpeed += acceleration;
        else if (_accelerationSpeed >= topSpeed) _accelerationSpeed = topSpeed;

        var velocity = _rigidbody.velocity;
        
        var moveForce = velocity.x =+ _accelerationSpeed;
        var move = moveForce * moveDirection.x;

        var appliedVelocity = new Vector2(move, _gravity);

        _rigidbody.velocity = appliedVelocity;
        _isDecelerating = false;
        
        animator.SetBool("IsWalking", true);
    }
    
    private void Decelerate()
    {
        _accelerationSpeed = 0;
        
        if(_isDecelerating) return;

        var resetVelocity = new Vector2(decelerationStrength * _lastMoveDirection.x, _gravity);
        _rigidbody.velocity = resetVelocity;

        _isDecelerating = true;
        animator.SetBool("IsWalking", false);
    }
    
    public void SetMoveDirection(Vector2 input)
    {
        if(!canMove) return;
        
        if (input.x > deadzone) input.x = 1;
        else if (input.x < -deadzone) input.x = -1;
        else input.x = 0;
        
        if (input.y > deadzone) input.y = 1;
        else if (input.y < -deadzone) input.y = -1;
        else input.y = 0;

        moveDirection = input;

        _isWalking = moveDirection != Vector2.zero;

        if (moveDirection != Vector2.zero) _lastMoveDirection.x = moveDirection.x;
        else Decelerate();

        sprite.flipX = _lastMoveDirection.x > 0;
        animator.SetFloat("LastMoveDirection", _lastMoveDirection.x);
    }
    
    public void ToggleCanMove() => canMove = !canMove;
}
