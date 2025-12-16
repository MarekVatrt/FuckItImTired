using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [Header("Item Settings")]
    public ItemData item;
    public int amount = 1;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (item != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = item.icon; // set world sprite to match inventory icon
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (item == null)
        {
            Debug.LogWarning("ItemPickup has no ItemData assigned!");
            return;
        }

        bool added = InventoryManager.Instance.AddItem(item, amount);

        if (added)
        {
            Debug.Log($"Picked up {item.itemName}");
            PickupAlertManager.Instance.ShowPickup(item.itemName);

            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Inventory full, could not pick up item.");
        }
    }
}
