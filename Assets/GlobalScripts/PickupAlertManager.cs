using UnityEngine;

public class PickupAlertManager : MonoBehaviour
{
    public static PickupAlertManager Instance;

    [SerializeField] private Transform panel;
    [SerializeField] private GameObject alertPrefab;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowPickup(string message)
    {
        GameObject alertObj = Instantiate(alertPrefab, panel);
        PickupAlert alert = alertObj.GetComponent<PickupAlert>();
        alert.Initialize(message);
    }
}
