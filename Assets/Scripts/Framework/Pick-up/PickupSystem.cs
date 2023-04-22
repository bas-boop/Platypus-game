using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

public sealed class PickupSystem : Singleton<PickupSystem>
{
    private readonly Dictionary<string, int> _inventory = new Dictionary<string, int>();
    private GameObject _player;

    protected override void Awake()
    {
        base.Awake();
        _player = GameObject.Find("Platypus");
    }

    public void AddPickup(Pickup pickup)
    {
        if (_inventory.ContainsKey(pickup.pickupType)) _inventory[pickup.pickupType] += 1;
        else _inventory[pickup.pickupType] = 1;

        UpdateCustomInspector();
    }

    #region Gets & Sets

        public GameObject Player() => _player;
        public Dictionary<string, int> Inventory() => _inventory;

    #endregion

    private void UpdateCustomInspector() => EditorUtility.SetDirty(this);
}
