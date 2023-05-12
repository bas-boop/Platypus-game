using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryPointUI : MonoBehaviour
{
    [SerializeField] private float fadeOutTime;
    [SerializeField] private float waitFadingTime;
    
    private Image _color;
    private RectTransform _fill;

    private void Awake()
    {
        _color = GetComponent<Image>();
        _fill = GetComponent<RectTransform>();
    }

    public void SetFillAmount(float widht)
    {
        ResetAlpha();
        
        var scale = new Vector3(widht / 100, _fill.localScale.y);
        _fill.localScale = scale;
        
        StartCoroutine(FadeOut());
    }

    private void ResetAlpha()
    {
        _color.CrossFadeAlpha(1,0,false);
    }
    
    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(waitFadingTime);
        _color.CrossFadeAlpha(0, fadeOutTime, false);
    }
}
