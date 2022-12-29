using System.Collections;
using UnityEngine;

public class PlayerBasicMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private GroundChecker _gc;

    private float _gravity;
    private float _accelerationSpeed;
    
    private bool _isWalking;
    private bool _isDecelerating;

    private Vector2 _lastMoveDirection;
    
    [Header("Read value's")]
    [SerializeField] private float topSpeed;
    [SerializeField] private Vector2 moveDirection;
    [SerializeField] private bool canMove;
    [SerializeField] private bool isRolling;

    [Header("Walk")]
    [SerializeField] private float groundedSpeed;
    [SerializeField] private float airedSpeed;
    [SerializeField] private float accelerationTime;
    [SerializeField] private float decelerationStrength;
    
    [Header("Roll")]
    [SerializeField] private float rollPower;
    [SerializeField] private float rollTime;

    [Header("Other")]
    [SerializeField] private float deadzone;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _gc = GetComponent<GroundChecker>();
    }

    private void FixedUpdate()
    {
        _gravity = _rb.velocity.y;
        
        if(!canMove) return;
        
        if(_isWalking) Walking();
    }

    private void Walking()
    {
        if(isRolling) return;

        topSpeed = _gc.IsGrounded ? groundedSpeed : airedSpeed;
        
        var acceleration = topSpeed / accelerationTime;

        if(_accelerationSpeed < topSpeed) _accelerationSpeed += acceleration;
        else if (_accelerationSpeed >= topSpeed) _accelerationSpeed = topSpeed;

        var velocity = _rb.velocity;
        
        var moveForce = velocity.x =+ _accelerationSpeed;
        var move = moveForce * moveDirection.x;

        var appliedVelocity = new Vector2(move, _gravity);

        _rb.velocity = appliedVelocity;
        _isDecelerating = false;
    }

    private void Decelerate()
    {
        _accelerationSpeed = 0;
        
        if(_isDecelerating) return;

        var resetVelocity = new Vector2(decelerationStrength * _lastMoveDirection.x, _gravity);
        _rb.velocity = resetVelocity;

        _isDecelerating = true;
    }

    public void ActivateRoll()
    {
        if(isRolling || !_gc.IsGrounded || _lastMoveDirection.x == 0) return;

        StartCoroutine(Roll(_lastMoveDirection.x));
    }

    public void CancelRoll()
    {
        StopCoroutine(Roll(_lastMoveDirection.x));
        isRolling = false;
    }
    
    IEnumerator Roll(float rollDirection)
    {
        ToggleCanMove(false);

        var rollForce = Vector2.zero;
        rollForce.x = rollDirection * rollPower;
        
        var timer = rollTime;
        while (timer > 0)
        {
            isRolling = true;
            _rb.velocity = rollForce;

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
        
        if (input.y > deadzone) input.y = 1;
        else if (input.y < -deadzone) input.y = -1;
        else input.y = 0;

        moveDirection = input;

        _isWalking = moveDirection != Vector2.zero;

        if (moveDirection != Vector2.zero) _lastMoveDirection.x = moveDirection.x;
        else Decelerate();
    }

    public void ToggleCanMove(bool input)
    {
        canMove = input;
    }
}
