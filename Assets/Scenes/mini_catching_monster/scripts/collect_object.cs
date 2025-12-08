using UnityEngine;

public class collect_object : MonoBehaviour
{
    [SerializeField] private GameObject bar;
    void OnTriggerEnter2D(Collider2D other)
    {
        //zoberieme script z padajuceho objektu
        progress_bar bar_script = bar.GetComponent<progress_bar>();

        if (other.gameObject.CompareTag("basket"))
        {
            if (gameObject.CompareTag("monster"))
            {
                Debug.Log("collected monster");
                Destroy(gameObject);
                bar_script.collect_monster();
            }
            else
            {
                Debug.Log("collected trash");
                Destroy(gameObject);
                bar_script.collect_trash();
            }
        }
    }
}
