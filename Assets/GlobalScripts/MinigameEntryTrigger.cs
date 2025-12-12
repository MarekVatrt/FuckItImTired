using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameEntryTrigger : MonoBehaviour
{
    public string minigameSceneName = "mini_catching_monster";

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player enters
        if (other.CompareTag("Player"))
        {
            // Only trigger if the quest state is correct
            if (QuestManager.Instance.CurrentState == QuestManager.QuestState.StartedAfterNPC)
            {
                Debug.Log("Entering minigame...");

                // Update quest state
                QuestManager.Instance.MinigameStarted();

                // Load minigame scene (replace current scene)
                SceneManager.LoadScene(minigameSceneName);
            }
        }
    }
}
