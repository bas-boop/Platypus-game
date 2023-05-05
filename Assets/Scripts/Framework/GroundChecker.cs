using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private bool _isOnGround;
    private bool _leavesGround;

    [SerializeField] private bool isGrounded;
    [SerializeField] private float rayDistance = 1f;
    [SerializeField] private LayerMask thisIsGround;
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
        // isGrounded = Physics2D.Raycast(origin, Vector2.down, rayDistance, thisIsGround);
        isGrounded = Physics2D.OverlapCircle(origin, rayDistance, thisIsGround);

        if (isGrounded)
        {
            _isOnGround = true;
            _leavesGround = false;
        }

        switch (isGrounded)
        {
            case true when !_isOnGround:
                onGroundEnter?.Invoke();
                _isOnGround = true;
                break;
            case true when !_leavesGround:
                onGroundLeave?.Invoke();
                _leavesGround = true;
                break;
        }
    }

    private void OnDrawGizmos()
    {
        var origin = transform.position + new Vector3(offSet.x, offSet.y, 0);
        Gizmos.DrawWireSphere(origin, rayDistance);
    }
}
