using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryPoint : MonoBehaviour
{
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D playerCollider)
    {
        if (playerCollider.gameObject != PickupSystem.Instance.Player()) return;
        PickupSystem.Instance.RemovePickup(PickupType.Stick);
    }
}
