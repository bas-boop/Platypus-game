using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Pickup : MonoBehaviour
{
    private PickupSystem _parent;
    
    private void Awake()
    {
        _parent = PickupSystem.Instance;
    }
}
