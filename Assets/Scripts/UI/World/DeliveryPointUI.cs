using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryPointUI : MonoBehaviour
{
    [SerializeField] private float fadeOutTime;
    [SerializeField] private float waitFadingTime;
    
    private Image _image;
    private RectTransform _fill;

    private const int DecibelConverter = 100;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _fill = GetComponent<RectTransform>();
    }

    /// <summary>
    /// Sets the amount of the fill bar to the percentage of the items delivered.
    /// </summary>
    /// <param name="fillAmount">The percentage of the fill.</param>
    /// <param name="isFull">Is the DeliveryPoint full</param>
    public void SetFillAmount(float fillAmount, bool isFull)
    {
        var scale = new Vector3(fillAmount / DecibelConverter, _fill.localScale.y);
        _fill.localScale = scale;
        if (isFull) _image.color = Color.green;
        
        ResetAlpha();
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
