using UnityEngine;
public class InventoryTest : MonoBehaviour
{
    public ItemData testItem;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            InventoryManager.Instance.AddItem(testItem, 1);
        }
    }
}
