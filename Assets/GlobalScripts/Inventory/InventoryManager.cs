using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [Header("Inventory Settings")]
    public int maxSlots = 15;

    [Header("Inventory Data")]
    public List<InventorySlot> inventory = new List<InventorySlot>();
    // event pre callnutie UI refreshu
    public event Action OnInventoryChanged;
    public event Action<ItemData, int> OnItemAdded;
    public event Action<ItemData, int> OnItemRemoved;

    public static event Action OnInventoryManagerReady;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Notify anyone waiting
            OnInventoryManagerReady?.Invoke();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool AddItem(ItemData item, int amount = 1)
    {
        int originalAmount = amount;

        // stacking logic...
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
                }
            }
        }

        // este potrebne pridat item
        while (amount > 0 && inventory.Count < maxSlots)
        {
            int toAdd = item.stackable ? Mathf.Min(item.maxStack, amount) : 1;
            inventory.Add(new InventorySlot(item, toAdd));
            amount -= toAdd;
        }

        // naplnili sme inventar, je plnka a stale mame co pridavat
        if (amount > 0)
            return false;

        int addedAmount = originalAmount - amount;

        OnItemAdded?.Invoke(item, addedAmount);
        OnInventoryChanged?.Invoke();

        return true;
    }


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
                {
                    OnItemRemoved?.Invoke(item, removed);
                    OnInventoryChanged?.Invoke();            
                    return; // menej ako 0 by to ani nemalo byt ale i guess radsej check
                }
            }
        }
    }

    public void UseItem(ItemData item, GameObject player)
    {
        if (item == null)
            return;

        item.Use(player);

        if (!item.isConsumed)
            return;

        // Consume if power-up
        // Do something if quest item (kava / pifko)
        if (item.itemType == ItemType.PowerUp ||
            item.itemType == ItemType.Quest)
        {
            RemoveItem(item, 1);
            OnInventoryChanged?.Invoke(); // notify UI
        }
    }


    public bool HasItem(ItemData item)
    {
        foreach (InventorySlot slot in inventory)
        {
            if (slot.item == item && slot.quantity > 0)
                return true;
        }
        return false;
    }

}
