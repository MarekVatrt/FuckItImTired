using UnityEngine;
using System.Collections;


public class DialogueGiver : MonoBehaviour, IQuestTarget
{
    // target transformer pre dany queststep
    public Transform TargetTransform => transform;
    // quest step v ktorom sa zapne konkretny dialog
    [SerializeField] private QuestStep questStepNeeded;
    public Dialogue dialogue;
    public bool lockPlayer;

    public bool isTriggeredByPlayer = false;

    [SerializeField] private bool isSceneChanger;

    public void Interact()
    {
        if (isSceneChanger)
            return;
        if (QuestManager.Instance.IsAtStep(questStepNeeded))
        {
            Debug.Log("SOM V SPRAVNOM STEPE, UKAZUJEM DIALOG");

            DialogueManager.Instance.StartDialogue(dialogue);
            if (lockPlayer)
                Player_controller.inputLocked = true;
            StartCoroutine(WaitForDialogueAndStartQuest());
        }
        else
        {
            Debug.Log("Default NPC dialog ked neni zapnuty konkretny quest");
        }
    }

    private IEnumerator WaitForDialogueAndStartQuest()
    {
        // Wait until dialogue closes
        while (DialogueManager.Instance.DialogueActive())
            yield return null;

        // Start quest
        // QuestManager.Instance.StartQuest();
        Debug.Log("SKONCIL DIALOGUE");
        // Destroy(this);
        // IBA AK HRAC INTERAGOVAL S DIALOGOM (stlacil E) CHOD NA DALSI QUEST STEP
        // if (isTriggeredByPlayer)
        // {
        //     QuestManager.Instance.AdvanceToNextStep();
        //     Debug.Log("IDE SA NA DALSI QUEST STEP");
        // }
        // questStarted = true;
        // Debug.Log("Quest has begun!");
    }
}
