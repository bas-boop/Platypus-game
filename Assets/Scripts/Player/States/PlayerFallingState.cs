using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerBaseState
{
    protected override void EnterState(PlayerStateManager player)
    {
        // player.moveData.Gravity = -2;
        // IsValidToSwitch = true;
    }

    protected override void UpdateState(PlayerStateManager player) { }

    protected override void FixedUpdateState(PlayerStateManager player) { }

    protected override void ExitState(PlayerStateManager player) { }
}
