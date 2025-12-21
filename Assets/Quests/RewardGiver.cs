using UnityEngine;

// toto pridat na objekt ktory da odmenu hracovy
public class RewardGiver : MonoBehaviour
{
    [SerializeField] private QuestReward reward;



    public void GiveRewards()
    {
        Debug.Log("DOSTAL SI REWARD JUCHHUUU");

        if (reward.weaponIndex != -1)
        {
            Player_attack.Instance.weapons[reward.weaponIndex].is_acq = true;
        }
        if (reward.items != null)
            foreach (ItemData item in reward.items)
                InventoryManager.Instance.AddItem(item);
    }
}
