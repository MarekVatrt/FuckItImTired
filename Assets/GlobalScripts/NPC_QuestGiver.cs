using UnityEngine;
using System.Collections;


public class NPC_QuestGiver : Interactable
{   
    // quest step v ktorom sa zapne konkretny dialog
    [SerializeField] private QuestStep questStepNeeded;
    public Dialogue dialogue;


    public override void Interact()
    {
        if (QuestManager.Instance.IsAtStep(questStepNeeded))
        {
            DialogueManager.Instance.StartDialogue(dialogue);
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
        QuestManager.Instance.AdvanceToNextStep();
        // questStarted = true;
        // Debug.Log("Quest has begun!");
    }
}
