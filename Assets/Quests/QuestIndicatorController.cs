using System.Collections;
using UnityEngine;

public class QuestIndicatorController : MonoBehaviour
{
    [SerializeField] private QuestIndicator indicator;
    [SerializeField] private Transform playerPos;

    private void OnEnable()
    {
        if (QuestManager.Instance != null)
        {
            QuestManager.Instance.OnQuestContextChanged += RefreshIndicator;
            // Scene_changer.Instance.OnSceneChange += RefreshIndicator;
        }
        else
        {
            QuestManager.OnQuestManagerReady += SubscribeToQuest;
        }
    }

    private void SubscribeToQuest()
    {
        QuestManager.Instance.OnQuestContextChanged += RefreshIndicator;
        QuestManager.OnQuestManagerReady -= SubscribeToQuest;
    }

    private void OnDisable()
    {
        if (QuestManager.Instance != null)
            QuestManager.Instance.OnQuestContextChanged -= RefreshIndicator;

        QuestManager.OnQuestManagerReady -= SubscribeToQuest;
    }


    private void Start()
    {
        // Set player reference on the indicator
        indicator.player = playerPos;

        // Initial refresh in case the quest step is already set
        RefreshIndicator();
    }

    private void Update()
    {
        // Only show indicator if not in dialogue or minigame
        if (!DialogueManager.Instance.DialogueActive() &&
            !QuestManager.Instance.MinigameActive())
        {
            indicator.gameObject.SetActive(true);
        }
        else
        {
            indicator.gameObject.SetActive(false);
        }
    }


    // Refreshes the indicator based on the current quest step and provider.
    private void RefreshIndicator()
    {
        StartCoroutine(WaitForProviderAndRefresh());
    }

    private IEnumerator WaitForProviderAndRefresh()
    {
        Debug.Log($"AKTIVNA SCENA JE {UnityEngine.SceneManagement.SceneManager.GetActiveScene().name}");
        // Wait until QuestTargetProvider.Active exists and is in the active scene
        while (QuestTargetProvider.Active == null ||
               QuestTargetProvider.Active.gameObject.scene != UnityEngine.SceneManagement.SceneManager.GetActiveScene())

        {
            yield return null; // wait a frame
        }

        Transform target = QuestTargetProvider.Active.GetTargetTransform(QuestManager.Instance.CurrentStep);

        if (target != null)
        {
            indicator.player = playerPos;
            indicator.SetTarget(target);
        }
    }
}
