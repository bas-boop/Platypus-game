using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerMoveData))]
public class PlayerSmackState : PlayerBaseState
{
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Vector2 offset;
    [SerializeField] private float hitRadius;
    
    protected override void EnterState(PlayerStateManager player)
    {
        ActivateSmack(player);
    }

    protected override void UpdateState(PlayerStateManager player) { }
    protected override void FixedUpdateState(PlayerStateManager player) { }
    protected override void ExitState(PlayerStateManager player) { }

    public void ActivateSmack(PlayerStateManager player)
    {
        if (player.moveData.IsSmacking) return;
        
        player.moveData.IsSmacking = true;
        player.moveData.Animator.SetBool("IsSmacking", true);
        
        StartCoroutine(StartSmack(player));
    }
    
    private IEnumerator StartSmack(PlayerStateManager player)
    {
        yield return new WaitForSeconds(0.2f);
        
        Smacking(player);
        IsValidToSwitch = true;// todo: na animation op true zetten + ToggelCanMove()
        
        yield return null;
    }

    private void Smacking(PlayerStateManager player)
    {
        var currentPos = new Vector2(transform.position.x, transform.position.y);
        offset = new Vector2(Mathf.Abs(offset.x) /* _pbm.LastMoveDirection.x*/, offset.y);
        
        var trueOffset = currentPos + offset;

        var hitColliders = Physics2D.OverlapCircleAll(trueOffset, hitRadius, enemyLayer);
        if (hitColliders == null) return;
        
        for (int i = 0; i < hitColliders.Length; i++)
        {
            hitColliders[i].GetComponent<SmackTarget>().ActivateTargetSmack();
        }

        player.moveData.IsSmacking = false;
        player.moveData.Animator.SetBool("IsSmacking", false);
    }
}
