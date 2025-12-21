using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestTargetProvider : MonoBehaviour
{
    public static QuestTargetProvider Active;

    [System.Serializable]
    public class QuestTargetEntry
    {
        public QuestStep step;
        public MonoBehaviour target; // must implement IQuestTarget
    }

    public QuestTargetEntry[] targets;


    private void Awake()
    {
        // Only allow provider from active scene
        if (gameObject.scene == SceneManager.GetActiveScene())
        {
            Active = this;
            Debug.Log($"[QuestTargetProvider] Activated for {gameObject.scene.name}");
        }
    }

    private void OnDestroy()
    {
        if (Active == this)
            Active = null;
    }

    public Transform GetTargetTransform(QuestStep step)
    {
        Debug.Log("MA ZAVOLALI ABYT SOM DAL TARGET TRANSFORM");
        foreach (var entry in targets)
        {
            if (entry.step != step || entry.target == null)
                continue;

            if (entry.target is IQuestTarget questTarget)
            {
                Debug.Log($"target je {entry.target.transform}");
                return questTarget.TargetTransform;
            }

            Debug.LogError(
                $"{entry.target.name} does NOT implement IQuestTarget"
            );
        }

        return null;
    }
}
