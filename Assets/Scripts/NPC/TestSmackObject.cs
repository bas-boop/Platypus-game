using System.Collections;
using UnityEngine;

public class TestSmackObject : TargetSmack
{
    [SerializeField] private SpriteRenderer sprite;

    public override void ActivateTargetSmack() => StartCoroutine(ColorChange());

    IEnumerator ColorChange()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        sprite.color = Color.white;
        yield return null;
    }
}
