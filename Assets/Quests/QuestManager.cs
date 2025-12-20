using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;
    private GameObject player;

    [Header("Quest State")]
    public QuestStep CurrentStep = QuestStep.Zobudenie;
    public QuestBranch ChosenBranch = QuestBranch.None;


    [Header("Player Prefab")]
    [SerializeField] private GameObject playerPrefab;

    [Header("Default Spawn Point")]
    [SerializeField] private Transform defaultSpawnPoint;

    private void Awake()
    {
        Vector3 spawnPosition = defaultSpawnPoint.position;
        player = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);

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

    public void SetPlayerActive(bool value)
    {
        if (player != null)
            player.SetActive(value);
    }
}
