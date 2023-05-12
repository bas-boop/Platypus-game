using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryPointUI : MonoBehaviour
{
    [SerializeField] private float fadeOutTime;
    [SerializeField] private float waitFadingTime;
    
    private Image _image;
    private RectTransform _fill;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _fill = GetComponent<RectTransform>();
    }

    public void SetFillAmount(float fillAmount, bool isFull)
    {
        ResetAlpha();
        
        var scale = new Vector3(fillAmount / 100, _fill.localScale.y);
        _fill.localScale = scale;
        if (isFull) _image.color = Color.green;
        
        StartCoroutine(FadeOut());
    }

    private void ResetAlpha()
    {
        _image.CrossFadeAlpha(1,0,false);
    }
    
    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(waitFadingTime);
        _image.CrossFadeAlpha(0, fadeOutTime, false);
    }
}
