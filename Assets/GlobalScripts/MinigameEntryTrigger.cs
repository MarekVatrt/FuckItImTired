using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameEntryTrigger : MonoBehaviour
{
    [Header("Minigame")]
    [SerializeField] private string minigameSceneName;

    [Header("Quest")]
    [SerializeField] private QuestStep requiredQuestStep;

    private GameObject player;

    private bool playerInside;

    void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.E))
        {
            TryEnterMinigame();
        }
    }

    private void TryEnterMinigame()
    {
        if (QuestManager.Instance.CurrentStep != requiredQuestStep)
        {
            Debug.Log("Minigame not available yet.");
            return;
        }
        // Deaktivuj hraca lebo nema byt v minihre
        QuestManager.Instance.SetPlayerActive(false);

        QuestManager.Instance.MarkMinigameStarted();
        SceneManager.LoadScene(minigameSceneName);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            player = other.gameObject;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            player = null;
        }
    }
}
