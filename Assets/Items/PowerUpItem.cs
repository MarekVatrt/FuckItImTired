using UnityEngine;

[CreateAssetMenu(menuName = "Items/PowerUp")]
public class PowerUpItem : ItemData
{
    [Header("PowerUp Stats")]
    public int healthRestore;
    public float speedBoost;
    public float boostDuration;

    private void OnEnable()
    {
        itemType = ItemType.PowerUp;
        stackable = true;
    }

    public override void Use(GameObject player)
    {
        Debug.Log("Used power-up: " + itemName);

        // Example effects (we'll implement later)
        // player.GetComponent<PlayerHealth>().Heal(healthRestore);
        // player.GetComponent<PlayerStats>().BoostSpeed(speedBoost, boostDuration);
    }
}
