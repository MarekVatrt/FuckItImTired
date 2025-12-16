using UnityEngine;

[CreateAssetMenu(menuName = "Items/PowerUp")]
public class PowerUpItem : ItemData
{
    [Header("PowerUp Stats")]
    public int healthRestore;
    public float speedBoost;
    public float boostDuration;
    public bool isHealthPowerUp;
    public bool isDamagePowerUp;
    public bool isKnockbackPowerUp;
    // public bool IsConsumed = true;


    private void OnEnable()
    {
        itemType = ItemType.PowerUp;
        stackable = true;
    }

    public override void Use(GameObject player)
    {
        if (player == null)
            return;

        PlayerStats stats = player.GetComponent<PlayerStats>();

        if (stats == null)
        {
            Debug.LogWarning("PlayerStats not found on player!");
            isConsumed = false;
            return;
        }

        Debug.Log("Used power-up: " + itemName);

        if (healthRestore > 0)
        {
            if (stats.IsFullHealth)
            {
                Debug.Log("MAS FULL HP BRASKO, CO ROBIS PROSIM TA ??? ");
                return;
            }

            stats.Heal(healthRestore);
            
        }

        if (speedBoost > 0 && boostDuration > 0)
            stats.BoostSpeed(speedBoost, boostDuration);
        
        if (isHealthPowerUp && boostDuration > 0)
            stats.BoostMaxHealth(boostDuration);

        if (isDamagePowerUp && boostDuration > 0)
            stats.BoostDamage(boostDuration);

        if (isKnockbackPowerUp && boostDuration > 0)
            stats.BoostKnockback(boostDuration);
    }

}
