using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smack : MonoBehaviour
{
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
        if (_isSmacking) return;
        _isSmacking = true;
        
        Smacking();
    }

    private void Smacking()
    {
        var currentPos = new Vector2(transform.position.x, transform.position.y);
        offset = new Vector2(offset.x *= _pbm.LastMoveDirection.x, offset.y);
        
        var trueOffset = currentPos + offset;
        Debug.Log(trueOffset);

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(trueOffset, hitRadius, enemyLayer);
        if (hitColliders == null) return;

        foreach (var enemies in hitColliders)
        {
            enemies.GetComponent<TestSmackObject>().GotHit();
        }

        _isSmacking = false;
    }
}
