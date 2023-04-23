using UnityEngine;
using UnityEngine.Events;

public sealed class Pickup : MonoBehaviour
{
    private PickupSystem _system;
    private bool _isPickedUp;
    
    [SerializeField] private GameObject visual;
    [field: SerializeField] public string PickupType { get; private set; }
    [field: SerializeField] public bool IsUnique { get; private set; }

    [SerializeField] private UnityEvent onPickedUp = new UnityEvent();
    
    private void Awake()
    {
        _system = PickupSystem.Instance;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(_isPickedUp || col.gameObject != _system.Player()) return;

        var isAdded = _system.AddPickup(this);
        if (!isAdded) return;
        
        _isPickedUp = true;
        visual.SetActive(false);
        onPickedUp?.Invoke();
    }
}
