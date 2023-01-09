using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GroundChecker))]
public class PlayerBasicMovement : MonoBehaviour
{
    [Header("Refrence's")]
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Animator animator;
    private Rigidbody2D _rb;
    private GroundChecker _gc;
    private DashAbillity _da;

    private float _gravity;
    private float _accelerationSpeed;
    private float _blinkTimer;
    private float _sitTimer;
    
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
    [SerializeField] private float blinkTimerStartTime;
    [SerializeField] private float sitTimerStartTime;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _gc = GetComponent<GroundChecker>();
        _da = GetComponent<DashAbillity>();
    }

    private void FixedUpdate()
    {
        _gravity = _rb.velocity.y;
        
        UpdateAnimations();
        
        if(!canMove) return;
        
        if(_isWalking && !_da.IsDashing) Walking();
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
        
        animator.SetBool("IsWalking", true);
    }

    private void Decelerate()
    {
        _accelerationSpeed = 0;
        
        if(_isDecelerating) return;

        var resetVelocity = new Vector2(decelerationStrength * _lastMoveDirection.x, _gravity);
        _rb.velocity = resetVelocity;

        _isDecelerating = true;
        animator.SetBool("IsWalking", false);
    }

    #region Roll

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
        animator.SetBool("IsRolling", true);
        
        yield return new WaitForSeconds(0.2f);
        
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
        if (!_da.IsDashing) _rb.velocity = Vector2.zero;
        animator.SetBool("IsRolling", false);
        ToggleCanMove(true);

        yield return null;
    }

    #endregion

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

        sprite.flipX = _lastMoveDirection.x > 0;
        animator.SetFloat("LastMoveDirection", _lastMoveDirection.x);
    }

    private void UpdateAnimations()
    {
        if(_isWalking) return;
        
        _blinkTimer -= Time.deltaTime;
        _sitTimer -= Time.deltaTime;

        if (_blinkTimer <= 0)
        {
            animator.SetTrigger("DoBlink");
            _blinkTimer = blinkTimerStartTime;
        }

        if (_sitTimer <= 0)
        {
            animator.SetTrigger("DoSit");
            _sitTimer = sitTimerStartTime;
        }
    }

    public void ToggleCanMove(bool input)
    {
        canMove = input;
    }

    public Vector2 LastMoveDirection
    {
        get => _lastMoveDirection;
        private set => _lastMoveDirection = value;
    }
    
    public bool IsRolling
    {
        get => isRolling;
        private set => isRolling = value;
    }
}
