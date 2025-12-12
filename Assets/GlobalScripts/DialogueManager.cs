using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI References")]
    public GameObject dialoguePanel;
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public TMP_Text continueText;

    private Dialogue currentDialogue;
    private int sentenceIndex;
    private bool isTyping;

    private void Awake()
    {
        dialoguePanel.SetActive(false);
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        sentenceIndex = 0;

        dialoguePanel.SetActive(true);
        continueText.gameObject.SetActive(false);

        nameText.text = currentDialogue.speakerName;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentDialogue.sentences[sentenceIndex]));
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in sentence)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(0.02f);
        }

        isTyping = false;
        continueText.gameObject.SetActive(true);
    }

    public void ContinueDialogue()
    {
        if (isTyping)
            return;

        continueText.gameObject.SetActive(false);
        sentenceIndex++;

        if (sentenceIndex < currentDialogue.sentences.Length)
        {
            StopAllCoroutines();
            StartCoroutine(TypeSentence(currentDialogue.sentences[sentenceIndex]));
        }
        else
        {
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        dialoguePanel.SetActive(false);
    }

    public bool DialogueActive()
    {
        return dialoguePanel.activeSelf;
    }
}
