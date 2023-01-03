using System.Collections;
using UnityEngine;

public class TestSmackObject : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;

    public void GotHit() => StartCoroutine(ColorChange());

    IEnumerator ColorChange()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        sprite.color = Color.white;
        yield return null;
    }
}
