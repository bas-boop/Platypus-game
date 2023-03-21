using System.Collections;
using UnityEngine;

public class Smack : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private PlayerBasicMovement _pbm;

    private bool _isSmacking;

    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Vector2 offset;
    [SerializeField] private float hitRadius;

    private void Awake()
    {
        _pbm = GetComponent<PlayerBasicMovement>();
    }

    public void ActivateSmack()
    {
        if (_isSmacking || _pbm.IsRolling) return;
        
        _isSmacking = true;
        animator.SetBool("IsSmacking", true);
        
        StartCoroutine(StartSmack());
    }
    
    IEnumerator StartSmack()
    {
        yield return new WaitForSeconds(0.2f);
        
        Smacking();
        
        yield return null;
    }

    private void Smacking()
    {
        var currentPos = new Vector2(transform.position.x, transform.position.y);
        offset = new Vector2(Mathf.Abs(offset.x) * _pbm.LastMoveDirection.x, offset.y);
        
        var trueOffset = currentPos + offset;

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(trueOffset, hitRadius, enemyLayer);
        if (hitColliders == null) return;
        
        for (int i = 0; i < hitColliders.Length; i++)
        {
            hitColliders[i].GetComponent<SmackTarget>().ActivateTargetSmack();
        }

        _isSmacking = false;
        animator.SetBool("IsSmacking", false);
    }
}
