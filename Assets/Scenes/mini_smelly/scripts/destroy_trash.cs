using UnityEngine;

public class destroy_trash : MonoBehaviour
{   
    [SerializeField] private GameObject trash_parent;
    private collect_trash trash_script;
    void Start()
    {
        trash_script=trash_parent.GetComponent<collect_trash>();
    }
    void Update()
    {}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("collision with player");
            trash_script.curr_trash++;
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("wtf");
        }
    }

}
