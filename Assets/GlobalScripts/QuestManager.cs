using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    public enum QuestState
    {
        NotStarted,
        StartedAfterNPC,
        MinigameInProgress,
        MinigameCompleted,
        FinalStage,
        Completed
    }

    public QuestState CurrentState = QuestState.NotStarted;

    void Awake()
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

    public void StartQuest()
    {
        CurrentState = QuestState.StartedAfterNPC;
        Debug.Log("Quest started!");
    }

    public void MinigameStarted()
    {
        CurrentState = QuestState.MinigameInProgress;
        Debug.Log("Minigame scene started.");
    }

    public void MinigameCompleted()
    {
        CurrentState = QuestState.MinigameCompleted;
        Debug.Log("Minigame completed.");
    }

    public void FinishQuest()
    {
        CurrentState = QuestState.Completed;
        Debug.Log("Quest finished!");
    }

}
