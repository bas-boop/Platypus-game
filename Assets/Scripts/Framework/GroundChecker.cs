using UnityEngine;
using UnityEngine.Events;

public class GroundChecker : MonoBehaviour
{
    private bool _isOnGround;
    private bool _leavesGround;

    [Header("Debug")]
    [SerializeField] private bool gizmos;
    [SerializeField] private Color gizmosColor = Color.cyan;

    [Header("Setting's")]
    [SerializeField] private float rayRadius = 1f;
    [SerializeField] private LayerMask thisIsGround;
    [SerializeField] private Vector2 offSet;
    [field: SerializeField] public bool IsGrounded { get; private set; }

    [SerializeField] private UnityEvent onGroundEnter = new UnityEvent();
    [SerializeField] private UnityEvent onGroundLeave = new UnityEvent();

    private void FixedUpdate()
    {
        var origin = transform.position + new Vector3(offSet.x, offSet.y, 0);
        IsGrounded = Physics2D.OverlapCircle(origin, rayRadius, thisIsGround);

        if (IsGrounded)
        {
            _isOnGround = true;
            _leavesGround = false;
        }

        switch (IsGrounded)
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
        
        Debug.Log(IsGrounded);
    }

    private void OnDrawGizmos()
    {
        if (!gizmos) return;
        
        var origin = transform.position + new Vector3(offSet.x, offSet.y, 0);
        Gizmos.color = gizmosColor;
        Gizmos.DrawWireSphere(origin, rayRadius);
    }
}
