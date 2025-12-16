using UnityEngine;

public enum ItemType
{
    Weapon,
    PowerUp,
    Quest
}

public abstract class ItemData : ScriptableObject
{
    [Header("Basic Info")]
    public string itemName;
    [TextArea]
    public string description;
    public Sprite icon;

    [Header("Inventory")]
    public bool stackable = true;
    public int maxStack = 99;
    public ItemType itemType;

    // Called when player clicks the item in inventory
    public abstract void Use(GameObject player);
}
