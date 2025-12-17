using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler
{
    public Image icon;
    public TextMeshProUGUI quantityText;

    private InventoryUI invScript;

    private ItemData item;

    void Start()
    {
        invScript = transform.parent.parent.parent.GetComponent<InventoryUI>();
    }
    public void Setup(ItemData newItem, int newQuantity)
    {
        item = newItem;

        if (icon != null)
        {
            icon.sprite = item.icon;
            icon.enabled = true;
            // Preserve aspect ensures it won't stretch
            icon.preserveAspect = true;
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
        Hotbar hotbar = FindFirstObjectByType<Hotbar>();
        HotbarUI hotbarUI = FindFirstObjectByType<HotbarUI>();

        Debug.Log("klikol si na item a ideme ho setnut");
        hotbar.SetItem(hotbar.activeIndex, item);
        hotbarUI.Refresh();

        invScript.Toggle();
        ItemTooltipUI.Instance.Hide();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
            ItemTooltipUI.Instance.Show(item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ItemTooltipUI.Instance.Hide();
    }

}
