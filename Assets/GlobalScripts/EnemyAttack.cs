using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int baseDamage = 10;

    public int GetDamage()
    {
        return Mathf.RoundToInt(
            baseDamage * GameSettings.enemyDamageMultiplier
        );
    }
}
