using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRollState : PlayerBaseState
{
    [SerializeField] private bool isRolling;
    [SerializeField] private bool canMove = true;
    [SerializeField] private Animator animator;
    [SerializeField] private float rollPower;
    [SerializeField] private float rollTime;
    private Rigidbody2D _rigidbody;
    private GroundChecker _groundChecker;
    private Vector2 _lastMoveDirection;

    protected override void EnterState(PlayerStateManager player)
    {
        ActivateRoll();
    }

    protected override void UpdateState(PlayerStateManager player) { }
    protected override void FixedUpdateState(PlayerStateManager player) { }

    protected override void ExitState(PlayerStateManager player)
    {
        CancelRoll();
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _groundChecker = GetComponent<GroundChecker>();
    }

    private void ActivateRoll()
    {
        if(!canMove) return;
        if(isRolling || !_groundChecker.IsGrounded || _lastMoveDirection.x == 0) return;

        StartCoroutine(Roll(_lastMoveDirection.x));
    }

    public void CancelRoll()
    {
        StopCoroutine(Roll(_lastMoveDirection.x));
        isRolling = false;
    }
    
    private IEnumerator Roll(float rollDirection)
    {
        ToggleCanMove();
        animator.SetBool("IsRolling", true);
        
        yield return new WaitForSeconds(0.2f);
        
        var rollForce = Vector2.zero;
        rollForce.x = rollDirection * rollPower;
        
        var timer = rollTime;
        while (timer > 0)
        {
            isRolling = true;
            _rigidbody.velocity = rollForce;

            timer -= Time.deltaTime;
        }

        yield return new WaitForSeconds(rollTime);
        
        isRolling = false;
        /*if (!_da.IsDashing)*/ _rigidbody.velocity = Vector2.zero;
        animator.SetBool("IsRolling", false);
        ToggleCanMove();

        yield return null;
    }
    
    public void ToggleCanMove() => canMove = !canMove;
}
