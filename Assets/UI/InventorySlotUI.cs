using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI quantityText;

    private InventoryUI invScript;

    private ItemData item;

    void Start()
    {
        invScript=transform.parent.parent.parent.GetComponent<InventoryUI>();
    }
    public void Setup(ItemData newItem, int newQuantity)
    {
        item = newItem;

        if (icon != null)
        {
            icon.sprite = item.icon;
            icon.enabled = true;
        }

        if (quantityText != null)
        {
            quantityText.text = item.stackable && newQuantity > 1
                ? newQuantity.ToString()
                : "";
        }
    }

    public void OnClick()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        InventoryManager.Instance.UseItem(item, player);
        invScript.Toggle();
    }
}
