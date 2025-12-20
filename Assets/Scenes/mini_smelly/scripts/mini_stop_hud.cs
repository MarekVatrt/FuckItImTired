using UnityEngine;

public class mini_stop_hud : MonoBehaviour
{
    [SerializeField] Canvas smell_bar;
    void Start()
    {
        //na zaciatku minihry zastavime hru a vypneme hud
        smell_bar.enabled=false;
        if (getters_for_hud.Instance != null)
        {
            Debug.Log("yes i am here");
            getters_for_hud.Instance.enable_hud=false;
        }
        Time.timeScale = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        //kvoli timingu vzniku instance pre getters musime dat toto aj do update
        if (getters_for_hud.Instance != null)
        {
            //Debug.Log("yes i am here");
            getters_for_hud.Instance.enable_hud=false;
        }
        //pri stlaceni entru ju spustime a zastavime script, aby sme zbytocne nedetegovali Enter
        if (Input.GetKeyDown("space"))
        {
            smell_bar.enabled=true;
            getters_for_hud.Instance.enable_hud=true;
            Time.timeScale = 1f;
            gameObject.SetActive(false);
        }
    }
}
