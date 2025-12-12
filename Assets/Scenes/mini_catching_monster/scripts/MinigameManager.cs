using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameManager : MonoBehaviour
{
    public string mainSceneName = "ground_floor"; // name of main scene to return to

    // Call this when the minigame is completed
    public void CompleteMinigame()
    {
        // Update quest state
        QuestManager.Instance.MinigameCompleted();

        Debug.Log("Minigame completed! Returning to main scene...");

        // Load main scene
        SceneManager.LoadScene(mainSceneName);
    }
}
