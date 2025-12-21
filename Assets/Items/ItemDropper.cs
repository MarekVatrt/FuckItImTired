using UnityEngine;
using UnityEngine.UIElements;

public class ItemDropper : MonoBehaviour, IQuestTarget
{
    public Transform TargetTransform => transform;

    [SerializeField] private RewardGiver rewardGiver;

    // [SerializeField] private QuestStep RequiredQuestStep;

    private bool isPlayerIn = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D other)
    {
        isPlayerIn = other.CompareTag("Player");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isPlayerIn = false;
    }

    void Update()
    {
        // E pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            
            if (isPlayerIn)
            {
                // je zadana poziadavka na to aby vypadol item
                // if (RequiredQuestStep != QuestStep.None){
                //     if (QuestManager.Instance == null){
                        
                //     }
                //     Debug.Log("Potrebujes mat QuestManager v scene ak chces mat nastaveny required queststep");
                //     if(QuestManager.Instance.IsAtStep(RequiredQuestStep))
                // }
                // if (QuestManager.Instance == null)
                // {
                //     Debug.Log("Potrebujes mat QuestManager v scene ak chces mat nastaveny required queststep");
                //     return;
                // }
                rewardGiver.GiveRewards();
            }
        }
    }
}
