using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeliveryPoint : MonoBehaviour
{
    [SerializeField]private int _sticks;
    [SerializeField] private int _maxSticks;

    [SerializeField] private UnityEvent reachedMaxAmount = new UnityEvent();
    
    private void OnTriggerEnter2D(Collider2D playerCollider)
    {
        if (playerCollider.gameObject != PickupSystem.Instance.Player()) return;

        if (_sticks == _maxSticks) return;

        if (PickupSystem.Instance.RemovePickup(PickupType.Stick))
        {
            _sticks++;
            if (_sticks == _maxSticks) reachedMaxAmount?.Invoke();
        }
    }
}
