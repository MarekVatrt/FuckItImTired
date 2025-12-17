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
        }
        else
            isConsumed = false; // Kava aby sa nekonzumla
    }
}