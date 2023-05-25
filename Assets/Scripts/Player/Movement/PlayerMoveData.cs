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
    
    [Header("Other")]
    [SerializeField] private float decelerationStrength;
    [SerializeField] private float dashedDecelerationStrength;
    [SerializeField] private float deadzone;
    [field: SerializeField] public bool CanMove { get; set; } = true;
    [field: SerializeField] public bool WasDashing { get; set; } = false;
    
    public Vector2 MoveDirection { get; private set; }
    public Vector2 MouseWorldPosition { get; set; }
    public Vector2 LastMoveDirection => _lastMoveDirection;
    private Vector2 _lastMoveDirection;

    public float Gravity
    {
        get => Rigidbody.velocity.y;
        set
        {
            var targetVelocity = new Vector2(Rigidbody.velocity.x, value);
            Rigidbody.velocity = targetVelocity;
        }
    }

    public bool IsDashing { get; set; }
    public bool IsDecelerating { get; set; }
    public bool IsRolling { get; set; }
    public bool IsSmacking { get; set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        GroundChecker = GetComponent<GroundChecker>();
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
            _lastMoveDirection = LastMoveDirection;
            _lastMoveDirection.x = MoveDirection.x;
        }

        sprite.flipX = LastMoveDirection.x > 0;
        Animator.SetFloat("LastMoveDirection", LastMoveDirection.x);
    }

    public void Deceleration(bool wasDashing = false)
    {
        if(IsDecelerating) return;

        var decelStrength = wasDashing ? dashedDecelerationStrength : decelerationStrength;
        var resetVelocity = new Vector2(decelStrength * LastMoveDirection.x, Gravity);
        Rigidbody.velocity = resetVelocity;
        
        IsDecelerating = true;
    }

    public void ToggleCanMove() => CanMove = !CanMove;

    public void ResetWasDashing()
    {
        if (WasDashing) WasDashing = false;
    }
}
