using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameController : MonoBehaviour
{
    [Header("Quest")]
    [SerializeField] private QuestStep winStep;
    [SerializeField] private QuestStep loseStep;
    // [SerializeField] private QuestBranch branchOnWin = QuestBranch.None;

    [Header("Rewards (Win Only)")]
    [SerializeField] private RewardGiver rewardGiver;

    [Header("Scene")]
    [SerializeField] private string returnSceneName;

    public void Win()
    {
        Debug.Log("[MINIGAME] Player won!");
        rewardGiver.GiveRewards();
        // MinigameReturnData.WasWin = true;

        // // Store current scene info if needed (optional)
        // MinigameReturnData.ReturnScene = returnSceneName;
        // MinigameReturnData.ReturnPosition = GetReturnPosition(); // spawn at door/player exit

        // Update quest
        QuestManager.Instance.AdvanceToNextStep();

        // Load previous scene
        SceneManager.LoadScene(returnSceneName);
        QuestManager.Instance.SetPlayerActive(true);
    }

    public void Lose()
    {
        Debug.Log("[MINIGAME] Player lost!");
        // MinigameReturnData.WasWin = false;

        // MinigameReturnData.ReturnScene = returnSceneName;
        // MinigameReturnData.ReturnPosition = GetReturnPosition();

        // Update quest if needed (different path)
        QuestManager.Instance.AdvanceToNextStep();

        SceneManager.LoadScene(returnSceneName);
        QuestManager.Instance.SetPlayerActive(true);
    }
}
