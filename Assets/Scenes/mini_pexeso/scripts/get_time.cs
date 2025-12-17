using UnityEngine;
using TMPro;
using System;

//upravena script get_score
public class get_time : MonoBehaviour
{
    public game_manager manager_script;
    private TextMeshProUGUI time_text;

    void Start()
    {
        time_text=GetComponent<TextMeshProUGUI>();
        manager_script=transform.parent.parent.GetComponentInChildren<game_manager>();
    }

    void Update()
    {
        float time = (float)Math.Ceiling(manager_script.game_timer * 10f) / 10f;
        time_text.text=time.ToString();
    }
}