using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public Transform gridParent;
    public GameObject slotPrefab;

    private bool isOpen;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Toggle();
        }
    }

    void Toggle()
    {
        Debug.Log("Inventory UI toggled");

        isOpen = !isOpen;
        inventoryPanel.SetActive(isOpen);

        if (isOpen)
            Refresh();
    }


    void Refresh()
    {
        Debug.Log("Refreshing inventory UI");

        if (InventoryManager.Instance == null)
        {
            Debug.LogError("InventoryManager.Instance is NULL");
            return;
        }

        if (gridParent == null)
        {
            Debug.LogError("GridParent is NULL");
            return;
        }

        if (slotPrefab == null)
        {
            Debug.LogError("SlotPrefab is NULL");
            return;
        }

        foreach (Transform child in gridParent)
            Destroy(child.gameObject);

        foreach (InventorySlot slot in InventoryManager.Instance.inventory)
        {
            GameObject obj = Instantiate(slotPrefab, gridParent);
            Debug.Log(slot.item);
            Debug.Log(slot.quantity);
            obj.GetComponent<InventorySlotUI>().Setup(slot.item, slot.quantity);
        }
    }

}
