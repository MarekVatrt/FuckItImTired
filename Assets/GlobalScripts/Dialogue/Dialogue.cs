using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [Header("Speaker")]
    public string speakerName;
    public Sprite NPCSprite;

    [Header("Dialogue")]
    [TextArea(3, 5)]
    public string[] sentences;

    [Header("Choices (Optional)")]
    public DialogueChoice[] choices;
}
