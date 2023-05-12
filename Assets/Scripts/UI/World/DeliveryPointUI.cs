using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryPointUI : MonoBehaviour
{
    [SerializeField] private RectTransform fill;
    private DeliveryPoint _deliveryPoint;
    
    private void Awake()
    {
        _deliveryPoint = GetComponent<DeliveryPoint>();
    }

    public void SetFillAmount()
    {
        var scale = new Vector3(_deliveryPoint.GetFillAmount() / 100, fill.localScale.y);

        fill.localScale = scale;
    }
}
