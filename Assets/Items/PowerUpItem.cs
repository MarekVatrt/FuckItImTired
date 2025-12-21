using UnityEngine;

[CreateAssetMenu(menuName = "Items/PowerUp")]
public class PowerUpItem : ItemData
{

    [Header("Audio")]
    public AudioClip speedBoostSound;

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

        PlayerStats stats = PlayerStats.Instance;

        if (stats == null)
        {
            Debug.LogWarning("PlayerStats not found on player!");
            return;
        }


        if (healthRestore > 0)
        {
            if (stats.IsFullHealth)
            {
                isConsumed = false;
                return;
            }

            stats.Heal(healthRestore);

        }

        if (speedBoost > 0 && boostDuration > 0)
        {
            stats.BoostSpeed(speedBoost, boostDuration);
            PlaySpeedBoostSound(player, boostDuration);
        }


        if (isHealthPowerUp && boostDuration > 0)
            stats.BoostMaxHealth(boostDuration);

        if (isDamagePowerUp && boostDuration > 0)
            stats.BoostDamage(boostDuration);

        if (isKnockbackPowerUp && boostDuration > 0)
            stats.BoostKnockback(boostDuration);
    }

    private void PlaySpeedBoostSound(GameObject player, float duration)
    {
        if (speedBoostSound == null) return;

        AudioSource source = player.GetComponent<AudioSource>();
        if (source == null)
            source = player.AddComponent<AudioSource>();

        source.clip = speedBoostSound;
        source.loop = true;
        source.Play();

        player.GetComponent<MonoBehaviour>()
              .StartCoroutine(StopSoundAfterTime(source, duration));
    }

    private System.Collections.IEnumerator StopSoundAfterTime(AudioSource source, float time)
    {
        yield return new WaitForSeconds(time);

        if (source != null)
        {
            source.Stop();
            source.loop = false;
            source.clip = null;
        }
    }



}
