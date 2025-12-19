// USE THIS SCRIPT ON OBJECT THAT

// The object does NOT load a scene

// It only advances quest steps

// Example: “Talk to NPC”, “Inspect corpse”, “Pick up letter”


using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    [SerializeField] private QuestStep requiredStep;
    [SerializeField] private QuestStep nextStep;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (!QuestManager.Instance.IsAtStep(requiredStep))
            return;

        QuestManager.Instance.AdvanceTo(nextStep);
    }
}
