using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryPoint : MonoBehaviour
{
    [SerializeField]private int _sticks;
    [SerializeField] private int _maxSticks;
    
    private void OnTriggerEnter2D(Collider2D playerCollider)
    {
        if (playerCollider.gameObject != PickupSystem.Instance.Player()) return;

        if (_sticks == _maxSticks) return;
        
        if (PickupSystem.Instance.RemovePickup(PickupType.Stick)) _sticks++;
    }
}
