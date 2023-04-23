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

    /// <summary>
    /// Adds the pick-up to inventory. If it's unique if checks if it already exist or not.
    /// </summary>
    /// <param name="pickup">The pick-up that's wants to be added.</param>
    /// <returns>If it's added.</returns>
    public bool AddPickup(Pickup pickup)
    {
        if (_inventory.ContainsKey(pickup.pickupType))
        {
            if (pickup.isUnique) return AddUniquePickup(pickup);

            _inventory[pickup.pickupType]++;
        }
        else _inventory[pickup.pickupType] = 1;

        UpdateCustomInspector();
        
        return true;
    }

    private bool AddUniquePickup(Pickup pickup)
    {
        if (_inventory[pickup.pickupType] == 0)
        {
            _inventory[pickup.pickupType] = 1;
            UpdateCustomInspector();
            return true;
        }
        
        Debug.LogError("Unique pick-up already exist.\nPick-up type: " + pickup.pickupType);
        return false;
    }
    
    #region Gets & Sets

        public GameObject Player() => _player;
        public Dictionary<string, int> Inventory() => _inventory;

    #endregion

    private void UpdateCustomInspector() => EditorUtility.SetDirty(this);
}
