using System.Collections;
using UnityEngine;

public class EnemyMovingState : EnemyBaseState
{
    private GroundChecker _groundChecker;
    private Rigidbody2D _rigidbody2D;
    
    [SerializeField] private float speed;
    [SerializeField] private float moveTime = 1;

    public int WalkDirection { get; private set; } = 1;

    protected override void EnterState(EnemyStateManger enemy)
    {
        //todo: Moving Animation
        
        SetWalkDirection();
        StartCoroutine(StopMoving(enemy));
    }

    protected override void UpdateState(EnemyStateManger enemy)
    {
        Walk();
    }
    
    protected override void FixedUpdateState(EnemyStateManger enemy) { }
    protected override void ExitState(EnemyStateManger enemy){ }

    private void Awake()
    {
        _groundChecker = GetComponent<GroundChecker>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void SetWalkDirection()
    {
        var random = Random.Range(-1f, 1f);

        if (random > 0) WalkDirection = -1;
        else if (random < 0) WalkDirection = 1;
    }

    private void Walk()
    {
        if (!_groundChecker.IsGrounded) return;
        
        var desiredPosition = new Vector2(speed * WalkDirection, 0);
        _rigidbody2D.velocity = desiredPosition * Time.deltaTime;
    }

    private IEnumerator StopMoving(EnemyStateManger enemy)
    {
        yield return new WaitForSeconds(moveTime);
        enemy.SwitchState(enemy.idleState);
        _rigidbody2D.velocity = Vector2.zero;
    }
}
