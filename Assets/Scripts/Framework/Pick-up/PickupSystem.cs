using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public sealed class PickupSystem : Singleton<PickupSystem>
{
    private readonly Dictionary<PickupType, int> _inventory = new Dictionary<PickupType, int>();
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
        if (_inventory.ContainsKey(pickup.PickupType))
        {
            if (pickup.IsUnique) return AddUniquePickup(pickup);

            _inventory[pickup.PickupType]++;
        }
        else _inventory[pickup.PickupType] = 1;

        UpdateCustomInspector();
        
        return true;
    }

    private bool AddUniquePickup(Pickup pickup)
    {
        if (_inventory[pickup.PickupType] == 0)
        {
            _inventory[pickup.PickupType] = 1;
            UpdateCustomInspector();
            return true;
        }
        
        Debug.LogError("Unique pick-up already exist.\nPick-up type: " + pickup.PickupType);
        return false;
    }

    /// <summary>
    /// Removes pick-up if there is more then 0 of it. If keyType does not exist there is going to be an error.
    /// </summary>
    /// <param name="pickupType">Pick-up type that is going to be removed.</param>
    public void RemovePickup(PickupType pickupType)
    {
        if (_inventory[pickupType] == 0)
        {
            Debug.LogError("There is none if that type of pick-up.");
            return;
        }

        _inventory[pickupType]--;
        UpdateCustomInspector();
    }
    
    #region Gets & Sets

        public GameObject Player() => _player;
        public Dictionary<PickupType, int> Inventory() => _inventory;

    #endregion

    private void UpdateCustomInspector() => EditorUtility.SetDirty(this);
}
