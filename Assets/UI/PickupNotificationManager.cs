using UnityEngine;

public class PickupNotificationManager : MonoBehaviour
{
    public static PickupNotificationManager Instance;

    public Transform notificationParent;
    public GameObject notificationPrefab;

    void Awake()
    {
        Instance = this;
    }

    public void Show(ItemData item, int amount)
    {
        GameObject obj = Instantiate(notificationPrefab, notificationParent, false);
        obj.GetComponent<PickupNotificationUI>().Setup(item, amount);
    }
}
