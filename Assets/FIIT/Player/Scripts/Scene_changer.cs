using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_changer : MonoBehaviour
{
    public static Scene_changer Instance;

    //vytvorime instance tejto script, budeme s nou pracovat v elevator_selector
    void Start()
    {
        if(Instance== null)
        {
            Instance=this;
        }
    }


    //avert your gaze!! cringe code alert!!
    void OnTriggerEnter2D(Collider2D collider)
    {
        
        //elevator_lobby -> selector
        if (collider.CompareTag("to_floor_selector"))
        {
            //ziadny vektor, lebo hrac v scene nie je
            SceneManager.LoadScene("elevator_selector");
        }

        //zo scen do ground floor
        if (collider.CompareTag("to_ground_up"))
        {
            SceneManager.LoadScene("ground_floor");
            transform.position = new Vector3(3,8,0);
        }
        if (collider.CompareTag("to_ground_library"))
        {
            SceneManager.LoadScene("ground_floor");
            transform.position = new Vector3(30.5f,5.5f,0);
        }
        if (collider.CompareTag("to_ground_fiitfood"))
        {
            SceneManager.LoadScene("ground_floor");
            transform.position = new Vector3(-24,6,0);
        }

        //ground_floor -> scenes
        if (collider.CompareTag("to_fiitfood"))
        {
            SceneManager.LoadScene("fiitfood");
            transform.position = new Vector3(43,-9,0);
        }

        if (collider.CompareTag("to_first_floor"))
        {
            SceneManager.LoadScene("first_floor");
            transform.position = new Vector3(-2,11,0);
        }

        if (collider.CompareTag("to_library"))
        {
            SceneManager.LoadScene("library");
            transform.position = new Vector3(8.5f,-10,0);
        }

        if (collider.CompareTag("to_elevator"))
        {
            SceneManager.LoadScene("elevator_lobby");
            transform.position = new Vector3(0,0,0);
        }
        
        
        //TODO pridat vsetky ground floor transitions
    }

    public void from_elevator(string floor)
    {
        if (floor == "coworking")
        {
            SceneManager.LoadScene("coworking");
            transform.position = new Vector3(-13,-5,0);
        }
        else if (floor == "first_floor")
        {
            SceneManager.LoadScene("first_floor");
            transform.position = new Vector3(-2,19,0);
        }
        else if (floor == "ground_floor")
        {
            SceneManager.LoadScene("ground_floor");
            transform.position = new Vector3(2,1,0);
        }
        else if (floor == "elevator_lobby")
        {
            SceneManager.LoadScene("elevator_lobby");
            transform.position = new Vector3(0,0,0);
        }
    }
}
