using UnityEngine;
using System.Collections;


public class NPC_QuestGiver : Interactable
{
    public Dialogue dialogue;

    private bool questStarted = false;

    public override void Interact()
    {
        if (!questStarted)
        {
            DialogueManager.Instance.StartDialogue(dialogue);
            StartCoroutine(WaitForDialogueAndStartQuest());
        }
        else
        {
            Debug.Log("Quest already started.");
        }
    }

    private IEnumerator WaitForDialogueAndStartQuest()
    {
        // Wait until dialogue closes
        while (DialogueManager.Instance.DialogueActive())
            yield return null;

        // Start quest
        QuestManager.Instance.StartQuest();
        questStarted = true;
        Debug.Log("Quest has begun!");
    }
}
