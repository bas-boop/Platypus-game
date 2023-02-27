using UnityEngine;

public class PlayerEnemyCollision : MonoBehaviour
{
    private void Awake() => Physics2D.IgnoreLayerCollision(7, 6, true);
}
