using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;


// bacha ne nejaky divny BUG ked je hrac v dialogu a pohne sa do scene changera
// tak sa quest step prograsne aj bez toho aby hrac interagoval
// s dialog oknom/npc (stlaci E)
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("Dialogue UI")]
    public GameObject dialoguePanel;
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public TMP_Text continueText;

    [Header("Character Avatar")]
    public GameObject CharacterAvatar;
    private Image NPCImage;

    [Header("Choice UI")]
    public GameObject choicePanel;
    public Button choiceButtonPrefab;

    private Dialogue currentDialogue;
    private int sentenceIndex;
    private bool isTyping;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        dialoguePanel.SetActive(false);
        choicePanel.SetActive(false);

        NPCImage = CharacterAvatar.GetComponent<Image>();
        CharacterAvatar.SetActive(false);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        sentenceIndex = 0;

        NPCImage.sprite = dialogue.NPCSprite;
        NPCImage.preserveAspect = true;

        dialoguePanel.SetActive(true);
        CharacterAvatar.SetActive(true);
        choicePanel.SetActive(false);
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
            yield return new WaitForSeconds(0.005f);
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
        if (currentDialogue.choices != null &&
            currentDialogue.showAfterIndexSentece < sentenceIndex &&
            currentDialogue.showAfterIndexSentece != 0)
        {
            ShowChoices();
        }
        if (sentenceIndex < currentDialogue.sentences.Length)
        {
            StopAllCoroutines();
            StartCoroutine(TypeSentence(currentDialogue.sentences[sentenceIndex]));
        }
        else
        {
            HandleDialogueEnd();
        }
    }

    private void HandleDialogueEnd()
    {
        if (currentDialogue.choices != null && currentDialogue.choices.Length > 0 && currentDialogue.showAfterIndexSentece == 0)
        {
            ShowChoices();
        }
        else
        {
            EndDialogue();
        }
    }

    private void ShowChoices()
    {
        DialogueChoice[] writtenChoices = currentDialogue.choices;
        if (writtenChoices.Length == 1 && writtenChoices[0].branchToSet == QuestBranch.None)
        {
            // if there is only one choice just advance to is and skip making buttons
            QuestManager.Instance.AdvanceTo(writtenChoices[0].stepToAdvanceTo);
            EndDialogue();
            return;
        }
        dialoguePanel.SetActive(false);
        choicePanel.SetActive(true);


        // Clear old buttons
        foreach (Transform child in choicePanel.transform)
            Destroy(child.gameObject);

        for (int i = 0; i < currentDialogue.choices.Length; i++)
        {
            int index = i;
            DialogueChoice choice = currentDialogue.choices[i];

            Button btn = Instantiate(choiceButtonPrefab, choicePanel.transform);
            btn.GetComponentInChildren<TMP_Text>().text = choice.choiceText;
            btn.onClick.AddListener(() => OnChoiceSelected(index));
        }
    }

    public void OnChoiceSelected(int choiceIndex)
    {
        DialogueChoice choice = currentDialogue.choices[choiceIndex];

        if (choice.branchToSet != QuestBranch.None)
            QuestManager.Instance.ChooseBranch(choice.branchToSet);

        QuestManager.Instance.AdvanceTo(choice.stepToAdvanceTo);

        choicePanel.SetActive(false);
        dialoguePanel.SetActive(true);

        if (choice.endsDialogue)
            EndDialogue();
    }

    public void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        CharacterAvatar.SetActive(false);
        choicePanel.SetActive(false);

        Player_controller.inputLocked = false;
    }

    public bool DialogueActive()
    {
        if (dialoguePanel != null && choicePanel != null)
            return dialoguePanel.activeSelf || choicePanel.activeSelf;
        return false;
    }
}
