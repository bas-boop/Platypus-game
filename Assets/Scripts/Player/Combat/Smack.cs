using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smack : MonoBehaviour
{
    private PlayerBasicMovement _pbm;

    private bool _isSmacking;

    private Vector2 _gizmoPosition;// temp

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
        
        Smacking();
    }

    private void Smacking()
    {
        var currentPos = new Vector2(transform.position.x, transform.position.y);
        offset = new Vector2(Mathf.Abs(offset.x) * _pbm.LastMoveDirection.x, offset.y);
        
        var trueOffset = currentPos + offset;

        _gizmoPosition = trueOffset; // temp

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(trueOffset, hitRadius, enemyLayer);
        if (hitColliders == null) return;
        
        for (int i = 0; i < hitColliders.Length; i++)
        {
            hitColliders[i].GetComponent<TestSmackObject>().GotHit();
        }

        _isSmacking = false;
    }

    private void OnDrawGizmos()// temp
    {
        Gizmos.DrawSphere(_gizmoPosition, hitRadius);
    }
}
