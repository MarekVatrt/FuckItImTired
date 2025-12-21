using UnityEngine;

public class QuestIndicator : MonoBehaviour
{
    // public static QuestIndicator Instance;
    public Transform player;
    public float heightOffset = 1.5f;

    private Transform target;

    // void Awake()
    // {
    //     if (Instance == null)
    //     {
    //         Instance = this;
    //         DontDestroyOnLoad(gameObject);
    //     }
    //     else
    //     {
    //         Destroy(gameObject);
    //     }

    // }
    

    public void SetTarget(Transform newTarget)
    {
        Debug.Log("SADHJBDSAHKBJDSAKHJBSD");
        target = newTarget;
        gameObject.SetActive(target != null);
    }

    void Update()
    {
        // neskutrocne Spagety .. pardon
        if(DialogueManager.Instance == null || QuestManager.Instance == null)
        {
            // Debug.Log("je to aktivneeeeeee");
            gameObject.SetActive(true);
        }
        else if (DialogueManager.Instance.DialogueActive() || QuestManager.Instance.MinigameActive())
        {
            // vypni indikator ked je dialog alebo ked je minihra
            // Debug.Log("JE TO VYPNUTEEEE");
            gameObject.SetActive(false);
            return;
        }
        // Debug.Log("KOKOTKOTTT");
        gameObject.SetActive(true);
        if (target == null) return;
        

        // Position above player
        transform.position = player.position + Vector3.up * heightOffset;

        // Rotate toward target
        Vector3 direction = target.position - player.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }
}
