using UnityEngine;

[System.Serializable]
public class DialogueChoice
{
    
    [Header("What player sees")]
    public string choiceText;

    [Header("Quest consequences")]
    public QuestBranch branchToSet = QuestBranch.None;
    public QuestStep stepToAdvanceTo;

    [Header("Optional")]
    public bool endsDialogue = true;
}
