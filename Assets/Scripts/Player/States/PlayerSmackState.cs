using System.Collections;
using UnityEngine;

public class PlayerSmackState : PlayerBaseState
{
    [SerializeField] private Animator animator;

    private bool _isSmacking;

    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Vector2 offset;
    [SerializeField] private float hitRadius;
    
    protected override void EnterState(PlayerStateManager player)
    {
        ActivateSmack();
    }

    protected override void UpdateState(PlayerStateManager player) { }
    protected override void FixedUpdateState(PlayerStateManager player) { }
    protected override void ExitState(PlayerStateManager player) { }

    public void ActivateSmack()
    {
        if (_isSmacking) return;
        
        _isSmacking = true;
        animator.SetBool("IsSmacking", true);
        
        StartCoroutine(StartSmack());
    }
    
    private IEnumerator StartSmack()
    {
        yield return new WaitForSeconds(0.2f);
        
        Smacking();
        IsValidToSwitch = true;// todo: na animation op true zetten
        
        yield return null;
    }

    private void Smacking()
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

        _isSmacking = false;
        animator.SetBool("IsSmacking", false);
    }
}
