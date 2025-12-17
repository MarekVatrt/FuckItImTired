using UnityEngine;

public class HotbarUI : MonoBehaviour
{
    public HotbarSlotUI[] slotUIs;

    Hotbar hotbar;

    void Start()
    {
        hotbar = FindFirstObjectByType<Hotbar>();
        InventoryManager.Instance.OnInventoryChanged += Refresh;
    }

    void OnDestroy()
    {
        InventoryManager.Instance.OnInventoryChanged -= Refresh;
    }

    public void Refresh()
    {
        for (int i = 0; i < slotUIs.Length; i++)
        {
            slotUIs[i].SetItem(hotbar.slots[i]);
        }
    }
}
