using UnityEngine;

[CreateAssetMenu(menuName = "Items/QuestItem")]
public class QuestItem : ItemData
{
    [Header("Quest item Stats")]
    public bool isBeer;
    public int boostDuration;

    private void OnEnable()
    {
        itemType = ItemType.Quest;
        stackable = true;
    }

    public override void Use(GameObject player)
    {
        PlayerStats stats = player.GetComponent<PlayerStats>();

        if (isBeer && boostDuration > 0)
        {
            stats.DrunkDebuff(boostDuration);
            isConsumed = true;
        }
        else
        {
            PlayerStats.Instance.BoostSpeed(4.5f, 5f);
            PlayerStats.Instance.BoostDamage(5f);
            PlayerStats.Instance.BoostMaxHealth(5f);

            isConsumed = true; // Kava sa da tiez konzumovat
        }
    }
}