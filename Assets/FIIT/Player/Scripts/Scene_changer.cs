using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_changer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("to_elevator"))
        {
            SceneManager.LoadScene("elevator_lobby");
        }

        if (collider.CompareTag("to_floor_selector"))
        {
            SceneManager.LoadScene("elevator_selector");
        }

        if (collider.CompareTag("to_ground"))
        {
            SceneManager.LoadScene("ground_floor");
        }

        if (collider.CompareTag("to_fiitfood"))
        {
            SceneManager.LoadScene("fiitfood");
        }

        if (collider.CompareTag("to_first_floor"))
        {
            SceneManager.LoadScene("first_floor");
        }

        if (collider.CompareTag("to_library"))
        {
            SceneManager.LoadScene("library");
        }
        
        //TODO pridat vsetky ground floor transitions
    }
}
