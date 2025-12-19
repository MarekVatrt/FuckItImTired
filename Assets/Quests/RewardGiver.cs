using UnityEngine;

// toto pridat na objekt ktory da odmenu hracovy
public class RewardGiver : MonoBehaviour
{
    [SerializeField] private QuestReward[] rewards;

    public void GiveRewards()
    {
        Debug.Log("DOSTAL SI REWARD ZA SPLNENIE MINIGAME JUCHHUUU");
        foreach (var reward in rewards)
        {
            // if (reward.item != null)
            //     InventoryManager.Instance.AddItem(reward.item, reward.amount);
        }
    }

}
