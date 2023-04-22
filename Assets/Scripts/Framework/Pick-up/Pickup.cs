using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public sealed class Pickup : MonoBehaviour
{
    public string pickupType;
    private PickupSystem _system;

    [SerializeField] private GameObject visual;
    
    public bool IsPickedUp { get; private set; }
    [SerializeField] private UnityEvent onPickedUp = new UnityEvent();
    
    private void Awake()
    {
        _system = PickupSystem.Instance;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(IsPickedUp || col.gameObject != _system.Player()) return;

        _system.AddPickup(this);
        IsPickedUp = true;
        visual.SetActive(false);
        onPickedUp?.Invoke();
    }
}
