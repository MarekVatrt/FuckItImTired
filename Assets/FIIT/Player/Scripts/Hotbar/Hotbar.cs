using UnityEngine;

public class Hotbar : MonoBehaviour
{
    // zatial iba jeden
    public ItemData[] slots = new ItemData[1];
    public int activeIndex;

    InventoryManager inventory;

    void Start()
    {
        inventory = InventoryManager.Instance;
        inventory.OnInventoryChanged += ValidateItems;
    }

    void OnDestroy()
    {
        if (inventory != null)
            inventory.OnInventoryChanged -= ValidateItems;
    }

    void ValidateItems()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null)
                continue;

            if (!inventory.HasItem(slots[i]))
            {
                slots[i] = null;

                if (i == activeIndex)
                    activeIndex = FindNextValidSlot();
            }
        }
    }

    int FindNextValidSlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] != null)
                return i;
        }
        return 0; // or -1 if you want "no active slot"
    }

    public ItemData GetActiveItem()
    {
        if (activeIndex < 0 || activeIndex >= slots.Length)
        {
            Debug.Log("TU BY SI NEMAL BYT KOKOTKO");
            return null;
            
        }

        Debug.Log($"aktivny item je {slots[activeIndex].itemName}");
        return slots[activeIndex];
    }

    public void SetItem(int index, ItemData item)
    {
        if (index < 0 || index >= slots.Length)
            return;

        slots[index] = item;
    }

    // public void SetActiveIndex(int index)
    // {
    //     if (index < 0 || index >= slots.Length)
    //         return;

    //     activeIndex = index;
    // }
}
