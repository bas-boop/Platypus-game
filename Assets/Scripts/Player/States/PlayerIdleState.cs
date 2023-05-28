using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerMoveData))]
public class PlayerIdleState : PlayerBaseState
{
    [SerializeField] private float blinkTimerStartTime;
    [SerializeField] private float sitTimerStartTime;
    private float _blinkTimer;
    private float _sitTimer;

    protected override void EnterState(PlayerStateManager player)
    {
        IsValidToSwitch = true;
        StartCoroutine(ChangeDecelerating(player));
        if (player.PreviousPlayState.Equals(PlayerState.Dashing)) player.moveData.Deceleration();
    }

    protected override void UpdateState(PlayerStateManager player)
    {
        UpdateAnimations(player);
    }

    protected override void FixedUpdateState(PlayerStateManager player) { }

    protected override void ExitState(PlayerStateManager player)
    {
        _blinkTimer = blinkTimerStartTime;
        _sitTimer = sitTimerStartTime;
    }

    private void UpdateAnimations(PlayerStateManager player)
    {
        _blinkTimer -= Time.deltaTime;
        _sitTimer -= Time.deltaTime;

        if (_blinkTimer <= 0)
        {
            player.moveData.Animator.SetTrigger("DoBlink");
            _blinkTimer = blinkTimerStartTime;
        }

        if (_sitTimer <= 0)
        {
            player.moveData.Animator.SetTrigger("DoSit");
            _sitTimer = sitTimerStartTime;
        }
    }

    private static IEnumerator ChangeDecelerating(PlayerStateManager player)
    {
        yield return new WaitForSeconds(0.1f);
        player.moveData.IsDecelerating = false;
    }
}
