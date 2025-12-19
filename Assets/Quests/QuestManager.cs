using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    [Header("Quest State")]
    public QuestStep CurrentStep = QuestStep.WakeUp;
    public QuestBranch ChosenBranch = QuestBranch.None;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // --- CORE API ---

    public void AdvanceTo(QuestStep nextStep)
    {
        CurrentStep = nextStep;
        Debug.Log($"[QUEST] Advanced to {CurrentStep}");
    }

    public void ChooseBranch(QuestBranch branch)
    {
        ChosenBranch = branch;
        Debug.Log($"[QUEST] Branch chosen: {branch}");
    }

    public void StartQuest()
    {
        CurrentStep = 0;
    }

    public void MarkMinigameStarted()
    {
        Debug.Log("Minigame started at step " + CurrentStep);
    }

    public void AdvanceToNextStep()
    {
        CurrentStep++;
        Debug.Log("Quest advanced to step " + CurrentStep);
    }

    public void AdvanceToFailStep()
    {
        CurrentStep++; // or different logic
        Debug.Log("Quest failed path, step " + CurrentStep);
    }
    // --- HELPERS ---

    public bool IsAtStep(QuestStep step)
    {
        return CurrentStep == step;
    }

    public bool HasChosenBranch()
    {
        return ChosenBranch != QuestBranch.None;
    }
}
