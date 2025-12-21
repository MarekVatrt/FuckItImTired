using System;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    // public delegate void QuestStepChangedHandler(QuestStep newStep);
    // public event QuestStepChangedHandler OnQuestStepChanged;
    public event Action OnQuestContextChanged;

    public static event Action OnQuestManagerReady;


    public static QuestManager Instance;
    private GameObject player;

    [Header("Quest State")]
    public QuestStep CurrentStep = QuestStep.Zobudenie;
    public QuestBranch ChosenBranch = QuestBranch.None;


    [Header("Player Prefab")]
    [SerializeField] private GameObject playerPrefab;

    [Header("Default Spawn Point")]
    [SerializeField] private Transform defaultSpawnPoint;

    private bool minigamestarted = false;

    private void Awake()
    {
        Vector3 spawnPosition = defaultSpawnPoint.position;
        player = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Notify anyone waiting
            OnQuestManagerReady?.Invoke();
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
        // Debug.Log($"[QUEST] Advanced to {CurrentStep}");

        // OnQuestStepChanged?.Invoke(CurrentStep); // notify subscribers
        OnQuestContextChanged?.Invoke(); // notify everyone
    }

    // public void AdvanceToNextStep()
    // {
    //     CurrentStep++;
    //     Debug.Log($"Quest advanced to step " + CurrentStep);

    //     OnQuestStepChanged?.Invoke(CurrentStep); // notify subscribers
    // }

    // public void AdvanceToFailStep(QuestStep stepAfterFail)
    // {
    //     // Stay on the same quest step
    //     CurrentStep = stepAfterFail;
    //     // Debug.Log($"Quest advanced to step " + CurrentStep);

    //     OnQuestStepChanged?.Invoke(CurrentStep); // notify subscribers
    // }


    public void ChooseBranch(QuestBranch branch)
    {
        ChosenBranch = branch;
        Debug.Log($"[QUEST] Branch chosen: {branch}");
    }

    // public void StartQuest()
    // {
    //     CurrentStep = QuestStep.None;
    // }

    public void MarkMinigameStarted()
    {
        minigamestarted = true;
    }

    public void MarkMinigameEnded()
    {
        minigamestarted = false;
    }

    public bool MinigameActive()
    {
        return minigamestarted;
    }

    // --- HELPERS ---

    public bool IsAtStep(QuestStep step)
    {
        return CurrentStep == step;
    }

    // public bool HasChosenBranch()
    // {
    //     return ChosenBranch != QuestBranch.None;
    // }

    public void SetPlayerActive(bool value)
    {
        if (player != null)
            player.SetActive(value);
    }

    // --- Inventory Checks --
    public void CheckForItem(ItemData item, int amount)
    {
        // toto je tu hardcodnute ale co uz ... bohuzila
        if (CurrentStep == QuestStep.ChodSpravitKavu && item.itemName == "Coffee")
        {
            AdvanceTo(QuestStep.OdnesKavuJozefovi);
        }
        // ak vypijem kavu, pozri ci mam este nejaku v inv
        if (CurrentStep == QuestStep.OdnesKavuJozefovi && item.itemName == "Coffee")
        {
            if (!InventoryManager.Instance.HasItem(item))
            {
                // nemam uz kavu a mal by som ju jozovi doniest .. to je zle
                // chod na quest robenia kavy
                AdvanceTo(QuestStep.ChodSpravitKavu);
            }

        }
        if (CurrentStep == QuestStep.PrehratePexeso && item.itemName == "pifko")
        {
            AdvanceTo(QuestStep.ZacatePexeso);
        }
    }

    // --- Event Subscriptions ---
    private void OnEnable()
    {
        TrySubscribeInventory();
        if (Scene_changer.Instance != null)
            Scene_changer.Instance.OnSceneChange += HandleSceneChange;
            
    }

    private void OnDisable()
    {
        UnsubscribeInventory();
        if (Scene_changer.Instance != null)
            Scene_changer.Instance.OnSceneChange -= HandleSceneChange;
    }

    private void HandleSceneChange()
    {
        Debug.Log("Scena sa zmenila, Quest manager ju zachytil juchuu");
        Debug.Log("Emittujem OnQuestContextChanmged :))))");
        // Scene changed
        OnQuestContextChanged?.Invoke();
    }

    private void TrySubscribeInventory()
    {
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.OnItemAdded += CheckForItem;
            InventoryManager.Instance.OnItemRemoved += CheckForItem;
        }
        else
        {
            InventoryManager.OnInventoryManagerReady += TrySubscribeInventory;
        }
    }

    private void UnsubscribeInventory()
    {
        if (InventoryManager.Instance != null)
            InventoryManager.Instance.OnItemAdded -= CheckForItem;
            InventoryManager.Instance.OnItemRemoved -= CheckForItem;

        InventoryManager.OnInventoryManagerReady -= TrySubscribeInventory;
    }


}
