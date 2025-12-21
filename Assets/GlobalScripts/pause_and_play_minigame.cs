using UnityEngine;

public class pause_and_play_minigame : MonoBehaviour
{
    void Start()
    {
        //na zaciatku minihry zastavime hru
        QuestManager.Instance.SetPlayerActive(false);
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //pri stlaceni entru ju spustime a zastavime script, aby sme zbytocne nedetegovali Enter
        if (Input.GetKeyDown("space"))
        {
            Time.timeScale = 1f;
            gameObject.SetActive(false);
        }
    }
}
