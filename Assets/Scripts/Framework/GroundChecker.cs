using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private bool isGrounded;
    [SerializeField] private float rayDistance;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Vector2 offSet;

    [SerializeField] private UnityEvent onGroundEnter = new UnityEvent();
    [SerializeField] private UnityEvent onGroundLeave = new UnityEvent();

    public bool IsGrounded
    {
        get => isGrounded; 
        private set => isGrounded = value;
    }

    private void FixedUpdate()
    {
        var origin = transform.position + new Vector3(offSet.x, offSet.y, 0);
        isGrounded = Physics2D.Raycast(origin, Vector2.down, rayDistance, whatIsGround);
        
        if (isGrounded) onGroundEnter?.Invoke();
        else onGroundLeave?.Invoke();
    }
}
