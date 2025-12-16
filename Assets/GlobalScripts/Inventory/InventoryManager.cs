using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [Header("Inventory Settings")]
    public int maxSlots = 20;

    [Header("Inventory Data")]
    public List<InventorySlot> inventory = new List<InventorySlot>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ===============================
    // ADD ITEM
    // ===============================
    public bool AddItem(ItemData item, int amount = 1)
    {
        // Try stacking first
        if (item.stackable)
        {
            foreach (InventorySlot slot in inventory)
            {
                if (slot.item == item && slot.quantity < item.maxStack)
                {
                    int spaceLeft = item.maxStack - slot.quantity;
                    int toAdd = Mathf.Min(spaceLeft, amount);

                    slot.quantity += toAdd;
                    amount -= toAdd;

                    if (amount <= 0)
                        return true;
                }
            }
        }

        // Add new slots if space available
        while (amount > 0 && inventory.Count < maxSlots)
        {
            int toAdd = item.stackable ? Mathf.Min(item.maxStack, amount) : 1;
            inventory.Add(new InventorySlot(item, toAdd));
            amount -= toAdd;
        }

        // If amount > 0, inventory is full
        if (amount > 0)
        {
            Debug.Log("Inventory full!");
            return false;
        }

        Debug.Log($"Added {item.itemName} to inventory");
        return true;
    }

    // ===============================
    // REMOVE ITEM
    // ===============================
    public void RemoveItem(ItemData item, int amount = 1)
    {
        for (int i = inventory.Count - 1; i >= 0; i--)
        {
            if (inventory[i].item == item)
            {
                int removed = Mathf.Min(amount, inventory[i].quantity);
                inventory[i].quantity -= removed;
                amount -= removed;

                if (inventory[i].quantity <= 0)
                    inventory.RemoveAt(i);

                if (amount <= 0)
                    return;
            }
        }
    }

    // ===============================
    // USE ITEM
    // ===============================
    public void UseItem(ItemData item, GameObject player)
    {
        if (item == null)
            return;

        item.Use(player);

        // Consume if power-up
        if (item.itemType == ItemType.PowerUp)
        {
            RemoveItem(item, 1);
        }
    }
}
