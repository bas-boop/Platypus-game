using UnityEngine;
using UnityEngine.Events;

public sealed class DeliveryPoint : MonoBehaviour
{
    [SerializeField] private DeliveryPointUI ui;
    [SerializeField] private PickupType deliverablePickupType;
    
    [SerializeField] private int items;
    [SerializeField] private int maxAmountItems;

    [SerializeField] private UnityEvent reachedMaxAmount = new UnityEvent();
    [SerializeField] private UnityEvent onDeposit = new UnityEvent();
    
    private const int DecibelConvertor = 100;

    private void OnTriggerEnter2D(Collider2D playerCollider)
    {
        if (playerCollider.gameObject != PickupSystem.Instance.Player() || items == maxAmountItems || !PickupSystem.Instance.RemovePickup(deliverablePickupType)) return;

        AddItem();
    }

    private void AddItem()
    {
        items++;

        var (fillAmount, isFull) = GetFillAmount();
        ui.SetFillAmount(fillAmount, isFull);
        
        if (items == maxAmountItems)
        {
            reachedMaxAmount?.Invoke();
            return;
        }
        
        onDeposit?.Invoke();
    }
    
    private (float, bool) GetFillAmount()
    {
        if (items == maxAmountItems) return (DecibelConvertor, true);
        
        var percentage  = DecibelConvertor / maxAmountItems;
        var fillAmount = items * percentage;
        return (fillAmount, false);
    }
}
