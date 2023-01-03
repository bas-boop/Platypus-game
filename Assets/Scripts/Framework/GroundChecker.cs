using UnityEngine;
using UnityEngine.Events;

public class GroundChecker : MonoBehaviour
{
    private bool _isOnGround;
    private bool _leavesGround;
    
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

        if (!isGrounded) _isOnGround = false;
        if (isGrounded) _leavesGround = false;

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
}
