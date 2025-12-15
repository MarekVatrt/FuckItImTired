using UnityEngine;
using TMPro;

public class get_score : MonoBehaviour
{
    public game_manager manager_script;
    private TextMeshProUGUI score_text;

    void Start()
    {
        score_text=GetComponent<TextMeshProUGUI>();
        manager_script=transform.parent.parent.GetComponentInChildren<game_manager>();
    }

    void Update()
    {
        score_text.text=manager_script.score.ToString();
    }
}
