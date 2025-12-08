using UnityEngine;
using UnityEngine.UI;

public class window_opening : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private nausea_controller bar_script;
    //inak mi to neslo spravit, ontrigger bere iba prvy krat co je player inside
    private bool player_in_field;
    void Start()
    {
        bar_script=slider.GetComponent<nausea_controller>();
        player_in_field=false;
    }
    void Update()
    {
        if (player_in_field)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("stlacene E");
                bar_script.fill_or_empty=false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        //hrac je v interact fielde
        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("enter");
            player_in_field=true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("exit");
            player_in_field=false;
        }
    }
}
