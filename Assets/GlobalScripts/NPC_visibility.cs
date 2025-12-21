using UnityEngine;

// this script only works when the gameobject is at a different scene and player
// goes to that scene and load this script
// it is not made dynamicaly, NPC cant "spawn" in scene
public class NPC_visibility : MonoBehaviour
{
    [SerializeField] private QuestStep BeVisibleOnStep;
    void Start()
    {
        if(QuestManager.Instance == null)
        {
            Debug.Log("Potrebuje existovat Quest Manager aby fungoval tento script");
            return;
        }
        // set the object visible if its at right quest step
        if (QuestManager.Instance.IsAtStep(BeVisibleOnStep))
        {
            gameObject.SetActive(true);
            return;
        }
        // else set it inactive
        gameObject.SetActive(false);
    }
}
