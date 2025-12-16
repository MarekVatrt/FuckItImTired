using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float baseMoveSpeed = 5.5f;
    public float CurrentMoveSpeed { get; private set; }

    [Header("Health")]
    [SerializeField] private int baseMaxHealth = 100;
    private int bonusMaxHealth = 0;

    public int MaxHealth => baseMaxHealth + bonusMaxHealth;
    public int CurrentHealth { get; private set; }

    [SerializeField] private float baseDamageMultiplier = 1f;
    private float currentDamageMultiplier;
    public float DamageMultiplier => currentDamageMultiplier;


    [SerializeField] private float baseKnockbackMultiplier = 1f;
    private float currentKnockbackMultiplier;
    public float KnockbackMultiplier => currentKnockbackMultiplier;

    public bool IsFullHealth => CurrentHealth >= MaxHealth;



    private Coroutine damageBoostRoutine;
    private Coroutine knockbackBoostRoutine;
    private Coroutine speedBoostRoutine;
    private Coroutine healthBoostRoutine;

    void Awake()
    {
        CurrentHealth = MaxHealth;
        CurrentMoveSpeed = baseMoveSpeed;
        currentDamageMultiplier = baseDamageMultiplier;
        currentKnockbackMultiplier = baseKnockbackMultiplier;

    }

    void Update()
    {
        // CurrentMoveSpeed = baseMoveSpeed;
        // CurrentHealth = maxHealth;

    }

    // ===== BUFFS =====


    // ===== MOVEMENT =====
    public void BoostSpeed(float bonusSpeed, float duration)
    {
        if (speedBoostRoutine != null)
            StopCoroutine(speedBoostRoutine);

        speedBoostRoutine = StartCoroutine(SpeedBoostCoroutine(bonusSpeed, duration));
    }

    private IEnumerator SpeedBoostCoroutine(float bonusSpeed, float duration)
    {
        CurrentMoveSpeed = baseMoveSpeed + bonusSpeed;
        yield return new WaitForSeconds(duration);
        CurrentMoveSpeed = baseMoveSpeed;
        speedBoostRoutine = null;
    }

    // ===== DAMAGE =====

    public void BoostDamage(float duration, float bonusMultiplier = 2)
    {
        if (damageBoostRoutine != null)
            StopCoroutine(damageBoostRoutine);

        damageBoostRoutine = StartCoroutine(DamageBoostCoroutine(bonusMultiplier, duration));
    }


    private IEnumerator DamageBoostCoroutine(float bonusMultiplier, float duration)
    {
        currentDamageMultiplier = baseDamageMultiplier * bonusMultiplier;
        yield return new WaitForSeconds(duration);
        currentDamageMultiplier = baseDamageMultiplier;
        damageBoostRoutine = null;
    }


    // ===== Knockback =====
    public void BoostKnockback(float duration, float bonusMultiplier = 2)
    {
        if (knockbackBoostRoutine != null)
            StopCoroutine(knockbackBoostRoutine);

        knockbackBoostRoutine = StartCoroutine(KockbackBoostCoroutine(bonusMultiplier, duration));
    }


    private IEnumerator KockbackBoostCoroutine(float bonusMultiplier, float duration)
    {
        currentKnockbackMultiplier = baseKnockbackMultiplier * bonusMultiplier;
        yield return new WaitForSeconds(duration);
        currentKnockbackMultiplier = baseKnockbackMultiplier;
        knockbackBoostRoutine = null;
    }


    // ===== HEALTH =====

    // Heal regen
    public void Heal(int amount)
    {
        if (amount <= 0)
            return;

        int oldHealth = CurrentHealth;
        CurrentHealth = Mathf.Min(CurrentHealth + amount, MaxHealth);

        Debug.Log($"Healed {CurrentHealth - oldHealth} HP (now {CurrentHealth}/{MaxHealth})");
    }


    public void BoostMaxHealth(float duration)
    {
        if (healthBoostRoutine != null)
            StopCoroutine(healthBoostRoutine);

        healthBoostRoutine = StartCoroutine(HealthBoostCoroutine(duration));
    }


    // Healing boost (twice the HP)
    private IEnumerator HealthBoostCoroutine(float duration)
    {
        int originalMax = MaxHealth;
        bonusMaxHealth = baseMaxHealth; // +100%

        // Keep same HP ratio
        float ratio = (float)CurrentHealth / originalMax;
        CurrentHealth = Mathf.RoundToInt(MaxHealth * ratio);

        yield return new WaitForSeconds(duration);

        // Remove bonus safely
        bonusMaxHealth = 0;
        CurrentHealth = Mathf.Min(CurrentHealth, MaxHealth);

        healthBoostRoutine = null;
    }


    public void TakeDamage(int amount)
    {
        if (amount <= 0)
            return;

        CurrentHealth = Mathf.Max(CurrentHealth - amount, 0);
        Debug.Log($"Took {amount} damage ({CurrentHealth}/{MaxHealth})");

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player died");
        // later: animation, respawn, game over
    }
}
