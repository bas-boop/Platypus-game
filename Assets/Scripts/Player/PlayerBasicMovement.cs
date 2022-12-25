using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private SpriteRenderer _sp;

    [Header("Value's")]
    [SerializeField] private Vector2 moveDirection;
    
    [Header("ATRIBUTES")]
    [SerializeField] private float walkSpeed;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sp = GetComponent<SpriteRenderer>(); 
    }

    private void FixedUpdate()
    {
        Walking();
    }

    private void Walking()
    {
        var appliedForce = new Vector2(moveDirection.x * walkSpeed, 0);
        Debug.Log(appliedForce);
        _rb.AddForce(appliedForce);
    }
    
    public void SetMoveDirection(Vector2 input)
    {
        moveDirection = input;
    }
}
