using UnityEngine;
using UnityEngine.UI;

public class HotbarSlotUI : MonoBehaviour
{
    public Image icon;
    // public GameObject selectedFrame;

    public void SetItem(ItemData item)
    {
        if (item == null)
        {
            icon.enabled = false;
        }
        else
        {
            icon.enabled = true;
            icon.sprite = item.icon;
        }

        // selectedFrame.SetActive(selected);
    }
}
