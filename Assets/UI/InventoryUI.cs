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

    public void Toggle()
    {
        Debug.Log("Inventory UI toggled");
        isOpen = !isOpen;
        // zapneme inventory UI
        inventoryPanel.SetActive(isOpen);

        // pomocou timeScale Pauzneme hru aby sa veci zastavili
        if (isOpen)
        {
            Time.timeScale = 0f;
            Player_controller.inputLocked = true;
        }
        else
        {
            Time.timeScale = 1f;
            Player_controller.inputLocked = false;
        }


        if (isOpen)
            Refresh();
    }


    private void OnEnable()
    {
        if (InventoryManager.Instance != null)
            InventoryManager.Instance.OnInventoryChanged += Refresh;
    }

    private void OnDisable()
    {
        if (InventoryManager.Instance != null)
            InventoryManager.Instance.OnInventoryChanged -= Refresh;
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
