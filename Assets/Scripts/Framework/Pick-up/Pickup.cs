using UnityEngine;
using UnityEngine.Events;

public sealed class Pickup : MonoBehaviour
{
    private PickupSystem _pickupSystem;
    private bool _isPickedUp;
    
    [SerializeField] private GameObject visual;
    [field: SerializeField] public PickupType PickupType { get; private set; }
    [field: SerializeField] public bool IsUnique { get; private set; }

    [SerializeField] private UnityEvent onPickedUp = new UnityEvent();
    
    private void Awake() => _pickupSystem = PickupSystem.Instance;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(_isPickedUp || col.gameObject != _pickupSystem.Player()) return;

        var isAdded = _pickupSystem.AddPickup(this);
        if (!isAdded) return;
        
        _isPickedUp = true;
        visual.SetActive(false);
        onPickedUp?.Invoke();
    }
}
