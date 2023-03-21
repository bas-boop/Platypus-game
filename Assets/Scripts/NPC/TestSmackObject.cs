using System.Collections;
using UnityEngine;

public class TestSmackObject : SmackTarget
{
    [SerializeField] private SpriteRenderer sprite;

    public override void ActivateTargetSmack() => StartCoroutine(ColorChange());

    private IEnumerator ColorChange()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        sprite.color = Color.white;
        yield return null;
    }
}
