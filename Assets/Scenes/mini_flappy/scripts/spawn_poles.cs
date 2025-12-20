using UnityEngine;

public class spawn_poles : MonoBehaviour
{
    [SerializeField] private GameObject poles;
    [SerializeField] private float spawn_rate=3f;
    //[SerializeField] private float pole_distance=6;
    [SerializeField] private float pole_speed=5f;
    private Vector2 xy;
    private scoring_system score_script;

    private float timer;
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        score_script=player.GetComponent<scoring_system>();
        timer=spawn_rate;
        xy= new Vector2(-pole_speed,0);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("score:"+score_script.score);
        timer-=Time.deltaTime;
        if (timer <= 0)
        {
            spawner();
            timer=spawn_rate;
        }
    }

    void spawner()
    {
        //spawn height randomizer
        float spawn_height = Random.Range(-0.4f, 1);
        float random_rotation =  Random.Range(-35f,35f); 
        //zmenime random_rotation na quaternion, aby sme to mohli pouzit v Instantiate
        Quaternion spawn_rotation = Quaternion.Euler(0, 0, 0);

        //idea, ze po skore 15 sa zvysi difficulty
        if (score_script.score > 5)
        {
            spawn_rotation=Quaternion.Euler(0, 0, random_rotation);
        }

        GameObject poles_instance = Instantiate(
            poles,
            //x pozicia mimo view, random spawn vyska, random rotacia
            new Vector3(10,spawn_height,0),
            spawn_rotation
        );
        poles_instance.GetComponent<Rigidbody2D>().constraints=RigidbodyConstraints2D.FreezeRotation;
        Debug.Log(poles_instance.transform.position);
        Rigidbody2D[] pole_children=poles_instance.GetComponentsInChildren<Rigidbody2D>();

        foreach(Rigidbody2D rb in pole_children)
        {
            rb.linearVelocity=xy;
            //freeze rotation nech sa po spawnuti nekrutia, aj ked som vypol gravity tak preistotu
            rb.constraints=RigidbodyConstraints2D.FreezeRotation;
        }
        //GameObject pole_down
    }
}
