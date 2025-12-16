using UnityEngine;

public class EnemyDropTable : MonoBehaviour
{
    [System.Serializable]
    public class DropEntry
    {
        public GameObject dropPrefab;
        [Range(0f, 1f)] public float dropChance;
    }

    public DropEntry[] possibleDrops;

    public void Drop()
    {
        if (possibleDrops.Length == 0)
            return;

        foreach (var entry in possibleDrops)
        {
            if (Random.value <= entry.dropChance)
            {
                Instantiate(entry.dropPrefab, transform.position, Quaternion.identity);
                return;
            }
        }
    }
}
