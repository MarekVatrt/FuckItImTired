using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float baseMoveSpeed = 5.5f;
    public float CurrentMoveSpeed { get; private set; }

    private Coroutine speedBoostRoutine;

    void Awake()
    {
        CurrentMoveSpeed = baseMoveSpeed;
    }

    void Update()
    {
        CurrentMoveSpeed = baseMoveSpeed;
        
    }

    // ===== BUFFS =====

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
}
