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
    [SerializeField] private string returnSceneNameOnWin;
    [SerializeField] private string returnSceneNameOnLose;
    

    public void Win()
    {
        Debug.Log("[MINIGAME] Player won!");
        if(rewardGiver != null)
            rewardGiver.GiveRewards();
        // MinigameReturnData.WasWin = true;

        // // Store current scene info if needed (optional)
        // MinigameReturnData.ReturnScene = returnSceneName;
        // MinigameReturnData.ReturnPosition = GetReturnPosition(); // spawn at door/player exit

        // Update quest
        QuestManager.Instance.AdvanceTo(winStep);
        QuestManager.Instance.MarkMinigameEnded();

        // Load previous scene
        SceneManager.LoadScene(returnSceneNameOnWin);
        QuestManager.Instance.SetPlayerActive(true);
    }

    public void Lose()
    {
        Debug.Log("[MINIGAME] Player lost!");
        // MinigameReturnData.WasWin = false;

        // MinigameReturnData.ReturnScene = returnSceneName;
        // MinigameReturnData.ReturnPosition = GetReturnPosition();

        // Update quest if needed (different path)
        QuestManager.Instance.AdvanceTo(loseStep);
        QuestManager.Instance.MarkMinigameEnded();

        SceneManager.LoadScene(returnSceneNameOnLose);
        QuestManager.Instance.SetPlayerActive(true);
    }
}
