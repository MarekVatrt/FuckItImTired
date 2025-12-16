using UnityEngine;

[CreateAssetMenu(menuName = "Items/Weapon")]
public class WeaponItem : ItemData
{
    [Header("Weapon Stats")]
    public int damage;
    public float attackSpeed;

    private void OnEnable()
    {
        itemType = ItemType.Weapon;
        stackable = false;
        maxStack = 1;
    }

    public override void Use(GameObject player)
    {
        Debug.Log("Equipped weapon: " + itemName);

        // Weapon equipping logic will go here later
        // Example: player.GetComponent<PlayerWeapon>().Equip(this);
    }
}
