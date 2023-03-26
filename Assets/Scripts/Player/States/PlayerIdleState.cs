using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    [SerializeField] private Animator animator;
    [SerializeField] private float blinkTimerStartTime;
    [SerializeField] private float sitTimerStartTime;
    private float _blinkTimer;
    private float _sitTimer;
    
    protected override void EnterState(PlayerStateManager player) { }

    protected override void UpdateState(PlayerStateManager player)
    {
        UpdateAnimations();
    }

    protected override void FixedUpdateState(PlayerStateManager player) { }
    protected override void ExitState(PlayerStateManager player) { }

    private void UpdateAnimations()
    {
        /*if (_isWalking)
        {
            _blinkTimer = blinkTimerStartTime;
            _sitTimer = sitTimerStartTime;
            return;
        }*/
        
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
}
