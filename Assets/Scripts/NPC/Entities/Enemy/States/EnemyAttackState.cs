using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    [SerializeField] private int damage;
    [SerializeField] private Vector2 hitOffset;
    [SerializeField] private LayerMask playerLayer;
    private HealthData _playerHealth;
    
    protected override void EnterState(EnemyStateManager enemy)
    {
        Smacking(enemy);
    }

    protected override void UpdateState(EnemyStateManager enemy) { }
    protected override void FixedUpdateState(EnemyStateManager enemy) { }
    protected override void ExitState(EnemyStateManager enemy) { }

    private void Awake()
    {
        _playerHealth = FindObjectOfType<HealthData>();
    }

    private void Smacking(EnemyStateManager enemy)
    {
        var currentPos = new Vector2(transform.position.x, transform.position.y);
        hitOffset = new Vector2(Mathf.Abs(hitOffset.x) * enemy.movingState.WalkDirection, hitOffset.y);
        
        var trueOffset = currentPos + hitOffset;
        var didHit = Physics2D.OverlapCircle(trueOffset, 1, playerLayer);

        if (didHit) _playerHealth.TakeDamage(damage);

        IsValidToSwitch = true;
        enemy.SwitchState(enemy.idleState);
    }
}
