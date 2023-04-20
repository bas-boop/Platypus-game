using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public sealed class PickupSystem : Singleton<PickupSystem>
{
    private GameObject _player;

    protected override void Awake()
    {
        base.Awake();
        _player = GameObject.Find("Platypus");
    }

    public void AddPickup(Pickup pickup)
    {
        Debug.Log(pickup.gameObject.name);
    }
    
    public GameObject Player() => _player;
}
