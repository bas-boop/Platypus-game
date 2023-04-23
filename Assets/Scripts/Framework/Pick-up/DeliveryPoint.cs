using UnityEngine;
using UnityEngine.Events;

public sealed class DeliveryPoint : MonoBehaviour
{
    [SerializeField] private PickupType deliverablePickupType;
    
    [SerializeField] private int items;
    [SerializeField] private int maxAmountItems;

    [SerializeField] private UnityEvent reachedMaxAmount = new UnityEvent();
    [SerializeField] private UnityEvent onDeposit = new UnityEvent();
    
    private void OnTriggerEnter2D(Collider2D playerCollider)
    {
        if (playerCollider.gameObject != PickupSystem.Instance.Player() || items == maxAmountItems) return;

        if (!PickupSystem.Instance.RemovePickup(deliverablePickupType)) return;
        
        AddItem();
    }

    private void AddItem()
    {
        items++;
        
        if (items == maxAmountItems)
        {
            reachedMaxAmount?.Invoke();
            return;
        }
        
        onDeposit?.Invoke();
    }
}
