using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GroundChecker))]
public class PlayerMoveData : MonoBehaviour
{
    [Header("Refrence's")]
    [SerializeField] private SpriteRenderer sprite;
    [field: SerializeField] public Animator Animator { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public GroundChecker GroundChecker { get; private set; }
    
    [field: SerializeField] public bool CanMove { get; private set; } = true;
    [SerializeField] private float deadzone;
    [SerializeField] private Vector2 lastMoveDirection;
    
    public Vector2 MoveDirection { get; private set; }
    public Vector2 LastMoveDirection => lastMoveDirection;
    public Vector2 MouseWorldPosition { get; set; }

    public float Gravity { get; private set; }

    public bool IsDashing { get; set; }
    public bool IsDecelerating { get; set; }
    public bool IsRolling { get; set; }
    public bool IsSmacking { get; set; }
    public bool CanDash { get; private set; } = true;
    
    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        GroundChecker = GetComponent<GroundChecker>();
    }

    private void FixedUpdate()
    {
        Gravity = Rigidbody.velocity.y;
    }

    public void SetMoveDirection(Vector2 input)
    {
        if(!CanMove) return;
        
        if (input.x > deadzone) input.x = 1;
        else if (input.x < -deadzone) input.x = -1;
        else input.x = 0;
        
        if (input.y > deadzone) input.y = 1;
        else if (input.y < -deadzone) input.y = -1;
        else input.y = 0;

        MoveDirection = input;

        if (MoveDirection != Vector2.zero)
        {
            lastMoveDirection = LastMoveDirection;
            lastMoveDirection.x = MoveDirection.x;
        }

        sprite.flipX = LastMoveDirection.x > 0;
        Animator.SetFloat("LastMoveDirection", LastMoveDirection.x);
    }

    public void ToggleCanMove()
    {
        CanMove = !CanMove;
        CanDash = !CanDash;
    }
}
