using UnityEngine;

public class in_range_detector : MonoBehaviour
{
    Enemy_ai AI_script;
    void Start()
    {
        AI_script=transform.root.GetComponentInChildren<Enemy_ai>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("imma attack u buddy");
            AI_script.in_range=true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("come back 'eeere");
            AI_script.in_range=false;
        }
    }
}
