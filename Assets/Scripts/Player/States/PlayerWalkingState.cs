using UnityEngine;

[RequireComponent(typeof(PlayerMoveData))]
public class PlayerWalkingState : PlayerBaseState
{
    [Header("Read value's")]
    [SerializeField] private float topSpeed;

    [Header("Walk")]
    [SerializeField] private float groundedSpeed;
    [SerializeField] private float airedSpeed;
    [SerializeField] private float accelerationTime;

    private float _accelerationSpeed;

    protected override void EnterState(PlayerStateManager player)
    {
        IsValidToSwitch = true;
    }
    
    protected override void UpdateState(PlayerStateManager player) { }

    protected override void FixedUpdateState(PlayerStateManager player)
    {
        if (player.moveData.CanMove) Walking(player);
    }

    protected override void ExitState(PlayerStateManager player)
    {
        _accelerationSpeed = 0;
        player.moveData.Deceleration();
        player.moveData.Animator.SetBool("IsWalking", false);
    }

    private void Walking(PlayerStateManager player)
    {
        if(player.moveData.IsRolling) return;

        topSpeed = player.moveData.GroundChecker.IsGrounded ? groundedSpeed : airedSpeed;
        
        var acceleration = topSpeed / accelerationTime;

        if(_accelerationSpeed < topSpeed) _accelerationSpeed += acceleration;
        else if (_accelerationSpeed >= topSpeed) _accelerationSpeed = topSpeed;

        var velocity = player.moveData.Rigidbody.velocity;
        
        var moveForce = velocity.x =+ _accelerationSpeed;
        var move = moveForce * player.moveData.MoveDirection.x;

        var appliedVelocity = new Vector2(move, player.moveData.Gravity);

        player.moveData.Rigidbody.velocity = appliedVelocity;
        player.moveData.IsDecelerating = false;
        
        player.moveData.Animator.SetBool("IsWalking", true);
    }
}
