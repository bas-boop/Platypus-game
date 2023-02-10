using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyMovingState : EnemyBaseState
{
    private GroundChecker _groundChecker;
    private Rigidbody2D _rigidbody2D;
    
    [SerializeField] private float speed;
    [SerializeField] private float moveTime = 1;
    
    private int _walkDirection = 1;
    
    public override void EnterState(EnemyStateManger enemy)
    {
        Debug.Log("MovingState");
        //todo: Moving Animation
        SetWalkDirection();
        StartCoroutine(StopMoving(enemy));
    }

    public override void UpdateState(EnemyStateManger enemy)
    {
        Walk();
    }

    private void Awake()
    {
        _groundChecker = GetComponent<GroundChecker>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void SetWalkDirection()
    {
        var random = Random.Range(-1f, 1f);

        if (random > 0) _walkDirection = -1;
        else if (random < 0) _walkDirection = 1;
    }

    private void Walk()
    {
        if (!_groundChecker.IsGrounded) return;
        
        var desiredPosition = new Vector2(speed * _walkDirection, 0);
        _rigidbody2D.velocity = desiredPosition * Time.deltaTime;
    }

    private IEnumerator StopMoving(EnemyStateManger enemy)
    {
        yield return new WaitForSeconds(moveTime);
        enemy.SwitchState(enemy.idleState);
        _rigidbody2D.velocity = Vector2.zero;
    }
}
