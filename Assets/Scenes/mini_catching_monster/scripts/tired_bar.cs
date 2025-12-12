using UnityEngine;

public class progress_bar : MonoBehaviour
{
    [SerializeField] private GameObject bar;
    private float progress=50;
    public double curr_scale=0.5;
    public float max_progress=100;
    public float min_progress=0;

    void Start()
    {
        Vector3 scale = bar.transform.localScale;   
        scale.x = (float)0.5;               
        bar.transform.localScale = scale;
    }
    public void collect_monster()
    {
        progress+=7;
        if(progress>= max_progress)
        {
            progress=max_progress;
            Debug.Log("u win bro");
            // Ked minihru vyhra je zavolana funkcia ktora zmeni scenu a quest state
            FindFirstObjectByType<MinigameManager>().CompleteMinigame();

        }
        curr_scale=progress/max_progress;
        change_scale();
    }

    public void collect_trash()
    {
        progress-=10;
        if (progress <= min_progress)
        {
            progress=min_progress;
            Debug.Log("u done bro");
            // Ked minihru prehra je zavolana funkcia ktora zmeni scenu a quest state
            FindFirstObjectByType<MinigameManager>().CompleteMinigame();
        }
        if (progress > 0)
        {
            curr_scale=progress/max_progress;
        }
        else
        {
            curr_scale=0;
        }
        change_scale();
    }

    void change_scale()
    {
        Vector3 scale = bar.transform.localScale;   
        scale.x = (float)curr_scale;               
        bar.transform.localScale = scale;
    }
}
