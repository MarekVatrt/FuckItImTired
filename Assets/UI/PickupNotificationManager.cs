using UnityEngine;

public class PickupNotificationManager : MonoBehaviour
{
    public static PickupNotificationManager Instance;

    public Transform notificationParent;
    public GameObject notificationPrefab;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        InventoryManager.Instance.OnItemAdded += Show;
    }

    private void OnDisable()
    {
        if (InventoryManager.Instance != null)
            InventoryManager.Instance.OnItemAdded -= Show;
    }

    public void Show(ItemData item, int amount)
    {
        GameObject obj = Instantiate(notificationPrefab, notificationParent, false);
        obj.GetComponent<PickupNotificationUI>().Setup(item, amount);
    }
}
